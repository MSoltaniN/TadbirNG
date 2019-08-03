USE [NGTadbirSys]
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

CREATE TABLE [Metadata].[View] (
    [ViewID]                 INT              IDENTITY (1, 1) NOT NULL,
    [Name]                   VARCHAR(64)      NOT NULL,
    [EntityType]             NVARCHAR(32)     NOT NULL,
    [IsHierarchy]            BIT              NOT NULL,
    [IsCartableIntegrated]   BIT              NOT NULL,
    [FetchUrl]               NVARCHAR(512)    NOT NULL,
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
    [Type]             VARCHAR(32)      NULL,
    [DotNetType]       VARCHAR(64)      NOT NULL,
    [StorageType]      VARCHAR(32)      NOT NULL,
    [ScriptType]       VARCHAR(32)      NOT NULL,
    [Length]           INT              NOT NULL,
    [MinLength]        INT              NOT NULL,
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
    [Name]                NVARCHAR(64)     NOT NULL,
    [EntityName]          NVARCHAR(64)     NULL,
    [Description]         NVARCHAR(512)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_PermissionGroup_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Auth_PermissionGroup_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_PermissionGroup] PRIMARY KEY CLUSTERED ([PermissionGroupID] ASC)
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

CREATE TABLE [Metadata].[ReportView](
	[ViewID]        INT              IDENTITY(1,1) NOT NULL,
	[Name]          NVARCHAR(64)     NOT NULL,
	[Description]   NVARCHAR(512)    NULL,
	[rowguid]       UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_ReportView_rowguid] DEFAULT (newid()) ROWGUIDCOL  NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_Metadata_ReportView_ModifiedDate] DEFAULT (getdate()) NOT NULL,
 CONSTRAINT [PK_Metadata_ReportView] PRIMARY KEY CLUSTERED([ViewID] ASC)
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
    [Values]         NTEXT            NOT NULL,
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
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Command_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Command_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Command] PRIMARY KEY CLUSTERED ([CommandID] ASC)
    , CONSTRAINT [FK_Metadata_Command_Metadata_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Metadata].[Command]([CommandID])
    , CONSTRAINT [FK_Metadata_Command_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission]([PermissionID])
)
GO

CREATE TABLE [Config].[CompanyDb] (
    [CompanyID]      INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [DbName]         NVARCHAR(128)    NOT NULL,
    [DbPath]         NVARCHAR(512)    NOT NULL,
    [Server]         NVARCHAR(64)     NOT NULL,
    [UserName]       NVARCHAR(32)     NULL,
    [Password]       NVARCHAR(32)     NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Corporate_Company_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Corporate_Company_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Corporate_Company] PRIMARY KEY CLUSTERED ([CompanyID] ASC)
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

CREATE TABLE [Core].[OperationLog] (
    [OperationLogID]   INT              IDENTITY (1, 1) NOT NULL,
    [UserID]           INT              NOT NULL,
    [CompanyID]        INT              NOT NULL,
    [Date]             DATETIME         NOT NULL,
    [Time]             TIME(7)          NOT NULL,
    [View]             NVARCHAR(64)     NOT NULL,
    [Action]           NVARCHAR(64)     NOT NULL,
    [Succeeded]        BIT              NOT NULL,
    [FailReason]       NVARCHAR(1024)   NULL,
    [BeforeState]      NVARCHAR(1024)   NULL,
    [AfterState]       NVARCHAR(1024)   NULL,
    [FiscalPeriodId]   INT              NOT NULL,
    [BranchId]         INT              NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Core_OperationLog_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Core_OperationLog_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_OperationLog] PRIMARY KEY CLUSTERED ([OperationLogID] ASC)
    , CONSTRAINT [FK_Core_OperationLog_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
    , CONSTRAINT [FK_Core_OperationLog_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb]([CompanyID])
)
GO


-- Create system metadata records

SET IDENTITY_INSERT [Metadata].[Locale] ON
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName, Code) VALUES (1, 'English', N'English', 'en')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName, Code) VALUES (2, 'Persian', N'فارسی', 'fa')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName, Code) VALUES (3, 'Arabic', N'العربیه', 'ar')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName, Code) VALUES (4, 'French', N'Français', 'fr')
SET IDENTITY_INSERT [Metadata].[Locale] OFF

SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (1, 'Account', 1, 1, N'/lookup/accounts')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (2, 'Voucher', 0, 1, N'/lookup/vouchers')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (3, 'VoucherLine', 0, 1, N'/lookup/vouchers/lines')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (4, 'User', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (5, 'Role', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (6, 'DetailAccount', 1, 1, N'/lookup/faccounts')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (7, 'CostCenter', 1, 1, N'/lookup/ccenters')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (8, 'Project', 1, 1, N'/lookup/projects')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (9, 'FiscalPeriod', 0, 1, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (10, 'Branch', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (11, 'CompanyDb', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (12, 'AccountGroup', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (13, 'OperationLog', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (14, N'AccountCollectionAccount', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (15, N'JournalByDateByRow', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (16, N'JournalByDateByRowDetail', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (17, N'JournalByDateByLedger', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (18, N'JournalByDateBySubsidiary', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (19, N'JournalByDateSummary', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (20, N'JournalByDateSummaryByDate', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (21, N'JournalByDateSummaryByMonth', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (22, N'JournalByNoByRow', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (23, N'JournalByNoByRowDetail', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (24, N'JournalByNoByLedger', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (25, N'JournalByNoBySubsidiary', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (26, N'JournalByNoSummary', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (27, N'AccountBookSingle', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (28, N'AccountBookSingleSummary', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (29, N'AccountBookSummary', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (30, 'Currency', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (31, 'CurrencyRate', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON 
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (1, 1, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'a131986b-2bb4-4040-b188-d04d7b784c47', CAST(N'2019-05-20T13:53:17.657' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (2, 1, N'Code', NULL, N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL, 0, NULL, N'7c938f86-3dab-414d-b4ae-a194a723014c', CAST(N'2019-05-20T13:53:18.147' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (3, 1, N'FullCode', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL, 1, NULL, N'2a7cc841-6191-4ff6-917a-cf7b382a6c48', CAST(N'2019-05-20T13:53:18.147' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (4, 1, N'Name', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL, N'b716b040-dc16-4516-b35f-9873aa8b4eb7', CAST(N'2019-05-20T13:53:18.147' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (5, 1, N'Level', NULL, N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', 3, NULL, N'a203d09e-ae21-4ced-94e3-b6571fe121d1', CAST(N'2019-05-20T13:53:18.147' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (6, 1, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL, N'6eb19c76-e367-4497-a7a6-64ae90fd97c3', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (7, 1, N'BranchScope', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'AlwaysHidden', -1, NULL, N'e2242cb0-e536-4994-939e-f616c035842e', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (8, 1, N'GroupId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'ac8c2dfb-e3b8-4a61-a7fe-cf1613832e60', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (9, 1, N'CurrencyId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'd1d7e325-7d14-4155-aa98-cd81859f81e1', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (10, 1, N'IsActive', NULL, N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 1, 1, 1, N'Hidden', 5, NULL, N'5efa410d-a2bd-4749-a469-2bbbc3d2f75a', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (11, 1, N'IsCurrencyAdjustable', NULL, N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL, N'198bb5ce-fc99-4cc1-98ff-9c36f40266bd', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (12, 1, N'TurnoverMode', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 1, 1, 1, N'Hidden', 7, NULL, N'0d8a3336-7ea4-48f0-a8f3-4a3b95012969', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (13, 2, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'8787dc72-063c-4f9b-8428-48e6da35baf1', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (14, 2, N'No', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'048a3cbb-6f6f-4a23-aa44-a20479e959ad', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (15, 2, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'297aa638-04e3-4f40-bb1a-d1bc75e8909c', CAST(N'2019-05-20T13:53:18.150' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (16, 2, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 2, NULL, N'974d92a9-105f-485a-8c44-1c9bf6cb3b75', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (17, 2, N'StatusName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, NULL, 3, NULL, N'0e949d99-1e58-478a-82e9-3964d82d1fc9', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (18, 2, N'Reference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, NULL, 4, NULL, N'3ef32ec6-874a-46dd-ab7f-1e92e5d88a0e', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (19, 2, N'Association', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, NULL, 5, NULL, N'bb1bbc00-1645-4f4f-bfcb-1339343b0bfc', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (20, 3, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'649d6059-ffc7-44c3-9eba-f68a975b13ec', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (21, 3, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 8, NULL, N'9d128331-0222-4790-8973-abc0af15a672', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (22, 3, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL, N'04c0a26d-bfdf-416e-b6f1-ab7aaf7b0a0d', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (23, 3, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL, N'e1f48cdf-0574-4dee-93c5-5bddc461282e', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (24, 3, N'CurrencyId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'948526ec-fab1-4ff3-835a-0f6442e39c03', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (25, 3, N'FullAccount', NULL, N'System.Object', N'(n/a)', N'object', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'9a20e8e0-ff89-400b-ac1e-f0d2522df3d0', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (26, 3, N'FullAccount.Account.Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'0d836f6c-bdd3-415b-9303-d1b6e1cbd1a1', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (27, 3, N'FullAccount.DetailAccount.Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'de3351d4-237c-4e41-a5ec-869d793e44ed', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (28, 3, N'FullAccount.CostCenter.Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'c8d97331-82b3-4690-aae1-4c7e1942cae7', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (29, 3, N'FullAccount.Project.Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'db0bf32d-1a15-4319-b59c-f937127fa454', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (30, 3, N'FullAccount.Account.FullCode', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL, 0, NULL, N'9ecee01c-b787-430e-a109-86b7d7ec8e13', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (31, 4, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'a10315f9-4a19-4b14-bf61-42632ca839de', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (32, 4, N'UserName', NULL, N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'3fb41b0f-fb58-4f46-b96a-0a43680199ba', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (33, 4, N'Password', NULL, N'System.String', N'nvarchar(32)', N'string', 32, 4, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'52e7d18b-eae4-4740-a441-daa8f81e9e09', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (34, 4, N'LastLoginDate', NULL, N'System.DateTime', N'datetime', N'DateTime', 0, 0, 0, 1, 1, 1, NULL, 3, NULL, N'8bfaaaed-1f88-49f3-9062-284ad4fe23db', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (35, 4, N'IsEnabled', NULL, N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 0, 1, 1, NULL, 4, NULL, N'2d889074-ac7a-40ff-bb9c-5a15b3688d55', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (36, 4, N'PersonFirstName', NULL, N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, N'Person.FirstName', N'9c4507f8-ee9a-4c0a-bba1-e67dfed92cb4', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (37, 4, N'PersonLastName', NULL, N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 2, NULL, 5, N'Person.LastName', N'55a34f6a-79f0-48c3-9880-104b5b5bb1d3', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (38, 5, N'Name', NULL, N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'7433ea78-52f7-45ea-a590-1667a0759a21', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (39, 5, N'Description', NULL, N'System.String', N'nvarchar(512)', N'string', 512, 0, 0, 1, 1, 1, NULL, 1, NULL, N'e2507fbc-9dac-41cb-84b1-94ad3901826b', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (40, 6, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'2a52cb6b-47dc-48b4-bb08-2bcf6bb236d0', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (41, 6, N'Code', NULL, N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL, 0, NULL, N'fc2fe355-10db-4490-a710-a3aa3ee72331', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (42, 6, N'FullCode', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL, 1, NULL, N'cb969bc2-68a4-42fc-b2fe-1de034f337c4', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (43, 6, N'Name', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL, N'ef6cf467-4fc8-4869-af6d-10454530de74', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (44, 6, N'Level', NULL, N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'4b0e8166-79f5-45c4-a05f-edb899c67b16', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (45, 6, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 3, NULL, N'92bca738-7360-42f2-8d35-dac94333efeb', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (46, 6, N'BranchScope', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'AlwaysHidden', -1, NULL, N'ffbc3049-24f9-4786-af99-89b44b912b6d', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (47, 7, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'ab2e842b-acaa-4c73-a54b-cadb0958010f', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (48, 7, N'Code', NULL, N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL, 0, NULL, N'df56fbd2-1344-4ba4-abc3-df2a2af887f8', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (49, 7, N'FullCode', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL, 1, NULL, N'61027699-aec0-418e-a10b-3342335910be', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (50, 7, N'Name', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL, N'7b88ba04-395e-4021-b4ea-a1cf855eee32', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (51, 7, N'Level', NULL, N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'3844ce0d-7e70-4b0f-a905-c267bc860082', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (52, 7, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 3, NULL, N'1c1b67e3-bfee-48e4-85c6-7bfc2f18c185', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (53, 7, N'BranchScope', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'AlwaysHidden', -1, NULL, N'078a889b-6fdd-4356-aca9-82331c5c7df1', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (54, 8, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'93c26795-9d63-4aa4-a5b3-774e06930b07', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (55, 8, N'Code', NULL, N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL, 0, NULL, N'ee7fc5aa-2c23-4cc7-a29c-a7efc7df0e9c', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (56, 8, N'FullCode', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL, 1, NULL, N'1534be52-3805-482b-ac79-b7bfa110a1a2', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (57, 8, N'Name', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL, N'69366e32-96d1-48c0-8a96-4af2118554d5', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (58, 8, N'Level', NULL, N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'2a66bdc3-1fda-4f9b-9343-24e25f302ab5', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (59, 8, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 3, NULL, N'f5f05df4-6146-4833-b423-280541992068', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (60, 8, N'BranchScope', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'AlwaysHidden', -1, NULL, N'3f16a34f-f3b0-4bf6-bc78-8f6cc58c0ef1', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (61, 9, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'cc8af974-6cde-42a8-8477-1961b9c9f9f6', CAST(N'2019-05-20T13:53:18.163' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (62, 9, N'Name', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'b09a48e8-1725-46b8-8329-7de3c832443e', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (63, 9, N'StartDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'4e7813c1-5363-4ba2-b05c-4b4a55aeed67', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (64, 9, N'EndDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 2, NULL, N'aafe50af-15b2-4893-9a3e-04143e4a9321', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (65, 9, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 3, NULL, N'fa8850a0-de84-4331-be21-36daeaf461a3', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (66, 10, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'fde2a898-d589-49af-b27a-a7b1b32ba524', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (67, 10, N'Name', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'e6c4f7b9-1468-4a96-b0ee-505b9ef80f82', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (68, 10, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 1, NULL, N'62392a2e-2c1c-4978-8f0a-52e42a8fcc25', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (69, 11, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'e3faf2a5-6450-4920-8d48-14ac0494419d', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (70, 11, N'Name', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'fde464e3-f1d1-43f0-a0b3-9212d46ecebc', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (71, 11, N'DbName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL, 1, NULL, N'869c4d36-87c8-4ffc-9edb-cfd7415c0bc0', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (72, 11, N'DbPath', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL, N'56eab0e2-4ed1-48b2-9400-577ac89b721b', CAST(N'2019-05-20T13:53:18.167' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (73, 11, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 3, NULL, N'f7f5389e-b31c-41ae-9f39-012d02199f4a', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (74, 11, N'Server', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL, N'274b13a6-b211-4fc6-8a20-b9bd5c174daf', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (75, 11, N'UserName', NULL, N'System.String', N'nvarchar', N'string', 32, 0, 0, 1, 1, 1, N'AlwaysVisible', 5, NULL, N'e34598a8-876e-4841-9990-d82c792b43e9', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (76, 11, N'Password', NULL, N'System.String', N'nvarchar', N'string', 32, 0, 0, 1, 1, 1, NULL, 6, NULL, N'32de91ec-2994-4721-943e-35335ab309a7', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (77, 12, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'd7a6680d-539c-4fed-a883-89d86e655be9', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (78, 12, N'Name', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'05915c74-9493-4bfa-843d-f1375e2be47b', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (79, 12, N'Category', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL, N'577b9a45-04cb-4918-8cec-2a82b1ffa6f8', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (80, 12, N'Description', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 1, 1, 1, NULL, 2, NULL, N'0c5e8259-5cd6-458c-b855-7e78662656db', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (81, 13, N'UserName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'be245264-5921-401e-b6ec-4a885077cea9', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (82, 13, N'CompanyName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL, N'f6602676-bad1-43ef-9c63-ec051c89e3a5', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (83, 13, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 2, NULL, N'94e921a5-02bb-4a35-a4a7-3641c33ef169', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (84, 13, N'Time', NULL, N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, NULL, 3, NULL, N'efbc0f74-a84b-4e29-8ca8-da189e353106', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (85, 13, N'Entity', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL, N'46b1a759-936a-43df-a878-1f70b697e108', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (86, 13, N'Action', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL, N'9b97e674-39f5-44ce-ad73-cb2c62273eab', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (87, 13, N'Result', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL, N'db86193b-1b84-4b09-92d9-062dc157a69e', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (88, 14, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'def2ddb2-66fb-4ffd-954e-2c2320b2b319', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (89, 14, N'Name', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'83640fb5-43bb-4aa4-b7e1-1e45cedb9a44', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (90, 14, N'FullCode', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL, 1, NULL, N'0d044911-7cc1-4c55-a10f-434e068e79fc', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (91, 15, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'1cb796dd-ef1c-48c9-a6a6-afb34684c5a2', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (92, 15, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'a48d157e-348a-4b92-9bd4-8bfe34eb0d39', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (93, 15, N'VoucherNo', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL, N'0091f417-bf72-4e08-87d3-99605d08346e', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (94, 15, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'9733c72d-aa80-4410-b7db-c5076eefd64e', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (95, 15, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'164a3ef7-7cda-49f4-85c8-759fc28f5675', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (96, 15, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL, N'b9303230-aa4f-4d69-af77-1419a911e04a', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (97, 15, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'af9c1215-362a-458d-a4eb-7f358c3cafc9', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (98, 15, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL, N'5bc1984f-862c-4252-a32d-19626d9f3229', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (99, 15, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL, N'4046def1-52df-4855-a013-b15a41ba9b6d', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (100, 16, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'bcd4a34f-235d-462f-aae1-f6bb0cd3ad37', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (101, 16, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'48eac34e-cc24-4492-8d35-520c9adcf55b', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (102, 16, N'VoucherNo', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL, N'a76d2c60-0dbb-41bf-9ebe-edbc884c6f24', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (103, 16, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'b671ab1d-1cb7-4c7f-b1d3-0f5b9b29507a', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (104, 16, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'770ea602-1253-480d-ab05-0b03f2af62d3', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (105, 16, N'DetailAccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL, N'fe8667c9-a4b0-44a9-b6fd-12641dda05c8', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (106, 16, N'DetailAccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 6, NULL, N'e7aea63d-2191-4615-b5ea-629e85ad047c', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (107, 16, N'CostCenterFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 7, NULL, N'4835d3ee-cbfc-4c9f-8b5e-89d28b7b0c26', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (108, 16, N'CostCenterName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 8, NULL, N'6f3e11ee-db76-4a8c-9499-c24052cf1d08', CAST(N'2019-05-20T13:53:18.177' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (109, 16, N'ProjectFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 9, NULL, N'5b194de1-2ca0-4fdb-8e90-006b424496be', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (110, 16, N'ProjectName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 10, NULL, N'd06ddaf8-729f-494e-8a0e-d015bf80dd30', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (111, 16, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 11, NULL, N'8fe8c274-539d-48d6-8fa7-906cd578b7e2', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (112, 16, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL, N'4d789388-8998-4ba4-b378-370bf5cfc56b', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (113, 16, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 13, NULL, N'ce286047-4a91-4962-b3c2-9bcefc0e0cb3', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (114, 16, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 14, NULL, N'b30aac0b-31fb-49d7-9e84-2385c9bcfd77', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (115, 17, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'cf15da28-c043-4984-a485-b0bcad3b99e3', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (116, 17, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'818a900e-10bf-414e-aa9b-f0b3c7ddc6a3', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (117, 17, N'VoucherNo', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL, N'080b21ab-36ea-4e01-b2ce-18eeecce7579', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (118, 17, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'31291664-8393-4b7e-9ead-1edcf7a713d5', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (119, 17, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'27ba8f2b-e6c0-4633-9318-3b0874db6e39', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (120, 17, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL, N'a3eb3daf-5f2d-4374-8927-cef6af4d43d6', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (121, 17, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'25ff7978-53f8-4f00-9f5a-2a28912b1695', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (122, 17, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL, N'6c9adbdd-e62e-4c74-8947-7d1997d48593', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (123, 17, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL, N'6fccee27-fcf9-44a9-8ccc-b34d8f4c4db0', CAST(N'2019-05-20T13:53:18.180' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (124, 18, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'83f91c00-1396-49c0-9117-28c3a3785d1e', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (125, 18, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'5928fd39-7963-4a25-aa22-46db3dd4edd0', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (126, 18, N'VoucherNo', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL, N'd70f1879-7250-42cb-bff6-780ff36e3a70', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (127, 18, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'4eea7bd9-42e2-4ef5-98c1-c08196127390', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (128, 18, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'52c0e0e7-35ac-4926-b86d-996797cb77de', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (129, 18, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL, N'46ecdc9e-b8ce-4367-82e3-b87bea0a5d03', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (130, 18, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'46d57f19-9802-4cea-9070-04ae8840ce52', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (131, 18, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL, N'faf685aa-85fb-4225-bd90-effd1b89125b', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (132, 18, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL, N'9df23c94-fb48-453a-b343-34d1bf853823', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (133, 19, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'7d7683d5-c6c1-451e-9616-ed7df96152e7', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (134, 19, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL, N'9ba8d027-3168-4bda-88c0-ffd6c0d875d9', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (135, 19, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL, N'cd0a4b7f-d6bb-485a-a79b-694e83465acc', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (136, 19, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'6ee912d1-f3c3-4869-b5af-192cbe2a2515', CAST(N'2019-05-20T13:53:18.183' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (137, 19, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL, N'6a43b624-d092-447b-86e1-ac85de915e1c', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (138, 19, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL, N'fa263042-613e-445c-ae1f-9bd6e0bb32bd', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (139, 19, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 6, NULL, N'71ce5654-4551-484a-9c08-f90ec2619bd5', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (140, 20, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'08fc1a97-6a9e-4737-b23e-af73118c7991', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (141, 20, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'933f264a-7308-4d5c-af9e-94e8cd369d2a', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (142, 20, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL, N'7c25f457-ef89-4d8f-af3d-fd335d145b05', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (143, 20, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'71aedd9f-dfae-46d0-8e9a-cd518f251ea8', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (144, 20, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'1e67c662-91d4-46f1-8667-3a79ccd352c1', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (145, 20, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL, N'7089c160-5d1e-411b-a5e3-f2527b3095c8', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (146, 20, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'7de5f248-18b5-4233-9863-7ad3d0434d15', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (147, 20, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL, N'0ab5e5e6-4788-4d61-8e71-66742a6ad205', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (148, 21, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'd92b8911-fee1-4a70-adca-d228d33e088b', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (149, 21, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'8e8f95a9-7c58-45ce-980e-80a7d018efa5', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (150, 21, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL, N'dfa865c1-49c0-4508-bc77-4ed9f1750a74', CAST(N'2019-05-20T13:53:18.187' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (151, 21, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'49283738-e0fe-4530-9e41-42354349de7a', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (152, 21, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'c942274e-7cd7-4ba1-ab26-fc5e93deec43', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (153, 21, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL, N'214e9a2a-6c82-4b4d-8156-8137732bc41f', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (154, 21, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'a14227a2-9342-47c1-86b3-f4d2152ab23d', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (155, 21, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL, N'1af04f93-1c3e-41bb-ae0b-b13e6c68d277', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (156, 22, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'4dcd64e3-8080-4083-a3e0-820f918c487e', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (157, 22, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'7bc1bde7-39a0-4134-9649-dcdc8008d54f', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (158, 22, N'VoucherNo', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL, N'aeae2ef6-3e14-42dc-99ae-e2ff8d442d15', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (159, 22, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'af9eab2c-c1dd-45f6-b947-725eeb471b6e', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (160, 22, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'b3892d1a-bb6e-4f26-aefe-673f4b27bd77', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (161, 22, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL, N'2bbc5775-04aa-4167-8608-fcbae972e801', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (162, 22, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'c99be7a5-85a0-41d7-af5b-12eff3a93330', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (163, 22, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL, N'47c5ca2b-f5ed-4229-a0ed-7bdb9286cc7f', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (164, 22, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL, N'19c57cf6-fa5b-4eee-bfe5-3b9ddadb35a7', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (165, 23, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'3e8fffa4-3e11-4422-86dc-b5d2c64d9688', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (166, 23, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'3e2df118-0601-4eeb-8c9b-01dc3d6ea7cc', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (167, 23, N'VoucherNo', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL, N'68dc6f4e-3e6b-4d7b-84f5-0003dc8dbe5a', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (168, 23, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'c5255341-b35d-4253-8b49-45f6019ba47e', CAST(N'2019-05-20T13:53:18.190' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (169, 23, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'4fbcdda5-33fc-4298-812e-40a732b3b2b9', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (170, 23, N'DetailAccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL, N'fa9fb431-afc9-4995-bb23-f20f2c16deba', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (171, 23, N'DetailAccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 6, NULL, N'78f002e5-70b4-4e7f-8480-a79adbb68240', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (172, 23, N'CostCenterFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 7, NULL, N'e86ffe0a-773a-4718-bb5f-c72decfc2901', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (173, 23, N'CostCenterName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 8, NULL, N'e9bdbb2a-e88f-45de-bce8-bf6ab15b7e1a', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (174, 23, N'ProjectFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 9, NULL, N'8d11cc36-0cf5-465c-be7e-2b7ddea8db46', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (175, 23, N'ProjectName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 10, NULL, N'06764e78-e747-4cf7-b56d-41049cf49665', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (176, 23, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 11, NULL, N'e9d05e8c-8167-400c-9680-41a77398c538', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (177, 23, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL, N'0490a2c1-321e-4f59-a033-70c5a463c727', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (178, 23, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 13, NULL, N'580cc9c3-982d-4201-8924-9d04d621fdb6', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (179, 23, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 14, NULL, N'266d0e2e-e9b3-4c8e-9709-4ee5eed966fc', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (180, 24, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'5e114589-a6b2-4575-8260-536f9e3c8ad2', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (181, 24, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'a2c58c6c-b888-48fc-b5ec-c7a866b3fb57', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (182, 24, N'VoucherNo', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL, N'8bd868d7-626e-431a-83a5-eb719bcb01e0', CAST(N'2019-05-20T13:53:18.193' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (183, 24, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'0b9b60b7-8705-4c48-af87-e86e0695460d', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (184, 24, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'e74020de-b282-45c1-b6db-6aa8b5e00777', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (185, 24, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL, N'445589a0-7e02-44c3-8bb3-6a919251e275', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (186, 24, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'2f81917d-626a-4372-9740-d73321324d94', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (187, 24, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL, N'1d3ac147-79ed-469f-9e89-dc81d83bf8d8', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (188, 24, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL, N'37a31230-a5c9-49c4-aa63-85b59ed75e70', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (189, 25, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'236e0e53-f3c6-4c9e-a326-4f36e76a88df', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (190, 25, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL, N'68e4ca6d-b0cb-4761-bf1c-642e74d8faf9', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (191, 25, N'VoucherNo', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL, N'b71cc255-f415-4bf8-9f69-3144a262c3a2', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (192, 25, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'c703ad61-6e5a-4be6-8cb1-be7191c2b4d6', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (193, 25, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL, N'e4b072d4-9baf-417b-b41d-36dc122fd80e', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (194, 25, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL, N'092ba99e-f187-409b-8740-203be2009fdc', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (195, 25, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'f950145e-8f78-4faa-84f5-df4ec1c88de0', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (196, 25, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL, N'359341c6-f1dd-458a-87d8-2a601f5c98d4', CAST(N'2019-05-20T13:53:18.197' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (197, 25, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL, N'9b5aac13-255c-4b1c-ac16-a800ffb2c5c7', CAST(N'2019-05-20T13:53:18.200' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (198, 26, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'd4a2a7f3-6cc7-4421-b6a4-f7c4c5de46a1', CAST(N'2019-05-20T13:53:18.200' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (199, 26, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL, N'a75f4a1e-ff66-4962-bca2-7cde68a1dfa5', CAST(N'2019-05-20T13:53:18.200' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (200, 26, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL, N'5af75e9e-0c51-4673-abfa-c38748a93640', CAST(N'2019-05-20T13:53:18.200' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (201, 26, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL, N'c37772fc-1859-45a9-b65a-aaf17264bb41', CAST(N'2019-05-20T13:53:18.200' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (202, 26, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL, N'e8b224b2-50ee-427a-a479-72768e4c3b51', CAST(N'2019-05-20T13:53:18.200' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (203, 26, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL, N'03405a84-ef0a-4385-bb61-d29be7f5a2ce', CAST(N'2019-05-20T13:53:18.200' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (204, 26, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 6, NULL, N'7a132281-a382-47af-b9f8-8432705d8596', CAST(N'2019-05-20T13:53:18.200' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (205, 2, N'StatusId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'43a06529-3347-41d9-9f7e-a9aaa1a08f60', CAST(N'2019-05-20T13:55:44.107' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (206, 2, N'DailyNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL, N'5aff6192-aae3-40c3-9a8c-1b3b08b1f8e4', CAST(N'2019-05-22T15:05:23.100' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (207, 2, N'Type', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'3cfdc909-62d1-44a4-9c97-ed0b411855cd', CAST(N'2019-05-22T15:05:23.593' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (208, 3, N'FullAccount.Account.Name', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL, 1, NULL, N'9fc6f028-6e6f-414a-bd74-33f628667b43', CAST(N'2019-05-22T15:34:24.417' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (209, 3, N'FullAccount.DetailAccount.FullCode', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 2, NULL, N'908e3fc7-0a64-4020-a454-65f566c636fa', CAST(N'2019-05-22T15:34:24.930' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (210, 3, N'FullAccount.DetailAccount.Name', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 3, NULL, N'12fc8fb9-76a4-43dc-9c65-8dcb9d6d4507', CAST(N'2019-05-22T15:34:24.930' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (211, 3, N'FullAccount.CostCenter.FullCode', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 4, NULL, N'2340386d-4aa8-432f-ae5b-897c13f164e4', CAST(N'2019-05-22T15:34:24.930' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (212, 3, N'FullAccount.CostCenter.Name', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 5, NULL, N'a4700fda-230b-48a5-ba6a-4b060ffc96ba', CAST(N'2019-05-22T15:34:24.930' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (213, 3, N'FullAccount.Project.FullCode', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 6, NULL, N'c36a7437-a687-48b6-857e-f359cce8d79a', CAST(N'2019-05-22T15:34:24.930' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (214, 3, N'FullAccount.Project.Name', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 7, NULL, N'6f65b60a-9553-45fb-8bd3-d293ad4fa459', CAST(N'2019-05-22T15:34:24.930' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) VALUES (215, 3, N'CurrencyName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) VALUES (216, 3, N'CurrencyValue', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysVisible', 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) VALUES (217, 3, N'CurrencyRate', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) VALUES (218, 3, N'TypeId', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) VALUES (219, 15, N'Mark', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 1, 1, 1, N'Visible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) VALUES (220, 16, N'Mark', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 1, 1, 1, N'Visible', 15, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) VALUES (221, 22, N'Mark', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 1, 1, 1, N'Visible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) VALUES (222, 23, N'Mark', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 1, 1, 1, N'Visible', 15, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (223, 27, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (224, 27, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (225, 27, N'VoucherNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (226, 27, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (227, 27, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (228, 27, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (229, 27, N'Balance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (230, 27, N'Mark', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (231, 27, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (232, 28, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (233, 28, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (234, 28, N'VoucherNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (235, 28, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (236, 28, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (237, 28, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (238, 28, N'Balance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (239, 28, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (240, 29, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (241, 29, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (242, 29, N'Count', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (243, 29, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (244, 29, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (245, 29, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (246, 29, N'Balance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (247, 29, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (248, 30, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (249, 30, N'Name', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (250, 30, N'Code', NULL, N'System.String', N'nvarchar', N'string', 8, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (251, 30, N'Country', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (252, 30, N'MinorUnit', NULL, N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (253, 30, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (254, 30, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (255, 30, N'DecimalCount', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (256, 30, N'BranchScope', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (257, 30, N'IsActive', NULL, N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 0, 1, 1, N'Hidden', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (258, 30, N'BranchId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (259, 31, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (260, 31, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (261, 31, N'Time', NULL, N'System.TimeSpan', N'time', N'number', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (262, 31, N'Multiplier', NULL, N'System.decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (263, 31, N'CurrencyId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (264, 31, N'BranchId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (265, 31, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL, 3, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF


-- Create configuration records...
SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (1, 'AccountRelationsSettings', 2, 1, 'RelationsConfig', N'{"useLeafDetails": true, "useLeafCostCenters": true,"useLeafProjects": true}', N'{"useLeafDetails": true, "useLeafCostCenters": true,"useLeafProjects": true}', 'AccountRelationsSettingsDescription')
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (2, 'DateRangeFilterSettings', 2, 0, 'DateRangeConfig', N'{"defaultDateRange": "FiscalStartToFiscalEnd"}', N'{"defaultDateRange": "FiscalStartToFiscalEnd"}', 'DateRangeFilterSettingsDescription')
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (3, 'NumberCurrencySettings', 2, 0, 'NumberDisplayConfig', N'{"useSeparator": true, "separatorMode": "UseCustom", "separatorSymbol": ",", "decimalPrecision": 0, "maxPrecision": 8}', N'{"useSeparator": true, "separatorMode": "UseCustom", "separatorSymbol": ",", "decimalPrecision": 0, "maxPrecision": 8}', 'NumberCurrencySettingsDescription')
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (4, 'ListFormViewSettings', 3, 2, 'ListFormViewConfig', N'{"pageSize": 10, "columnViews": []}', N'{"pageSize": 10, "columnViews": []}', 'ListFormViewSettingsDescription')
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (5, 'ViewTreeSettings', 2, 2, 'ViewTreeConfig', N'{}', N'{}', 'ViewTreeSettingsDescription')
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (6, 'QuickSearchSettings', 3, 2, 'QuickSearchConfig', N'{}', N'{}', 'QuickSearchSettingsDescription')
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (7, 'QuickReportSettings', 3, 2, 'QuickReportConfig', N'{}', N'{}', 'QuickReportSettingsDescription')
SET IDENTITY_INSERT [Config].[Setting] OFF


-- Create system records for security

-- admin user is added with password 'Demo1234' (case-sensitive)
SET IDENTITY_INSERT [Auth].[User] ON
INSERT INTO [Auth].[User] (UserID, UserName, PasswordHash, IsEnabled) VALUES (1, N'admin', 'b22f213ec710f0b0e86297d10279d69171f50f01a04edf40f472a563e7ad8576', 1)
SET IDENTITY_INSERT [Auth].[User] OFF

SET IDENTITY_INSERT [Contact].[Person] ON
INSERT INTO [Contact].[Person] (PersonID, UserID, FirstName, LastName) VALUES (1, 1, N'راهبر', N'سیستم')
SET IDENTITY_INSERT [Contact].[Person] OFF

SET IDENTITY_INSERT [Auth].[Role] ON
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (1, N'Role_SysAdmin', N'Role_SysAdminDesc')
SET IDENTITY_INSERT [Auth].[Role] OFF

SET IDENTITY_INSERT [Auth].[UserRole] ON
INSERT INTO [Auth].[UserRole] (UserRoleID, UserID, RoleID) VALUES (1, 1, 1)
SET IDENTITY_INSERT [Auth].[UserRole] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (1, N'ManageEntities,Accounts', N'Account')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (2, N'ManageEntities,DetailAccounts', N'DetailAccount')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (3, N'ManageEntities,CostCenters', N'CostCenter')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (4, N'ManageEntities,Projects', N'Project')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (5, N'ManageEntities,FiscalPeriods', N'FiscalPeriod')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (6, N'ManageEntities,Currencies', N'Currency')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (7, N'ManageEntities,Vouchers', N'Voucher')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (8, N'ManageEntities,Branches', N'Branch')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (9, N'ManageEntities,Companies', N'Company')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (10, N'ManageEntities,Users', N'User')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (11, N'ManageEntities,Roles', N'Role')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (12, N'AccountRelations', N'AccountRelations')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (13, N'RowAccessSettings', N'RowAccess')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (14, N'Settings', N'Setting')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (15, N'ManageEntities,AccountGroups', N'AccountGroup')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (16, N'ManageEntities,AccountCollections', N'AccountCollection')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (17, N'ManageEntities,OperationLogs', N'OperationLog')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (18, N'ManageEntities,UserReports', N'UserReport')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (19, N'JournalReport', N'Journal')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (20, N'ManageEntities,Reports', N'Report')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (21, N'AccountBookReport', N'AccountBook')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (22, N'CurrencyRate', N'CurrencyRate')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (1, 1, N'ViewEntities,Accounts', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (2, 1, N'CreateEntity,Account', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (3, 1, N'EditEntity,Account', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (4, 1, N'DeleteEntity,Account', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (5, 2, N'ViewEntities,DetailAccounts', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (6, 2, N'CreateEntity,DetailAccount', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (7, 2, N'EditEntity,DetailAccount', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (8, 2, N'DeleteEntity,DetailAccount', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (9, 3, N'ViewEntities,CostCenters', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (10, 3, N'CreateEntity,CostCenter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (11, 3, N'EditEntity,CostCenter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (12, 3, N'DeleteEntity,CostCenter', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (13, 4, N'ViewEntities,Projects', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (14, 4, N'CreateEntity,Project', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (15, 4, N'EditEntity,Project', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (16, 4, N'DeleteEntity,Project', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (17, 5, N'ViewEntities,FiscalPeriods', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (18, 5, N'CreateEntity,FiscalPeriod', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (19, 5, N'EditEntity,FiscalPeriod', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (20, 5, N'DeleteEntity,FiscalPeriod', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (21, 5, N'AssignRolesToEntity,FiscalPeriod', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (22, 6, N'ViewEntities,Currencies', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (23, 6, N'CreateEntity,Currency', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (24, 6, N'EditEntity,Currency', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (25, 6, N'DeleteEntity,Currency', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (26, 7, N'ViewEntities,Vouchers', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (27, 7, N'CreateEntity,Voucher', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (28, 7, N'EditEntity,Voucher', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (29, 7, N'DeleteEntity,Voucher', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (30, 7, N'NavigateEntities,Vouchers', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (31, 7, N'Lookup', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (32, 7, N'Filter', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (33, 7, N'Print', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (34, 7, N'Check', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (35, 7, N'UndoCheck', 512)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (36, 8, N'ViewEntities,Branches', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (37, 8, N'CreateEntity,Branch', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (38, 8, N'EditEntity,Branch', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (39, 8, N'DeleteEntity,Branch', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (40, 8, N'AssignRolesToEntity,Branch', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (41, 9, N'ViewEntities,Companies', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (42, 9, N'CreateEntity,Company', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (43, 9, N'EditEntity,Company', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (44, 9, N'DeleteEntity,Company', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (45, 10, N'ViewEntities,Users', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (46, 10, N'CreateEntity,User', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (47, 10, N'EditEntity,User', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (48, 10, N'AssignRolesToEntity,User', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (49, 11, N'ViewEntities,Roles', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (50, 11, N'CreateEntity,Role', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (51, 11, N'EditEntity,Role', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (52, 11, N'DeleteEntity,Role', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (53, 11, N'AssignEntityToRole,User', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (54, 11, N'AssignEntityToRole,Branch', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (55, 11, N'AssignEntityToRole,FiscalPeriod', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (56, 12, N'ViewEntities,AccountRelations', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (57, 12, N'ManageEntities,AccountRelations', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (58, 13, N'ViewEntities,ViewRowPermission', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (59, 13, N'ManageEntities,ViewRowPermission', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (60, 14, N'ViewEntities,Setting', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (61, 14, N'ManageEntities,Setting', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (62, 15, N'ViewEntities,AccountGroups', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (63, 15, N'CreateEntity,AccountGroup', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (64, 15, N'EditEntity,AccountGroup', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (65, 15, N'DeleteEntity,AccountGroup', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (66, 16, N'ViewEntities,AccountCollections', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (67, 16, N'CreateEntity,AccountCollection', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (68, 17, N'ViewEntities,OperationLogs', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (69, 18, N'SaveEntity,UserReport', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (70, 18, N'DeleteEntity,UserReport', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (71, 18, N'SetDefault,UserReport', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (72, 19, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (73, 19, N'Lookup', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (74, 19, N'Filter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (75, 19, N'Print', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (76, 19, N'Mark', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (77, 19, N'ViewByBranch', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (78, 20, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (79, 20, N'Design', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (80, 20, N'QuickReportDesign', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (81, 21, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (82, 21, N'Lookup', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (83, 21, N'Filter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (84, 21, N'Print', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (85, 21, N'Mark', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (86, 21, N'ViewByBranch', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (87, 7, N'Confirm', 1024)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (88, 7, N'UndoConfirm', 2048)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (89, 7, N'Approve', 4096)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (90, 7, N'UndoApprove', 8192)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (91, 7, N'Finalize', 16384)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (92, 6, N'Lookup', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (93, 6, N'Filter', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (94, 6, N'Print', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (95, 6, N'ChangeStatus', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (96, 22, N'ViewEntities,CurrencyRates', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (97, 22, N'CreateEntity,CurrencyRate', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (98, 22, N'EditEntity,CurrencyRate', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (99, 22, N'DeleteEntity,CurrencyRate', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (100, 22, N'Lookup', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (101, 22, N'Filter', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (102, 22, N'Print', 64)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Auth].[RolePermission] ON
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (1, 1, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (2, 1, 2)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (3, 1, 3)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (4, 1, 4)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (5, 1, 5)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (6, 1, 6)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (7, 1, 7)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (8, 1, 8)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (9, 1, 9)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (10, 1, 10)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (11, 1, 11)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (12, 1, 12)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (13, 1, 13)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (14, 1, 14)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (15, 1, 15)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (16, 1, 16)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (17, 1, 17)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (18, 1, 18)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (19, 1, 19)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (20, 1, 20)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (21, 1, 21)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (22, 1, 22)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (23, 1, 23)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (24, 1, 24)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (25, 1, 25)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (26, 1, 26)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (27, 1, 27)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (28, 1, 28)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (29, 1, 29)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (30, 1, 30)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (31, 1, 31)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (32, 1, 32)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (33, 1, 33)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (34, 1, 34)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (35, 1, 35)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (36, 1, 36)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (37, 1, 37)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (38, 1, 38)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (39, 1, 39)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (40, 1, 40)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (41, 1, 41)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (42, 1, 42)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (43, 1, 43)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (44, 1, 44)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (45, 1, 45)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (46, 1, 46)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (47, 1, 47)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (48, 1, 48)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (49, 1, 49)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (50, 1, 50)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (51, 1, 51)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (52, 1, 52)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (53, 1, 53)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (54, 1, 54)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (55, 1, 55)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (56, 1, 56)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (57, 1, 57)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (58, 1, 58)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (59, 1, 59)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (60, 1, 60)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (61, 1, 61)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (62, 1, 62)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (63, 1, 63)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (64, 1, 64)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (65, 1, 65)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (66, 1, 66)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (67, 1, 67)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (68, 1, 68)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (69, 1, 69)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (70, 1, 70)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (71, 1, 71)
SET IDENTITY_INSERT [Auth].[RolePermission] OFF
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 72)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 73)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 74)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 75)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 76)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 77)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 78)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 79)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 80)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 81)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 82)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 83)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 84)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 85)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 86)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 87)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 88)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 89)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 90)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 91)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 92)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 93)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 94)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 95)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 96)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 97)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 98)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 99)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 100)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 101)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 102)

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (1, NULL, 1, 1, NULL, N'Administration', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (2, 1, 1, 1, NULL, N'Admin-Base', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (3, 1, 1, 1, NULL, N'Admin-Operation', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (4, 1, 1, 1, NULL, N'Admin-Report', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (5, 2, 1, 1, NULL, N'Admin-Base-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (6, 3, 1, 1, NULL, N'Admin-Operation-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (7, 4, 1, 1, NULL, N'Admin-Report-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (8, 5, 1, 1, 11, N'Companies', N'companies', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (9, 5, 1, 1, 10, N'Branches', N'branches', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (10, 5, 1, 1, 4, N'Users', N'users', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (11, 5, 1, 1, 5, N'Roles', N'roles', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (12, 6, 1, 1, 13, N'Operation-Logs', N'oplog', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (13, NULL, 1, 2, NULL, N'Accounting', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (14, 13, 1, 2, NULL, N'Accnt-Base', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (15, 13, 1, 2, NULL, N'Accnt-Operation', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (16, 13, 1, 2, NULL, N'Accnt-Report', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (17, 14, 1, 2, NULL, N'Accnt-Base-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (18, 15, 1, 2, NULL, N'Accnt-Operation-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (19, 16, 1, 2, NULL, N'Accnt-Report-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (20, 15, 1, 2, NULL, N'Voucher-Printing', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (21, 17, 1, 2, 9, N'Fiscal-Periods', N'fperiods', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (22, 17, 1, 2, 1, N'Accounts', N'accounts', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (23, 17, 1, 2, 6, N'Detail-Accounts', N'faccounts', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (24, 17, 1, 2, 7, N'Cost-Centers', N'ccenters', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (25, 17, 1, 2, 8, N'Projects', N'projects', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (26, 17, 1, 2, 12, N'Account-Groups', N'accgroups', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (27, 19, 1, 2, 15, N'Journal-ByDate-ByRow', N'reports/journal/by-date/by-row', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (28, 19, 1, 2, 16, N'Journal-ByDate-ByRow-Detail', N'reports/journal/by-date/by-row-detail', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (29, 19, 1, 2, 17, N'Journal-ByDate-ByLedger', N'reports/journal/by-date/by-ledger', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (30, 19, 1, 2, 18, N'Journal-ByDate-BySubsidiary', N'reports/journal/by-date/by-subsid', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (31, 19, 1, 2, 19, N'Journal-ByDate-LedgerSummary', N'reports/journal/by-date/summary', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (32, 19, 1, 2, 20, N'Journal-ByDate-LedgerSummary-ByDate', N'reports/journal/by-date/sum-by-date', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (33, 19, 1, 2, 21, N'Journal-ByDate-LedgerSummary-ByMonth', N'reports/journal/by-date/sum-by-month', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (34, 19, 1, 2, 22, N'Journal-ByNo-ByRow', N'reports/journal/by-no/by-row', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (35, 19, 1, 2, 23, N'Journal-ByNo-ByRow-Detail', N'reports/journal/by-no/by-row-detail', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (36, 19, 1, 2, 24, N'Journal-ByNo-ByLedger', N'reports/journal/by-no/by-ledger', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (37, 19, 1, 2, 25, N'Journal-ByNo-BySubsidiary', N'reports/journal/by-no/by-subsid', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (38, 19, 1, 2, 26, N'Journal-ByNo-LedgerSummary', N'reports/journal/by-no/summary', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (39, 18, 1, 2, 2, N'Voucher-Sum-By-Date', N'reports/voucher/sum-by-date', 0, 1, 0, 0, N'RowNo,Voucher,Date,DebitSum,CreditSum,Difference,PreparedBy,BalanceLabel,CheckLabel,Origin')
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (40, 20, 1, 2, 1, N'Voucher-Std-Form', N'reports/voucher/std-form', 0, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (41, 20, 1, 2, 1, N'Voucher-Std-Form-Detail', N'reports/voucher/std-form-detail', 0, 1, 1, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (42, NULL, 1, 1, NULL, N'Report-QReport-Manage', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (43, 42, 1, 1, NULL, N'QReport-Design-Template', NULL, 0, 1, 1, 0, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (1, 27, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (2, 27, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (3, 28, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (4, 28, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (5, 29, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (6, 29, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (7, 30, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (8, 30, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (9, 31, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (10, 31, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (11, 32, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (12, 32, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (13, 33, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (14, 33, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (15, 34, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (16, 34, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (17, 35, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (18, 35, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (19, 36, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (20, 36, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (21, 37, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (22, 37, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (23, 38, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (24, 38, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (25, 39, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (26, 39, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (1, 1, 1, N'Administration', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (2, 2, 1, N'راهبری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (3, 3, 1, N'Administration', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (4, 4, 1, N'Administration', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (5, 1, 2, N'Base data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (6, 2, 2, N'اطلاعات پایه', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (7, 3, 2, N'Base data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (8, 4, 2, N'Base data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (9, 1, 3, N'Operational data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (10, 2, 3, N'اطلاعات عملیاتی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (11, 3, 3, N'Operational data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (12, 4, 3, N'Operational data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (13, 1, 4, N'Reports', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (14, 2, 4, N'گزارشات', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (15, 3, 4, N'Reports', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (16, 4, 4, N'Reports', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (17, 1, 5, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (18, 2, 5, N'گزارش فوری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (19, 3, 5, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (20, 4, 5, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (21, 1, 6, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (22, 2, 6, N'گزارش فوری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (23, 3, 6, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (24, 4, 6, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (25, 1, 7, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (26, 2, 7, N'گزارش فوری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (27, 3, 7, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (28, 4, 7, N'Quick Report', NULL)
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
    VALUES (45, 1, 12, N'Operation logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (46, 2, 12, N'لاگ های عملیاتی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (47, 3, 12, N'Operation logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (48, 4, 12, N'Operation logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (49, 1, 13, N'Accounting', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (50, 2, 13, N'حسابداری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (51, 3, 13, N'Accounting', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (52, 4, 13, N'Accounting', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (53, 1, 14, N'Base data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (54, 2, 14, N'اطلاعات پایه', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (55, 3, 14, N'Base data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (56, 4, 14, N'Base data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (57, 1, 15, N'Operational data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (58, 2, 15, N'اطلاعات عملیاتی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (59, 3, 15, N'Operational data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (60, 4, 15, N'Operational data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (61, 1, 16, N'Reports', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (62, 2, 16, N'گزارشات', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (63, 3, 16, N'Reports', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (64, 4, 16, N'Reports', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (65, 1, 17, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (66, 2, 17, N'گزارش فوری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (67, 3, 17, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (68, 4, 17, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (69, 1, 18, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (70, 2, 18, N'گزارش فوری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (71, 3, 18, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (72, 4, 18, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (73, 1, 19, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (74, 2, 19, N'گزارش فوری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (75, 3, 19, N'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (76, 4, 19, N'Quick Report', NULL)
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
    VALUES (153, 1, 39, N'Voucher summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (154, 2, 39, N'خلاصه اسناد بر حسب تاریخ')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (155, 3, 39, N'Voucher summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (156, 4, 39, N'Voucher summary by date')
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
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

-- Sample user settings for UserID = 1 and Account List form (Admin user)...
SET IDENTITY_INSERT [Config].[UserSetting] ON
INSERT [Config].[UserSetting] ([UserSettingID], [SettingID], [ViewID], [UserID], [RoleID], [ModelType], [Values])
    VALUES (1, 4, 1, 1, NULL, N'ListFormViewConfig', N'{"viewId":1,"pageSize":25,"columnViews":[{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}},{"name":"Code","large":{"width":100,"index":0,"designIndex":0,"visibility":"Visible"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"Visible"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}},{"name":"FullCode","large":{"width":150,"index":1,"designIndex":0,"visibility":"Visible"},"medium":{"width":150,"index":1,"designIndex":0,"visibility":"Visible"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}},{"name":"Name","large":{"width":180,"index":2,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":180,"index":2,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":125,"index":2,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":125,"index":2,"designIndex":0,"visibility":"AlwaysVisible"}},{"name":"Level","large":{"width":50,"index":4,"designIndex":0,"visibility":"Visible"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}},{"name":"Description","large":{"width":360,"index":3,"designIndex":0,"visibility":"Visible"},"medium":{"width":360,"index":3,"designIndex":0,"visibility":"Visible"},"small":{"width":180,"index":3,"designIndex":0,"visibility":"Visible"},"extraSmall":{"width":180,"index":3,"designIndex":0,"visibility":"Visible"}}]}')
SET IDENTITY_INSERT [Config].[UserSetting] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (1, NULL, NULL, N'Accounting', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (2, 1, NULL, N'BaseData', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (3, 2, 62, N'AccountGroup', N'/account-groups', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (4, 2, 1, N'Account', N'/account', 'tasks', 'Ctrl+Shift+A')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (5, 2, 5, N'DetailAccount', N'/detailAccount', 'list', 'Ctrl+Shift+D')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (6, 2, 9, N'CostCenter', N'/costCenter', 'list', 'Ctrl+Shift+C')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (7, 2, 13, N'Project', N'/projects', 'list', 'Ctrl+Shift+P')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (8, 2, 56, N'AccountRelations', N'/accountrelations', 'list', 'Ctrl+Shift+R')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (9, 2, 66, N'AccountCollections', N'/account-collection', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (10, 2, 22, N'Currency', N'/currency', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (11, 1, NULL, N'VoucherOps', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (12, 11, 27, N'NewVoucher', N'/vouchers/new', N'list', N'Ctrl+N')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (13, 11, 28, N'VoucherByNo', N'/vouchers/by-no', N'list', N'Ctrl+S')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (14, 11, 28, N'LastVoucher', N'/vouchers/last', N'list', N'Ctrl+L')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (15, 11, 26, N'Vouchers', N'/voucher', 'list', 'Ctrl+Shift+V')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (16, 1, NULL, N'SpecialOps', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (17, 16, NULL, N'IssueOpeningVoucher', N'/opening-voucher', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (18, 16, NULL, N'IssueClosingVoucher', N'/closing-voucher', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (19, 16, NULL, N'ClosingTempAccounts', N'/close-temp-accounts', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (20, 1, NULL, N'AccountingLedgers', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (21, 20, 72, N'JournalLedger', N'/journal', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (22, 20, 81, N'AccountBook', N'/account-book', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (23, NULL, NULL, N'Organization', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (24, 23, 41, N'Companies', N'/companies', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (25, 23, 36, N'Branches', N'/branches', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (26, 23, 17, N'FiscalPeriods', N'/fiscalperiod', 'list', 'Ctrl+Shift+F')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (27, NULL, NULL, N'Administration', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (28, 27, 45, N'Users', N'/users', 'user', 'Ctrl+Shift+U')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (29, 27, 49, N'Roles', N'/roles', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (30, 27, 58, N'RowAccessSettings', N'/viewRowPermission', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (31, 27, 60, N'Settings', N'/settings', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (32, 27, 68, N'OperationLogs', N'/operation-log', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (33, NULL, NULL, N'Profile', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (34, 33, NULL, N'ChangePassword', N'/changePassword', 'eye-open', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (35, 33, NULL, N'LogOut', N'/logout', 'log-out', 'Ctrl+Shift+X')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (36, 33, NULL, N'ChangeCompany', N'/login', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (37, NULL, NULL, N'Tools', NULL, N'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (38, 37, 78, N'ReportManagement', N'reports', N'list', N'Ctrl+R')
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


--create report parameters
delete [Reporting].[Parameter]

SET IDENTITY_INSERT [Reporting].[Parameter] ON 

GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (1, 22, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate', N'b8b827a7-69d7-40a2-9fff-c63efeef059f', CAST(N'2019-04-26T12:10:23.803' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (2, 22, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate', N'544b8a44-8ff7-4554-823a-b1830e99eaa2', CAST(N'2019-04-26T12:10:23.803' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (3, 23, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate', N'fb7a563b-f777-4b21-a518-13213052146f', CAST(N'2019-04-26T12:10:23.803' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (4, 23, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate', N'0472b468-563a-4fa3-80b1-db7088527459', CAST(N'2019-04-26T12:10:23.803' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (5, 24, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate', N'e1f27ad6-738e-4101-a9dd-f9518bb63536', CAST(N'2019-04-26T12:10:23.803' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (6, 24, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate', N'14b1a7a2-928c-461a-a752-bda0de0efa5e', CAST(N'2019-04-26T12:10:23.803' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (7, 25, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate', N'd4d9d58a-9c31-450d-9aa9-12fa989566c0', CAST(N'2019-04-26T12:10:23.807' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (8, 25, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate', N'9e0f5238-534f-483a-a300-490717ef9d80', CAST(N'2019-04-26T12:10:23.807' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (9, 26, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate', N'a06cdf30-972a-46ce-a2f1-5fa8917fc1ba', CAST(N'2019-04-26T12:10:23.807' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (10, 26, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate', N'11999574-a438-4fb9-8959-c76045374a4f', CAST(N'2019-04-26T12:10:23.807' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (11, 27, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate', N'9470509f-2097-4dd3-a9cb-c6d18b86b602', CAST(N'2019-04-26T12:10:23.807' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (12, 27, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate', N'aa5ceed5-9c59-4755-8a04-d991e5c0b103', CAST(N'2019-04-26T12:10:23.807' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (13, 28, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo', N'8bb9e6ff-ade7-404d-9f79-7807d6ff4b4c', CAST(N'2019-04-26T12:10:23.807' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (14, 28, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo', N'e392b5f9-4ad0-4b0d-968b-a6dea76fe8b6', CAST(N'2019-04-26T12:10:23.807' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (15, 29, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo', N'9e511a81-2288-4591-80b6-25c7b0769578', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (16, 29, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo', N'3bfbe292-5350-4209-8d95-111a4b983e26', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (17, 30, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo', N'de9f1234-d983-4e59-bd6d-fb0621d1d3ab', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (18, 30, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo', N'7971905a-7ca7-4a79-96b4-45f40c6459a2', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (19, 31, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo', N'd31a9e15-0293-49ca-96d2-f6248917ea2a', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (20, 31, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo', N'a9fa8561-87ae-4261-87d0-a28aa2f6ab6b', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (21, 32, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7f82eab4-ea51-4179-a0cd-63cb27ad0be6', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (22, 32, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo', N'a34ad970-0a34-4c66-8aa4-551624e049b6', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (23, 33, N'FromDate', N'Date', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'489ca63a-d39b-42c5-952c-5514eaa3720d', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (24, 33, N'ToDate', N'Date', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'a94a756b-2bbe-48a0-9a96-098e64b5bab0', CAST(N'2019-04-26T12:10:23.810' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (25, 36, N'from', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ccc7feaf-33b6-4512-8f56-382337f1dfd7', CAST(N'2019-04-28T10:11:32.503' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (26, 36, N'to', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate', N'65b19b0f-d6c3-433c-9624-96aa1fe5ee9b', CAST(N'2019-04-28T10:11:32.503' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (27, 34, N'VoucherId', N'id', N'EQ', N'System.Int32', N'TextBox', N'VoucherId', NULL, NULL, NULL, N'VoucherId', N'a9a5032c-9a9f-4937-a49d-331a5438d8e3', CAST(N'2019-05-04T19:04:05.177' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (28, 22, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (29, 23, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (30, 24, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (31, 25, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (32, 26, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (33, 27, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (34, 28, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (35, 29, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (36, 30, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (37, 31, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (38, 32, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (39, 37, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate', N'b8b827a7-69d7-40a2-9fff-c63efeef059f', CAST(N'2019-04-26T12:10:23.803' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (40, 37, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate', N'544b8a44-8ff7-4554-823a-b1830e99eaa2', CAST(N'2019-04-26T12:10:23.803' AS DateTime))
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate]) VALUES (41, 37, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus', N'3f722615-4f14-4e64-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))
GO
SET IDENTITY_INSERT [Reporting].[Parameter] OFF
GO

--create report parameters


--quick report template
update [Reporting].[Report] set IsSystem = 0 where [ReportID] = 43
update  [Reporting].[LocalReport] set Template = N'{   "ReportVersion": "2019.2.3",   "ReportGuid": "844e3f8b66544887a8f4da4dff2f1e34",   "ReportName": "Report",   "ReportAlias": "Report",   "ReportFile": "Report.mrt",   "ReportCreated": "/Date(1558362229000+0430)/",   "ReportChanged": "/Date(1558653437000+0430)/",   "EngineVersion": "EngineV2",   "CalculationMode": "Interpretation",   "ReportUnit": "Inches",   "PreviewSettings": 268435455,   "Styles": {     "0": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان گزارش",       "Name": "Tadbir_ReportTitle",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;14.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:255,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "1": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "پاورقی گزارش",       "Name": "Tadbir_ReportFooter",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "2": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان صفحه",       "Name": "Tadbir_PageHeader",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;14.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "3": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "پاورقی صفحه",       "Name": "Tadbir_PageFooter",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;11.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseBorderSidesFromLocation": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "4": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان پارامتر ها",       "Name": "Tadbir_ParameterTitle",       "HorAlignment": "Right",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "5": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "مقدار پارامتر ها",       "Name": "Tadbir_ParameterValue",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:139,69,19",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "6": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان ستون ها",       "Name": "Tadbir_ColumnHeaders",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": "All;155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:211,211,211",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "7": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "ستون ها اطلاعاتی",       "Name": "Tadbir_ColumnData",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:Transparent",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "8": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "شماره صفحه",       "Name": "Tadbir_PageNumber",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;9.75;;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     }   },   "Dictionary": {     "Variables": {       "0": {         "Name": "vReportTitle",         "Alias": "عنوان گزارش",         "Type": "System.String"       },       "1": {         "Name": "vReportFirstTitle",         "Alias": "عنوان ابتدایی گزارش",         "Type": "System.String"       },       "2": {         "Name": "vReportSummaryTitle",         "Alias": "متن پاورقی گزارش",         "Type": "System.String"       }     }   },   "Pages": {     "0": {       "Ident": "StiPage",       "Name": "Page1",       "Guid": "19d54b1fd3494d4f9caefe40a5cfde4b",       "Interaction": {         "Ident": "StiInteraction"       },       "Border": ";;2;;;;;solid:Black",       "Brush": "solid:Transparent",       "Components": {         "0": {           "Ident": "StiReportTitleBand",           "Name": "ReportTitle",           "ClientRectangle": "0,0.2,7.49,0.4",           "Alias": "بخش عنوان گزارش - نمایش در صفحه اول گزارش",           "Enabled": false,           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtReportHeader",               "Guid": "f67cff7056872b69bf5946e8a49b65d4",               "ClientRectangle": "2.8,0,2.1,0.3",               "ComponentStyle": "Tadbir_PageNumber",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportFirstTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;9.75;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "1": {           "Ident": "StiPageFooterBand",           "Name": "PageFooter",           "ClientRectangle": "0,11.29,7.49,0.4",           "Alias": "بخش فوتر صفحه - نمایش در همه صفحات گزارش",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtPageNumber",               "Guid": "591b0a4d9e59c4fd83ad1dbd4588ee4e",               "ClientRectangle": "3.2,-0.02,1.3,0.3",               "Alias": "شماره صفحه",               "ComponentStyle": "Tadbir_PageNumber",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{PageNumber}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;9.75;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "Type": "Expression"             }           }         },         "2": {           "Ident": "StiPageHeaderBand",           "Name": "PageHeader",           "ClientRectangle": "0,1,7.49,0.8",           "Alias": "بخش عنوان صفحه - نمایش در همه صفحات گزارش",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtPageHeader",               "Guid": "557c3f85101d4160a57980988cfc1cc1",               "ClientRectangle": "2.9,0.1,2.1,0.5",               "ComponentStyle": "Tadbir_ReportTitle",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;14.25;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:255,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "3": {           "Ident": "StiTable",           "Name": "Table1",           "ClientRectangle": "0,2.2,7.49,0.3",           "Interaction": {             "Ident": "StiBandInteraction"           },           "Border": ";;;None;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiTableCell",               "Name": "Table1_Cell1",               "Guid": "bc5fa7bb16d255d99db5551b5fec13e6",               "ClientRectangle": "0,0,1.9,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterTitle",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "از تاریخ"               },               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Margins": {                 "Left": 5,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 0             },             "1": {               "Ident": "StiTableCell",               "Name": "Table1_Cell2",               "Guid": "253ef13ce543bbad6a0da14fa476ad19",               "ClientRectangle": "1.9,0,1.9,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterValue",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "1397/01/01"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:139,69,19",               "TextOptions": {                 "RightToLeft": true               },               "Margins": {                 "Left": 9,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 1             },             "2": {               "Ident": "StiTableCell",               "Name": "Table1_Cell3",               "Guid": "38081d6522a5daa7f6fd1b1fbd5ece5f",               "ClientRectangle": "3.8,0,1.8,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterTitle",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "تا تاریخ"               },               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 2             },             "3": {               "Ident": "StiTableCell",               "Name": "Table1_Cell4",               "Guid": "3a930c25fea4cb09525477618d29cd96",               "ClientRectangle": "5.6,0,1.8,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterValue",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "1397/05/05"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:139,69,19",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 3             }           },           "MinHeight": 0.1,           "AutoWidth": "Page",           "RowCount": 1,           "ColumnCount": 4,           "NumberID": 25         },         "4": {           "Ident": "StiColumnHeaderBand",           "Name": "ColumnHeaderBand",           "CanShrink": true,           "ClientRectangle": "0,2.9,7.49,0.4",           "Alias": "بخش عناوین ستون ها",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtColumnHeader",               "Guid": "10214f235f3f47c399dfd5a98a68f584",               "CanShrink": true,               "CanGrow": true,               "ClientRectangle": "3.1,0,1.5,0.4",               "ComponentStyle": "Tadbir_ColumnHeaders",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "عنوان ستون"               },               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": "All;155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:211,211,211",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "5": {           "Ident": "StiDataBand",           "Name": "DataBand1",           "CanShrink": true,           "ClientRectangle": "0,3.7,7.49,0.3",           "Alias": "بخش دیتا یا رکورد های اطلاعاتی",           "Interaction": {             "Ident": "StiBandInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtColumnData",               "Guid": "3eb3c72204f94fb4a2508a352f28bae1",               "CanShrink": true,               "CanGrow": true,               "ClientRectangle": "3.1,0,1.5,0.3",               "ComponentStyle": "Tadbir_ColumnData",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "محتوای اطلاعاتی ستون"               },               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "6": {           "Ident": "StiReportSummaryBand",           "Name": "ReportSummary",           "ClientRectangle": "0,4.4,7.49,0.3",           "Alias": "بخش فوتر گزارش - نمایش در صفحه آخر گزارش",           "Enabled": false,           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtReportFooter",               "Guid": "f9efae84676440e7bae58c451dec3b9c",               "ClientRectangle": "2.5,0,2.6,0.3",               "ComponentStyle": "Tadbir_ReportFooter",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportSummaryTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         }       },       "PaperSize": "A4",       "TitleBeforeHeader": true,       "PageWidth": 8.27,       "PageHeight": 11.69,       "Watermark": {         "TextBrush": "solid:50,0,0,0"       },       "Margins": {         "Left": 0.39,         "Right": 0.39,         "Top": 0,         "Bottom": 0       }     }   } }' where [ReportID] = 43 and LocaleID = 2


Go

update  [Reporting].[LocalReport] set Template = N'{   "ReportVersion": "2019.2.3",   "ReportGuid": "844e3f8b66544887a8f4da4dff2f1e34",   "ReportName": "Report",   "ReportAlias": "Report",   "ReportFile": "Report.mrt",   "ReportCreated": "/Date(1558362229000+0430)/",   "ReportChanged": "/Date(1558653437000+0430)/",   "EngineVersion": "EngineV2",   "CalculationMode": "Interpretation",   "ReportUnit": "Inches",   "PreviewSettings": 268435455,   "Styles": {     "0": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان گزارش",       "Name": "Tadbir_ReportTitle",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;14.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:255,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "1": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "پاورقی گزارش",       "Name": "Tadbir_ReportFooter",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "2": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان صفحه",       "Name": "Tadbir_PageHeader",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;14.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "3": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "پاورقی صفحه",       "Name": "Tadbir_PageFooter",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;11.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseBorderSidesFromLocation": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "4": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان پارامتر ها",       "Name": "Tadbir_ParameterTitle",       "HorAlignment": "Right",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "5": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "مقدار پارامتر ها",       "Name": "Tadbir_ParameterValue",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:139,69,19",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "6": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان ستون ها",       "Name": "Tadbir_ColumnHeaders",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": "All;155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:211,211,211",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "7": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "ستون ها اطلاعاتی",       "Name": "Tadbir_ColumnData",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:Transparent",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "8": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "شماره صفحه",       "Name": "Tadbir_PageNumber",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;9.75;;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     }   },   "Dictionary": {     "Variables": {       "0": {         "Name": "vReportTitle",         "Alias": "عنوان گزارش",         "Type": "System.String"       },       "1": {         "Name": "vReportFirstTitle",         "Alias": "عنوان ابتدایی گزارش",         "Type": "System.String"       },       "2": {         "Name": "vReportSummaryTitle",         "Alias": "متن پاورقی گزارش",         "Type": "System.String"       }     }   },   "Pages": {     "0": {       "Ident": "StiPage",       "Name": "Page1",       "Guid": "19d54b1fd3494d4f9caefe40a5cfde4b",       "Interaction": {         "Ident": "StiInteraction"       },       "Border": ";;2;;;;;solid:Black",       "Brush": "solid:Transparent",       "Components": {         "0": {           "Ident": "StiReportTitleBand",           "Name": "ReportTitle",           "ClientRectangle": "0,0.2,7.49,0.4",           "Alias": "بخش عنوان گزارش - نمایش در صفحه اول گزارش",           "Enabled": false,           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtReportHeader",               "Guid": "f67cff7056872b69bf5946e8a49b65d4",               "ClientRectangle": "2.8,0,2.1,0.3",               "ComponentStyle": "Tadbir_PageNumber",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportFirstTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;9.75;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "1": {           "Ident": "StiPageFooterBand",           "Name": "PageFooter",           "ClientRectangle": "0,11.29,7.49,0.4",           "Alias": "بخش فوتر صفحه - نمایش در همه صفحات گزارش",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtPageNumber",               "Guid": "591b0a4d9e59c4fd83ad1dbd4588ee4e",               "ClientRectangle": "3.2,-0.02,1.3,0.3",               "Alias": "شماره صفحه",               "ComponentStyle": "Tadbir_PageNumber",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{PageNumber}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;9.75;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "Type": "Expression"             }           }         },         "2": {           "Ident": "StiPageHeaderBand",           "Name": "PageHeader",           "ClientRectangle": "0,1,7.49,0.8",           "Alias": "بخش عنوان صفحه - نمایش در همه صفحات گزارش",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtPageHeader",               "Guid": "557c3f85101d4160a57980988cfc1cc1",               "ClientRectangle": "2.9,0.1,2.1,0.5",               "ComponentStyle": "Tadbir_ReportTitle",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;14.25;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:255,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "3": {           "Ident": "StiTable",           "Name": "Table1",           "ClientRectangle": "0,2.2,7.49,0.3",           "Interaction": {             "Ident": "StiBandInteraction"           },           "Border": ";;;None;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiTableCell",               "Name": "Table1_Cell1",               "Guid": "bc5fa7bb16d255d99db5551b5fec13e6",               "ClientRectangle": "0,0,1.9,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterTitle",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "از تاریخ"               },               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Margins": {                 "Left": 5,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 0             },             "1": {               "Ident": "StiTableCell",               "Name": "Table1_Cell2",               "Guid": "253ef13ce543bbad6a0da14fa476ad19",               "ClientRectangle": "1.9,0,1.9,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterValue",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "1397/01/01"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:139,69,19",               "TextOptions": {                 "RightToLeft": true               },               "Margins": {                 "Left": 9,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 1             },             "2": {               "Ident": "StiTableCell",               "Name": "Table1_Cell3",               "Guid": "38081d6522a5daa7f6fd1b1fbd5ece5f",               "ClientRectangle": "3.8,0,1.8,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterTitle",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "تا تاریخ"               },               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 2             },             "3": {               "Ident": "StiTableCell",               "Name": "Table1_Cell4",               "Guid": "3a930c25fea4cb09525477618d29cd96",               "ClientRectangle": "5.6,0,1.8,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterValue",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "1397/05/05"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:139,69,19",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 3             }           },           "MinHeight": 0.1,           "AutoWidth": "Page",           "RowCount": 1,           "ColumnCount": 4,           "NumberID": 25         },         "4": {           "Ident": "StiColumnHeaderBand",           "Name": "ColumnHeaderBand",           "CanShrink": true,           "ClientRectangle": "0,2.9,7.49,0.4",           "Alias": "بخش عناوین ستون ها",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtColumnHeader",               "Guid": "10214f235f3f47c399dfd5a98a68f584",               "CanShrink": true,               "CanGrow": true,               "ClientRectangle": "3.1,0,1.5,0.4",               "ComponentStyle": "Tadbir_ColumnHeaders",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "عنوان ستون"               },               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": "All;155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:211,211,211",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "5": {           "Ident": "StiDataBand",           "Name": "DataBand1",           "CanShrink": true,           "ClientRectangle": "0,3.7,7.49,0.3",           "Alias": "بخش دیتا یا رکورد های اطلاعاتی",           "Interaction": {             "Ident": "StiBandInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtColumnData",               "Guid": "3eb3c72204f94fb4a2508a352f28bae1",               "CanShrink": true,               "CanGrow": true,               "ClientRectangle": "3.1,0,1.5,0.3",               "ComponentStyle": "Tadbir_ColumnData",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "محتوای اطلاعاتی ستون"               },               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "6": {           "Ident": "StiReportSummaryBand",           "Name": "ReportSummary",           "ClientRectangle": "0,4.4,7.49,0.3",           "Alias": "بخش فوتر گزارش - نمایش در صفحه آخر گزارش",           "Enabled": false,           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtReportFooter",               "Guid": "f9efae84676440e7bae58c451dec3b9c",               "ClientRectangle": "2.5,0,2.6,0.3",               "ComponentStyle": "Tadbir_ReportFooter",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportSummaryTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         }       },       "PaperSize": "A4",       "TitleBeforeHeader": true,       "PageWidth": 8.27,       "PageHeight": 11.69,       "Watermark": {         "TextBrush": "solid:50,0,0,0"       },       "Margins": {         "Left": 0.39,         "Right": 0.39,         "Top": 0,         "Bottom": 0       }     }   } }' 
where [ReportID] = 43 and LocaleID = 1

Go

SET IDENTITY_INSERT [Reporting].[Parameter] On

INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], 
[DescriptionKey], [rowguid], [ModifiedDate]) VALUES (42, 40, N'id', N'id', N'EQ', N'System.Int32', N'TextBox', N'VoucherId', NULL, NULL, NULL, 
N'VoucherId', N'3f722611-4f14-4e62-bea9-6f9478ef2ce0', CAST(N'2019-05-06T15:23:45.140' AS DateTime))

SET IDENTITY_INSERT [Reporting].[Parameter] OFF


update Reporting.LocalReport set Template = N'<?xml version="1.0" encoding="utf-8" standalone="yes"?>  <StiSerializer version="1.02" type="Net" application="StiReport">    <Dictionary Ref="1" type="Dictionary" isKey="true">      <BusinessObjects isList="true" count="0" />      <Databases isList="true" count="1">        <Vouchers Ref="2" type="Stimulsoft.Report.Dictionary.StiJsonDatabase" isKey="true">          <Alias>Vouchers</Alias>          <Key />          <Name>Vouchers</Name>          <PathData />        </Vouchers>      </Databases>      <DataSources isList="true" count="1">        <VouchersStdForm Ref="3" type="DataTableSource" isKey="true">          <Alias>VouchersStdForm</Alias>          <Columns isList="true" count="5">            <value>accountFullCode,System.String</value>            <value>credit,System.Decimal</value>            <value>debit,System.Decimal</value>            <value>description,System.String</value>            <value>partialAmount,System.Decimal</value>          </Columns>          <Dictionary isRef="1" />          <Name>VouchersStdForm</Name>          <NameInSource>Vouchers.Vouchers</NameInSource>        </VouchersStdForm>      </DataSources>      <Relations isList="true" count="0" />      <Report isRef="0" />      <Resources isList="true" count="0" />      <Variables isList="true" count="5">        <value>,currentDate,currentDate,,System.String,,False,False,False,False</value>        <value>,date,date,,System.String,,False,False,False,False</value>        <value>,description,description,,System.String,,False,False,False,False</value>        <value>,id,id,,System.String,,False,False,False,False</value>        <value>,no,no,,System.String,,False,False,False,False</value>      </Variables>    </Dictionary>    <EngineVersion>EngineV2</EngineVersion>    <GlobalizationStrings isList="true" count="0" />    <Key>b59bced6d8f04f9a8e0e79062b5a3296</Key>    <MetaTags isList="true" count="0" />    <Pages isList="true" count="1">      <Page1 Ref="4" type="Page" isKey="true">        <Border>None;Black;2;Solid;False;4;Black</Border>        <Brush>Transparent</Brush>        <Components isList="true" count="22">          <PageHeaderBand1 Ref="5" type="PageHeaderBand" isKey="true">            <Brush>Transparent</Brush>            <CanGrow>False</CanGrow>            <CanShrink>True</CanShrink>            <ClientRectangle>0,0.2,8.07,1.1</ClientRectangle>            <Components isList="true" count="10">              <Text1 Ref="6" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>2.6,0,2.5,0.4</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,16</Font>                <Guid>61598abee58f4786bae84d76ef0986b6</Guid>                <Margins>0,0,0,0</Margins>                <Name>Text1</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>سند حسابداری (فرم مرسوم)</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,,A=0</TextOptions>                <Type>Expression</Type>              </Text1>              <Text6 Ref="7" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>6.6,0.8,0.8,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,10</Font>                <Guid>9b3462eb08f14a83b28a0aa8538243cf</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text6</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>شماره سند :</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text6>              <Text7 Ref="8" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>4.9,0.8,0.8,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,10</Font>                <Guid>519bad8b0ab241cdb645cf73425d7f19</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text7</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>تاریخ سند :</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text7>              <Text8 Ref="9" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>6,0.81,0.7,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,10</Font>                <Guid>b4bf24897b9c4fdf8f21e3e072147e4d</Guid>                <Margins>0,0,0,0</Margins>                <Name>Text8</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>{no}</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text8>              <Text9 Ref="10" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>4.1,0.81,0.9,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,10</Font>                <Guid>bb3c8abb1ce64d9988b4339921c3cb4a</Guid>                <Linked>True</Linked>                <Margins>0,0,0,0</Margins>                <Name>Text9</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>{date}</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text9>              <Text11 Ref="11" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>6.4,0.5,1.1,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,9</Font>                <Guid>4853aea835fd418099e3bb2b08c49e5d</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text11</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>تاریخ گزارش :</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text11>              <Text12 Ref="12" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>5.7,0.53,0.9,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,9</Font>                <Guid>1bb2f39261bb49afb4efee45186f2047</Guid>                <Margins>0,0,0,0</Margins>                <Name>Text12</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>{currentDate}</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text12>              <Text26 Ref="13" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>1,0.8,0.6,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,8</Font>                <Guid>89ccf77ee3aa44258a3bd8c58d2e8122</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text26</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>شماره صفحه :</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text26>              <Text10 Ref="14" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>0.6,0.85,0.4,0.2</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,11</Font>                <Guid>e056ed4a38bc4ce48b352dc4fd10daf9</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text10</Name>                <Page isRef="4" />                <Parent isRef="5" />                <Text>{PageNumber.ToString() + "-" + TotalPageCount.ToString()}</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>              </Text10>              <HorizontalLinePrimitive1 Ref="15" type="HorizontalLinePrimitive" isKey="true">                <ClientRectangle>0.6,0.5,6.8,0.01</ClientRectangle>                <Color>Black</Color>                <EndCap Ref="16" type="Cap" isKey="true">                  <Color>Black</Color>                </EndCap>                <Guid>086eea2ac3d64b7494d8e10f08061cb7</Guid>                <Name>HorizontalLinePrimitive1</Name>                <Page isRef="4" />                <Parent isRef="5" />                <StartCap Ref="17" type="Cap" isKey="true">                  <Color>Black</Color>                </StartCap>              </HorizontalLinePrimitive1>            </Components>            <Conditions isList="true" count="0" />            <Name>PageHeaderBand1</Name>            <Page isRef="4" />            <Parent isRef="4" />          </PageHeaderBand1>          <PageFooterBand1 Ref="18" type="PageFooterBand" isKey="true">            <Brush>Transparent</Brush>            <CanGrow>False</CanGrow>            <ClientRectangle>0,10.79,8.07,0.7</ClientRectangle>            <Components isList="true" count="5">              <Text3 Ref="19" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>5.7,-0.01,1.4,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,9</Font>                <Guid>a168af00042a4de0a9eaa1fa32f5e14a</Guid>                <Margins>0,0,0,0</Margins>                <Name>Text3</Name>                <Page isRef="4" />                <Parent isRef="18" />                <Text>شرح سند خرید و فروش کالا</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text3>              <Text5 Ref="20" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>0.3,-0.01,5.4,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,9</Font>                <Guid>4b0a8ed9957d496fa0229f1354480589</Guid>                <Margins>0,0,0,0</Margins>                <Name>Text5</Name>                <Page isRef="4" />                <Parent isRef="18" />                <Text>{description}</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text5>              <Text16 Ref="21" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>6.1,0.29,1,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,8</Font>                <Guid>807837d02a1a44e1bb18b2fd6027cac9</Guid>                <Margins>0,0,0,0</Margins>                <Name>Text16</Name>                <Page isRef="4" />                <Parent isRef="18" />                <Text>تهیه کننده سند :</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text16>              <Text17 Ref="22" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>3.2,0.29,1,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,8</Font>                <Guid>f7660f3711a544fd91c3c713040a2bd5</Guid>                <Margins>0,0,0,0</Margins>                <Name>Text17</Name>                <Page isRef="4" />                <Parent isRef="18" />                <Text>مدیر امور مالی :</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text17>              <Text18 Ref="23" type="Text" isKey="true">                <Brush>Transparent</Brush>                <ClientRectangle>0.4,0.29,0.9,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,8</Font>                <Guid>8113c969ce5a4eb7b334c252ccabe4a5</Guid>                <Margins>0,0,0,0</Margins>                <Name>Text18</Name>                <Page isRef="4" />                <Parent isRef="18" />                <Text>مدیر عامل :</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text18>            </Components>            <Conditions isList="true" count="0" />            <Name>PageFooterBand1</Name>            <Page isRef="4" />            <Parent isRef="4" />          </PageFooterBand1>          <DataVouchers_id Ref="24" type="Text" isKey="true">            <Brush>Transparent</Brush>            <CanGrow>True</CanGrow>            <ClientRectangle>8.7,0.4,0.7,0.3</ClientRectangle>            <Conditions isList="true" count="0" />            <Font>B Zar,8</Font>            <HorAlignment>Center</HorAlignment>            <Margins>0,0,0,0</Margins>            <Name>DataVouchers_id</Name>            <Page isRef="4" />            <Parent isRef="4" />            <Text>{Vouchers.id}</Text>            <TextBrush>Black</TextBrush>            <TextOptions>,,,,WordWrap=True,A=0</TextOptions>            <VertAlignment>Center</VertAlignment>          </DataVouchers_id>          <ColumnHeaderBand1 Ref="25" type="Stimulsoft.Report.Components.StiColumnHeaderBand" isKey="true">            <Brush>Transparent</Brush>            <ClientRectangle>0,1.7,8.07,0.3</ClientRectangle>            <Components isList="true" count="19">              <Text2 Ref="26" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>4.9,0,0.6,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,12</Font>                <Guid>903ebd75f2ad48eabcbac45411e6b772</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text2</Name>                <Page isRef="4" />                <Parent isRef="25" />                <Text>شرح</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text2>              <Text4 Ref="27" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>0.7,0,0.9,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,12</Font>                <Guid>0b33e32cceff4f7e8911431ff9fb46f9</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text4</Name>                <Page isRef="4" />                <Parent isRef="25" />                <Text>بستانکار</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text4>              <Text13 Ref="28" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>6.5,0,0.9,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,12</Font>                <Guid>95d746759f5a4a1ea90fd0c85cef280b</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text13</Name>                <Page isRef="4" />                <Parent isRef="25" />                <Text>کد حساب</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text13>              <Text15 Ref="29" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>2.9,0,1,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,12</Font>                <Guid>c411bb1be6a841849d811e63b8fb7889</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text15</Name>                <Page isRef="4" />                <Parent isRef="25" />                <Text>مبلغ جزء</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text15>              <Text14 Ref="30" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>1.8,0,0.9,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,12</Font>                <Guid>cf4339f31d404e0c9683f067b9818406</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text14</Name>                <Page isRef="4" />                <Parent isRef="25" />                <Text>بدهکار</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text14>              <HorizontalLinePrimitive4 Ref="31" type="HorizontalLinePrimitive" isKey="true">                <ClientRectangle>0.6,0,6.8,0.01</ClientRectangle>                <Color>Black</Color>                <EndCap Ref="32" type="Cap" isKey="true">                  <Color>Black</Color>                </EndCap>                <Guid>94d940813f7b45b3b99e62f82f6de1b5</Guid>                <Name>HorizontalLinePrimitive4</Name>                <Page isRef="4" />                <Parent isRef="25" />                <StartCap Ref="33" type="Cap" isKey="true">                  <Color>Black</Color>                </StartCap>              </HorizontalLinePrimitive4>              <HorizontalLinePrimitive2 Ref="34" type="HorizontalLinePrimitive" isKey="true">                <ClientRectangle>0.6,0.3,6.8,0.01</ClientRectangle>                <Color>Black</Color>                <EndCap Ref="35" type="Cap" isKey="true">                  <Color>Black</Color>                </EndCap>                <Guid>dff7fb07183d466c855c3941da6b4fcf</Guid>                <Name>HorizontalLinePrimitive2</Name>                <Page isRef="4" />                <Parent isRef="25" />                <StartCap Ref="36" type="Cap" isKey="true">                  <Color>Black</Color>                </StartCap>              </HorizontalLinePrimitive2>              <StartPointPrimitive1 Ref="37" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>6.5,0,0,0</ClientRectangle>                <Name>StartPointPrimitive1</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>2c6ebb0fc3254a1c934234d94c2b5057</ReferenceToGuid>              </StartPointPrimitive1>              <EndPointPrimitive1 Ref="38" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>6.3,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive1</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>2c6ebb0fc3254a1c934234d94c2b5057</ReferenceToGuid>              </EndPointPrimitive1>              <StartPointPrimitive2 Ref="39" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>7.4,0,0,0</ClientRectangle>                <Name>StartPointPrimitive2</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>ac1b087b702c4406816d56d2370a52c9</ReferenceToGuid>              </StartPointPrimitive2>              <EndPointPrimitive2 Ref="40" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>7.2,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive2</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>ac1b087b702c4406816d56d2370a52c9</ReferenceToGuid>              </EndPointPrimitive2>              <StartPointPrimitive3 Ref="41" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>3.9,0,0,0</ClientRectangle>                <Name>StartPointPrimitive3</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>52b0aa0f1f004274aa63df0bf07eecb6</ReferenceToGuid>              </StartPointPrimitive3>              <EndPointPrimitive3 Ref="42" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>3.7,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive3</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>52b0aa0f1f004274aa63df0bf07eecb6</ReferenceToGuid>              </EndPointPrimitive3>              <StartPointPrimitive4 Ref="43" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>2.8,0,0,0</ClientRectangle>                <Name>StartPointPrimitive4</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>8f77d8139c7c4d8999cf3d92b3f8d27e</ReferenceToGuid>              </StartPointPrimitive4>              <EndPointPrimitive4 Ref="44" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>2.6,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive4</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>8f77d8139c7c4d8999cf3d92b3f8d27e</ReferenceToGuid>              </EndPointPrimitive4>              <StartPointPrimitive5 Ref="45" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>1.7,0,0,0</ClientRectangle>                <Name>StartPointPrimitive5</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>7bae1ba5ede64ec3967c93671ebb1dfb</ReferenceToGuid>              </StartPointPrimitive5>              <EndPointPrimitive5 Ref="46" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>1.5,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive5</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>7bae1ba5ede64ec3967c93671ebb1dfb</ReferenceToGuid>              </EndPointPrimitive5>              <StartPointPrimitive6 Ref="47" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>0.6,0,0,0</ClientRectangle>                <Name>StartPointPrimitive6</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>7d92dea8f4344eb4a08c5839fb7b6ef9</ReferenceToGuid>              </StartPointPrimitive6>              <EndPointPrimitive6 Ref="48" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>0.4,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive6</Name>                <Page isRef="4" />                <Parent isRef="25" />                <ReferenceToGuid>7d92dea8f4344eb4a08c5839fb7b6ef9</ReferenceToGuid>              </EndPointPrimitive6>            </Components>            <Conditions isList="true" count="0" />            <Name>ColumnHeaderBand1</Name>            <Page isRef="4" />            <Parent isRef="4" />          </ColumnHeaderBand1>          <DataVouchers Ref="49" type="DataBand" isKey="true">            <Brush>Transparent</Brush>            <BusinessObjectGuid isNull="true" />            <ClientRectangle>0,2.4,8.07,0.3</ClientRectangle>            <Components isList="true" count="18">              <DataVouchers_date Ref="50" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>3.95,0,2.49,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,8</Font>                <Margins>0,0,0,0</Margins>                <Name>DataVouchers_date</Name>                <Page isRef="4" />                <Parent isRef="49" />                <Text>{VouchersStdForm.description}</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,RightToLeft=True,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </DataVouchers_date>              <DataVouchers_statusName Ref="51" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>0.65,0,1,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,8</Font>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>DataVouchers_statusName</Name>                <Page isRef="4" />                <Parent isRef="49" />                <Text>{VouchersStdForm.credit}</Text>                <TextBrush>Black</TextBrush>                <TextFormat Ref="52" type="NumberFormat" isKey="true">                  <DecimalDigits>0</DecimalDigits>                  <GroupSeparator>,</GroupSeparator>                  <NegativePattern>1</NegativePattern>                  <State>DecimalDigits, GroupSeparator, GroupSize</State>                </TextFormat>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </DataVouchers_statusName>              <Text19 Ref="53" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>2.75,0,1.1,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,8</Font>                <Guid>fe3c47eee3554b4e9df93ac86601ed80</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text19</Name>                <Page isRef="4" />                <Parent isRef="49" />                <Text>{VouchersStdForm.partialAmount}</Text>                <TextBrush>Black</TextBrush>                <TextFormat Ref="54" type="NumberFormat" isKey="true">                  <DecimalDigits>0</DecimalDigits>                  <GroupSeparator>,</GroupSeparator>                  <NegativePattern>1</NegativePattern>                  <State>DecimalDigits, GroupSeparator, GroupSize</State>                </TextFormat>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text19>              <Text20 Ref="55" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>1.8,0,0.9,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,8</Font>                <Guid>20293cc6e5bd42c2938f32a4668064ae</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text20</Name>                <Page isRef="4" />                <Parent isRef="49" />                <Text>{VouchersStdForm.debit}</Text>                <TextBrush>Black</TextBrush>                <TextFormat Ref="56" type="NumberFormat" isKey="true">                  <DecimalDigits>0</DecimalDigits>                  <GroupSeparator>,</GroupSeparator>                  <NegativePattern>1</NegativePattern>                  <State>DecimalDigits, GroupSeparator, GroupSize</State>                </TextFormat>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text20>              <Text21 Ref="57" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>6.6,0,0.69,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,8</Font>                <Guid>98bc4d90eeb54b549da14d4b841426c7</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text21</Name>                <Page isRef="4" />                <Parent isRef="49" />                <Text>{VouchersStdForm.accountFullCode}</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text21>              <StartPointPrimitive10 Ref="58" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>7.4,0,0,0</ClientRectangle>                <Name>StartPointPrimitive10</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>4a8f90e298a94f9eaf66671abcea1f57</ReferenceToGuid>              </StartPointPrimitive10>              <EndPointPrimitive10 Ref="59" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>7.2,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive10</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>4a8f90e298a94f9eaf66671abcea1f57</ReferenceToGuid>              </EndPointPrimitive10>              <HorizontalLinePrimitive3 Ref="60" type="HorizontalLinePrimitive" isKey="true">                <ClientRectangle>0.6,0.3,6.8,0.01</ClientRectangle>                <Color>Black</Color>                <EndCap Ref="61" type="Cap" isKey="true">                  <Color>Black</Color>                </EndCap>                <Guid>42ae6dd47770407db103ee1e07c89d69</Guid>                <Name>HorizontalLinePrimitive3</Name>                <Page isRef="4" />                <Parent isRef="49" />                <StartCap Ref="62" type="Cap" isKey="true">                  <Color>Black</Color>                </StartCap>              </HorizontalLinePrimitive3>              <StartPointPrimitive11 Ref="63" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>0.6,0,0,0</ClientRectangle>                <Name>StartPointPrimitive11</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>d8f54115cef04c8799b3aef6369f7b84</ReferenceToGuid>              </StartPointPrimitive11>              <EndPointPrimitive11 Ref="64" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>0.4,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive11</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>d8f54115cef04c8799b3aef6369f7b84</ReferenceToGuid>              </EndPointPrimitive11>              <StartPointPrimitive12 Ref="65" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>3.9,0,0,0</ClientRectangle>                <Name>StartPointPrimitive12</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>a3850fac806a4fe49c15a9772ddfc84a</ReferenceToGuid>              </StartPointPrimitive12>              <EndPointPrimitive12 Ref="66" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>3.7,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive12</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>a3850fac806a4fe49c15a9772ddfc84a</ReferenceToGuid>              </EndPointPrimitive12>              <StartPointPrimitive13 Ref="67" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>2.8,0,0,0</ClientRectangle>                <Name>StartPointPrimitive13</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>59fe0656965b473fb63d09f8c56869e7</ReferenceToGuid>              </StartPointPrimitive13>              <EndPointPrimitive13 Ref="68" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>2.6,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive13</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>59fe0656965b473fb63d09f8c56869e7</ReferenceToGuid>              </EndPointPrimitive13>              <StartPointPrimitive15 Ref="69" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>1.7,0,0,0</ClientRectangle>                <Name>StartPointPrimitive15</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>50e3f7e7d121482abd18a2481cf4ccdd</ReferenceToGuid>              </StartPointPrimitive15>              <EndPointPrimitive15 Ref="70" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>1.5,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive15</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>50e3f7e7d121482abd18a2481cf4ccdd</ReferenceToGuid>              </EndPointPrimitive15>              <StartPointPrimitive16 Ref="71" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>6.5,0,0,0</ClientRectangle>                <Name>StartPointPrimitive16</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>814789f33d1b40c7a3a16286942090f2</ReferenceToGuid>              </StartPointPrimitive16>              <EndPointPrimitive16 Ref="72" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>6.3,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive16</Name>                <Page isRef="4" />                <Parent isRef="49" />                <ReferenceToGuid>814789f33d1b40c7a3a16286942090f2</ReferenceToGuid>              </EndPointPrimitive16>            </Components>            <Conditions isList="true" count="0" />            <DataSourceName>VouchersStdForm</DataSourceName>            <Filters isList="true" count="0" />            <Linked>True</Linked>            <Name>DataVouchers</Name>            <Page isRef="4" />            <Parent isRef="4" />            <RightToLeft>True</RightToLeft>            <Sort isList="true" count="0" />          </DataVouchers>          <ColumnFooterBand1 Ref="73" type="Stimulsoft.Report.Components.StiColumnFooterBand" isKey="true">            <Brush>Transparent</Brush>            <ClientRectangle>0,3.1,8.07,0.3</ClientRectangle>            <Components isList="true" count="12">              <Text22 Ref="74" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>2.8,0,0.5,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Titr,12</Font>                <Guid>e8154825486744ddbc192d023828f61b</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text22</Name>                <Page isRef="4" />                <Parent isRef="73" />                <Text>جمع</Text>                <TextBrush>Black</TextBrush>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Expression</Type>                <VertAlignment>Center</VertAlignment>              </Text22>              <Text24 Ref="75" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>1.7,0,1.1,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,8,Bold</Font>                <Guid>1c034e68d16c4fd596ad0c64e72ce41c</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text24</Name>                <Page isRef="4" />                <Parent isRef="73" />                <Text>{Sum(DataVouchers,VouchersStdForm.debit)}</Text>                <TextBrush>Black</TextBrush>                <TextFormat Ref="76" type="NumberFormat" isKey="true">                  <DecimalDigits>0</DecimalDigits>                  <GroupSeparator>,</GroupSeparator>                  <NegativePattern>1</NegativePattern>                  <State>DecimalDigits, GroupSeparator, GroupSize</State>                </TextFormat>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Totals</Type>                <VertAlignment>Center</VertAlignment>              </Text24>              <Text25 Ref="77" type="Text" isKey="true">                <Brush>Transparent</Brush>                <CanGrow>True</CanGrow>                <ClientRectangle>0.6,0,1.1,0.3</ClientRectangle>                <Conditions isList="true" count="0" />                <Font>B Zar,8,Bold</Font>                <Guid>a91502d4d0c8468182e66a5645e58e35</Guid>                <HorAlignment>Center</HorAlignment>                <Margins>0,0,0,0</Margins>                <Name>Text25</Name>                <Page isRef="4" />                <Parent isRef="73" />                <Text>{Sum(DataVouchers,VouchersStdForm.credit)}</Text>                <TextBrush>Black</TextBrush>                <TextFormat Ref="78" type="NumberFormat" isKey="true">                  <DecimalDigits>0</DecimalDigits>                  <GroupSeparator>,</GroupSeparator>                  <NegativePattern>1</NegativePattern>                  <State>DecimalDigits, GroupSeparator, GroupSize</State>                </TextFormat>                <TextOptions>,,,,WordWrap=True,A=0</TextOptions>                <Type>Totals</Type>                <VertAlignment>Center</VertAlignment>              </Text25>              <HorizontalLinePrimitive5 Ref="79" type="HorizontalLinePrimitive" isKey="true">                <ClientRectangle>0.6,0.3,6.8,0.01</ClientRectangle>                <Color>Black</Color>                <EndCap Ref="80" type="Cap" isKey="true">                  <Color>Black</Color>                </EndCap>                <Guid>c3653ea5fe30406ba17de5adcf6555e1</Guid>                <Name>HorizontalLinePrimitive5</Name>                <Page isRef="4" />                <Parent isRef="73" />                <StartCap Ref="81" type="Cap" isKey="true">                  <Color>Black</Color>                </StartCap>              </HorizontalLinePrimitive5>              <StartPointPrimitive7 Ref="82" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>1.7,0,0,0</ClientRectangle>                <Name>StartPointPrimitive7</Name>                <Page isRef="4" />                <Parent isRef="73" />                <ReferenceToGuid>7dd160aa727f45609d505c408ca39962</ReferenceToGuid>              </StartPointPrimitive7>              <EndPointPrimitive7 Ref="83" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>1.5,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive7</Name>                <Page isRef="4" />                <Parent isRef="73" />                <ReferenceToGuid>7dd160aa727f45609d505c408ca39962</ReferenceToGuid>              </EndPointPrimitive7>              <StartPointPrimitive8 Ref="84" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>2.8,0,0,0</ClientRectangle>                <Name>StartPointPrimitive8</Name>                <Page isRef="4" />                <Parent isRef="73" />                <ReferenceToGuid>6b5510863355489497a1cb082edbb95d</ReferenceToGuid>              </StartPointPrimitive8>              <EndPointPrimitive8 Ref="85" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">                <ClientRectangle>2.6,0.3,0,0</ClientRectangle>                <Name>EndPointPrimitive8</Name>                <Page isRef="4" />                <Parent isRef="73" />                <ReferenceToGuid>6b5510863355489497a1cb082edbb95d</ReferenceToGuid>              </EndPointPrimitive8>              <StartPointPrimitive9 Ref="86" type="Stimulsoft.Report.Components.StiStartPointPrimitive" isKey="true">                <ClientRectangle>7.4,0,0,0</ClientRectangle>                <Name>StartPointPrimitive9</Name>                <Page isRef="4" />                <Parent isRef="73" />                <ReferenceToGuid>f9a8f43d78a04be7899ef81b11031b90</ReferenceToGuid>              </StartPointPrimitive9>              <EndPointPrimitive9 Ref="87" type="Stimulsoft.Report.Components.StiEndPointPrimitive" isKey="true">'  where ReportID = 40 and LocaleID = 2



GO



Go

update Metadata.Command set RouteUrl = '/reports' where CommandID = 31

GO


--select * from Reporting.LocalReport where ReportID = 40

update Reporting.LocalReport set Template=N'{
  "ReportVersion": "2019.2.3.0",
  "ReportGuid": "a56d6285a5474c6b881cc91ec4d611aa",
  "ReportName": "Report",
  "ReportAlias": "Report",
  "ReportCreated": "/Date(1542053910000+0330)/",
  "ReportChanged": "/Date(1544569474000+0330)/",
  "EngineVersion": "EngineV2",
  "ReportUnit": "Inches",
  "Script": "using System;\r\nusing System.Drawing;\r\nusing System.Windows.Forms;\r\nusing System.Data;\r\nusing Stimulsoft.Controls;\r\nusing Stimulsoft.Base.Drawing;\r\nusing Stimulsoft.Report;\r\nusing Stimulsoft.Report.Dialogs;\r\nusing Stimulsoft.Report.Components;\r\n\r\nnamespace Reports\r\n{\r\n    public class Report : Stimulsoft.Report.StiReport\r\n    {\r\n        public Report()        {\r\n            this.InitializeComponent();\r\n        }\r\n\r\n        #region StiReport Designer generated code - do not modify\r\n\t\t#endregion StiReport Designer generated code - do not modify\r\n    }\r\n}\r\n",
  "ReferencedAssemblies": {
    "0": "System.Dll",
    "1": "System.Drawing.Dll",
    "2": "System.Windows.Forms.Dll",
    "3": "System.Data.Dll",
    "4": "System.Xml.Dll",
    "5": "Stimulsoft.Controls.Dll",
    "6": "Stimulsoft.Base.Dll",
    "7": "Stimulsoft.Report.Dll"
  },
  "Dictionary": {
    "Variables": {
      "0": {
        "Name": "currentDate",
        "Alias": "currentDate",
        "Type": "System.String"
      },
      "1": {
        "Name": "date",
        "Alias": "date",
        "Type": "System.String"
      },
      "2": {
        "Name": "description",
        "Alias": "description",
        "Type": "System.String"
      },
      "3": {
        "Name": "id",
        "Alias": "id",
        "Type": "System.String"
      },
      "4": {
        "Name": "no",
        "Alias": "no",
        "Type": "System.String"
      }
    },
    "DataSources": {
      "0": {
        "Ident": "StiDataTableSource",
        "Name": "VouchersStdForm",
        "Alias": "VouchersStdForm",
        "Columns": {
          "0": {
            "Name": "accountFullCode",
            "Index": -1,
            "NameInSource": "accountFullCode",
            "Alias": "accountFullCode",
            "Type": "System.String"
          },
          "1": {
            "Name": "credit",
            "Index": -1,
            "NameInSource": "credit",
            "Alias": "credit",
            "Type": "System.Decimal"
          },
          "2": {
            "Name": "debit",
            "Index": -1,
            "NameInSource": "debit",
            "Alias": "debit",
            "Type": "System.Decimal"
          },
          "3": {
            "Name": "description",
            "Index": -1,
            "NameInSource": "description",
            "Alias": "description",
            "Type": "System.String"
          },
          "4": {
            "Name": "partialAmount",
            "Index": -1,
            "NameInSource": "partialAmount",
            "Alias": "partialAmount",
            "Type": "System.Decimal"
          }
        },
        "NameInSource": "Vouchers.Vouchers"
      }
    },
    "Databases": {
      "0": {
        "Ident": "StiJsonDatabase",
        "Name": "Vouchers",
        "Alias": "Vouchers"
      }
    }
  },
  "Pages": {
    "0": {
      "Ident": "StiPage",
      "Name": "Page1",
      "Guid": "3bd92efd509e4fe9bffa5620fcd0b140",
      "Interaction": {
        "Ident": "StiInteraction"
      },
      "Border": ";;2;;;;;solid:Black",
      "Brush": "solid:",
      "Components": {
        "0": {
          "Ident": "StiPageHeaderBand",
          "Name": "PageHeaderBand1",
          "CanShrink": true,
          "ClientRectangle": "0,0.2,8.07,1.1",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text1",
              "Guid": "61598abee58f4786bae84d76ef0986b6",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.6,0,2.5,0.4",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "سند حسابداری (فرم مرسوم)"
              },
              "Font": "B Titr;16;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text6",
              "Guid": "9b3462eb08f14a83b28a0aa8538243cf",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.6,0.8,0.8,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "شماره سند :"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text7",
              "Guid": "519bad8b0ab241cdb645cf73425d7f19",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.9,0.8,0.8,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "تاریخ سند :"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "3": {
              "Ident": "StiText",
              "Name": "Text8",
              "Guid": "b4bf24897b9c4fdf8f21e3e072147e4d",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6,0.81,0.7,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{no}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "4": {
              "Ident": "StiText",
              "Name": "Text9",
              "Guid": "bb3c8abb1ce64d9988b4339921c3cb4a",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.1,0.81,0.9,0.3",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{date}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "5": {
              "Ident": "StiText",
              "Name": "Text11",
              "Guid": "4853aea835fd418099e3bb2b08c49e5d",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0.5,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "تاریخ گزارش :"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "6": {
              "Ident": "StiText",
              "Name": "Text12",
              "Guid": "1bb2f39261bb49afb4efee45186f2047",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.7,0.53,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{currentDate}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "7": {
              "Ident": "StiText",
              "Name": "Text26",
              "Guid": "89ccf77ee3aa44258a3bd8c58d2e8122",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1,0.8,0.6,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "شماره صفحه :"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "8": {
              "Ident": "StiText",
              "Name": "Text10",
              "Guid": "e056ed4a38bc4ce48b352dc4fd10daf9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.85,0.4,0.2",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{PageNumber.ToString() + \"-\" + TotalPageCount.ToString()}"
              },
              "HorAlignment": "Center",
              "Font": "B Zar;11;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "9": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive1",
              "Guid": "086eea2ac3d64b7494d8e10f08061cb7",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.5,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            }
          },
          "CanGrow": false
        },
        "1": {
          "Ident": "StiPageFooterBand",
          "Name": "PageFooterBand1",
          "ClientRectangle": "0,10.79,8.07,0.7",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text3",
              "Guid": "a168af00042a4de0a9eaa1fa32f5e14a",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.7,-0.01,1.4,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "شرح سند خرید و فروش کالا"
              },
              "VertAlignment": "Center",
              "Font": "B Titr;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text5",
              "Guid": "4b0a8ed9957d496fa0229f1354480589",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.3,-0.01,5.4,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{description}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text16",
              "Guid": "807837d02a1a44e1bb18b2fd6027cac9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.1,0.29,1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "تهیه کننده سند :"
              },
              "VertAlignment": "Center",
              "Font": "B Titr;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "3": {
              "Ident": "StiText",
              "Name": "Text17",
              "Guid": "f7660f3711a544fd91c3c713040a2bd5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.2,0.29,1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "مدیر امور مالی :"
              },
              "VertAlignment": "Center",
              "Font": "B Titr;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "4": {
              "Ident": "StiText",
              "Name": "Text18",
              "Guid": "8113c969ce5a4eb7b334c252ccabe4a5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.4,0.29,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "مدیر عامل :"
              },
              "VertAlignment": "Center",
              "Font": "B Titr;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            }
          },
          "CanGrow": false
        },
        "2": {
          "Ident": "StiText",
          "Name": "DataVouchers_id",
          "CanGrow": true,
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "8.7,0.4,0.7,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Text": {
            "Value": "{Vouchers.id}"
          },
          "HorAlignment": "Center",
          "VertAlignment": "Center",
          "Font": "B Zar;;;",
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "TextBrush": "solid:Black",
          "TextOptions": {
            "WordWrap": true
          }
        },
        "3": {
          "Ident": "StiColumnHeaderBand",
          "Name": "ColumnHeaderBand1",
          "ClientRectangle": "0,1.7,8.07,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text2",
              "Guid": "903ebd75f2ad48eabcbac45411e6b772",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.9,0,0.6,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "شرح"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text4",
              "Guid": "0b33e32cceff4f7e8911431ff9fb46f9",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.7,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "بستانکار"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text13",
              "Guid": "95d746759f5a4a1ea90fd0c85cef280b",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.5,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "کد حساب"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "3": {
              "Ident": "StiText",
              "Name": "Text15",
              "Guid": "c411bb1be6a841849d811e63b8fb7889",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.9,0,1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "مبلغ جزء"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "4": {
              "Ident": "StiText",
              "Name": "Text14",
              "Guid": "cf4339f31d404e0c9683f067b9818406",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.8,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "بدهکار"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "5": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive4",
              "Guid": "94d940813f7b45b3b99e62f82f6de1b5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "6": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive2",
              "Guid": "dff7fb07183d466c855c3941da6b4fcf",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "7": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive1",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.5,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "2c6ebb0fc3254a1c934234d94c2b5057"
            },
            "8": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive1",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.3,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "2c6ebb0fc3254a1c934234d94c2b5057"
            },
            "9": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive2",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ac1b087b702c4406816d56d2370a52c9"
            },
            "10": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive2",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.2,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ac1b087b702c4406816d56d2370a52c9"
            },
            "11": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive3",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.9,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "52b0aa0f1f004274aa63df0bf07eecb6"
            },
            "12": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive3",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.7,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "52b0aa0f1f004274aa63df0bf07eecb6"
            },
            "13": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive4",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.8,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "8f77d8139c7c4d8999cf3d92b3f8d27e"
            },
            "14": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive4",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "8f77d8139c7c4d8999cf3d92b3f8d27e"
            },
            "15": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7bae1ba5ede64ec3967c93671ebb1dfb"
            },
            "16": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.5,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7bae1ba5ede64ec3967c93671ebb1dfb"
            },
            "17": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive6",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7d92dea8f4344eb4a08c5839fb7b6ef9"
            },
            "18": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive6",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7d92dea8f4344eb4a08c5839fb7b6ef9"
            }
          }
        },
        "4": {
          "Ident": "StiDataBand",
          "Name": "DataVouchers",
          "ClientRectangle": "0,2.4,8.07,0.3",
          "Linked": true,
          "Interaction": {
            "Ident": "StiBandInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "DataVouchers_date",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.95,0,2.49,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.description}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "DataVouchers_statusName",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.65,0,1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.credit}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text19",
              "Guid": "fe3c47eee3554b4e9df93ac86601ed80",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.75,0,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.partialAmount}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Expression"
            },
            "3": {
              "Ident": "StiText",
              "Name": "Text20",
              "Guid": "20293cc6e5bd42c2938f32a4668064ae",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.8,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.debit}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Expression"
            },
            "4": {
              "Ident": "StiText",
              "Name": "Text21",
              "Guid": "98bc4d90eeb54b549da14d4b841426c7",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.6,0,0.69,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.accountFullCode}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "5": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive10",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "4a8f90e298a94f9eaf66671abcea1f57"
            },
            "6": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive10",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.2,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "4a8f90e298a94f9eaf66671abcea1f57"
            },
            "7": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive3",
              "Guid": "42ae6dd47770407db103ee1e07c89d69",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "8": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive11",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "d8f54115cef04c8799b3aef6369f7b84"
            },
            "9": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive11",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "d8f54115cef04c8799b3aef6369f7b84"
            },
            "10": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive12",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.9,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "a3850fac806a4fe49c15a9772ddfc84a"
            },
            "11": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive12",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.7,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "a3850fac806a4fe49c15a9772ddfc84a"
            },
            "12": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive13",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.8,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "59fe0656965b473fb63d09f8c56869e7"
            },
            "13": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive13",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "59fe0656965b473fb63d09f8c56869e7"
            },
            "14": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive15",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "50e3f7e7d121482abd18a2481cf4ccdd"
            },
            "15": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive15",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.5,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "50e3f7e7d121482abd18a2481cf4ccdd"
            },
            "16": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive16",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.5,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "814789f33d1b40c7a3a16286942090f2"
            },
            "17": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive16",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.3,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "814789f33d1b40c7a3a16286942090f2"
            }
          },
          "DataSourceName": "VouchersStdForm",
          "RightToLeft": true
        },
        "5": {
          "Ident": "StiColumnFooterBand",
          "Name": "ColumnFooterBand1",
          "ClientRectangle": "0,3.1,8.07,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text22",
              "Guid": "e8154825486744ddbc192d023828f61b",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.8,0,0.5,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "جمع"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text24",
              "Guid": "1c034e68d16c4fd596ad0c64e72ce41c",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{Sum(DataVouchers,VouchersStdForm.debit)}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;Bold;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Totals"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text25",
              "Guid": "a91502d4d0c8468182e66a5645e58e35",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{Sum(DataVouchers,VouchersStdForm.credit)}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;Bold;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Totals"
            },
            "3": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive5",
              "Guid": "c3653ea5fe30406ba17de5adcf6555e1",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "4": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive7",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7dd160aa727f45609d505c408ca39962"
            },
            "5": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive7",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.5,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7dd160aa727f45609d505c408ca39962"
            },
            "6": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive8",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.8,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "6b5510863355489497a1cb082edbb95d"
            },
            "7": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive8",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "6b5510863355489497a1cb082edbb95d"
            },
            "8": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "f9a8f43d78a04be7899ef81b11031b90"
            },
            "9": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.2,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "f9a8f43d78a04be7899ef81b11031b90"
            },
            "10": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive14",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ba8d15b78e1646b29b870c7a76b238b2"
            },
            "11": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive14",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ba8d15b78e1646b29b870c7a76b238b2"
            }
          }
        },
        "6": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive10",
          "Guid": "4a8f90e298a94f9eaf66671abcea1f57",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.4,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "7": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive11",
          "Guid": "d8f54115cef04c8799b3aef6369f7b84",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "0.6,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "8": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive13",
          "Guid": "a3850fac806a4fe49c15a9772ddfc84a",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "3.9,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "9": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive14",
          "Guid": "59fe0656965b473fb63d09f8c56869e7",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "2.8,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "10": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive16",
          "Guid": "50e3f7e7d121482abd18a2481cf4ccdd",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.7,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "11": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive17",
          "Guid": "814789f33d1b40c7a3a16286942090f2",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "6.5,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "12": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive1",
          "Guid": "2c6ebb0fc3254a1c934234d94c2b5057",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "6.5,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "13": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive3",
          "Guid": "ac1b087b702c4406816d56d2370a52c9",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.4,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "14": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive4",
          "Guid": "52b0aa0f1f004274aa63df0bf07eecb6",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "3.9,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "15": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive5",
          "Guid": "8f77d8139c7c4d8999cf3d92b3f8d27e",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "2.8,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "16": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive6",
          "Guid": "7bae1ba5ede64ec3967c93671ebb1dfb",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.7,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "17": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive8",
          "Guid": "7d92dea8f4344eb4a08c5839fb7b6ef9",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "0.6,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "18": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive19",
          "Guid": "7dd160aa727f45609d505c408ca39962",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.7,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "19": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive20",
          "Guid": "6b5510863355489497a1cb082edbb95d",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "2.8,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "20": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive22",
          "Guid": "f9a8f43d78a04be7899ef81b11031b90",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.4,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "21": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive23",
          "Guid": "ba8d15b78e1646b29b870c7a76b238b2",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "0.6,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        }
      },
      "PaperSize": "A4",
      "PageWidth": 8.27,
      "PageHeight": 11.69,
      "Watermark": {
        "TextBrush": "solid:50,0,0,0"
      },
      "Margins": {
        "Left": 0.1,
        "Right": 0.1,
        "Top": 0.1,
        "Bottom": 0.1
      }
    }
  }
}' where ReportID=40 and LocaleID = 2

update Reporting.Parameter set [Name]='no' , [FieldName] = 'no' , CaptionKey = 'VoucherNo' , DescriptionKey = 'VoucherNo' where ReportID = 40

update Reporting.Report set ViewID = 3,IsDefault = 1 where ReportID = 40

Go
