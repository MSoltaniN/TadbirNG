USE [TadbirSysDemo]
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

CREATE SCHEMA [WFTracking]
GO

CREATE TABLE [Metadata].[View] (
    [ViewID]                 INT              IDENTITY (1, 1) NOT NULL,
    [Name]                   VARCHAR(64)      NOT NULL,
    [IsHierarchy]            BIT              NOT NULL,
    [IsCartableIntegrated]   BIT              NOT NULL,
    [FetchUrl]               NVARCHAR(512)    NOT NULL,
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
    [Expression]       VARCHAR(64)      NULL,
    [Settings]         NVARCHAR(1024)   NULL,
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
    [Template]       NVARCHAR(MAX)    NULL,
    [TemplateLtr]    NVARCHAR(MAX)    NULL,
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

CREATE TABLE [WFTracking].[WorkflowInstanceEvent] (
	[EventID]                    INT IDENTITY(1,1) NOT NULL,
	[WorkflowInstanceId]         UNIQUEIDENTIFIER NOT NULL,
	[ActivityDefinition]         NVARCHAR(256) NULL,
	[RecordNumber]               BIGINT NOT NULL,
	[State]                      NVARCHAR(128) NULL,
	[TraceLevelId]               TINYINT NULL,
	[Reason]                     NVARCHAR(2048) NULL,
	[ExceptionDetails]           NVARCHAR(MAX) NULL,
	[SerializedAnnotations]      NVARCHAR(MAX) NULL,
	[TimeCreated]                DATETIME NOT NULL,
    [rowguid]                    UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_WorkflowInstanceEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]               DATETIME CONSTRAINT [DF_WFTracking_WorkflowInstanceEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_WFTracking_WorkflowInstanceEvent] PRIMARY KEY ([EventID]),
);
GO

CREATE TABLE [WFTracking].[ActivityInstanceEvent] (
	[EventID]               INT IDENTITY(1,1) NOT NULL,
	[WorkflowInstanceId]    UNIQUEIDENTIFIER NOT NULL,
	[RecordNumber]          BIGINT NOT NULL,
	[State]                 NVARCHAR(128) NULL,
	[TraceLevelId]          TINYINT NULL,
	[ActivityRecordType]    NVARCHAR(128) NOT NULL,
	[ActivityName]          NVARCHAR(1024) NULL,
	[ActivityId]            NVARCHAR(256) NULL,
	[ActivityInstanceId]    NVARCHAR(256) NULL,
	[ActivityType]          NVARCHAR(2048) NULL,
	[SerializedArguments]   NVARCHAR(MAX) NULL,
	[SerializedVariables]   NVARCHAR(MAX) NULL,
    [SerializedAnnotations] NVARCHAR(MAX) NULL,
	[TimeCreated]           DATETIME NOT NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_ActivityInstanceEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME CONSTRAINT [DF_WFTracking_ActivityInstanceEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_WFTracking_ActivityInstanceEvent] PRIMARY KEY ([EventID]),
);
GO

CREATE TABLE [WFTracking].[ExtendedActivityEvent] (
	[EventID]                        INT IDENTITY(1,1) NOT NULL,
	[WorkflowInstanceId]             UNIQUEIDENTIFIER NOT NULL,
	[RecordNumber]                   BIGINT NULL,
	[TraceLevelId]                   TINYINT NULL,
	[ActivityRecordType]             NVARCHAR(128) NOT NULL,
	[ActivityName]                   NVARCHAR(1024) NULL,
	[ActivityId]                     NVARCHAR(256) NULL,
	[ActivityInstanceId]             NVARCHAR(256) NULL,
	[ActivityType]                   NVARCHAR(2048) NULL,
	[ChildActivityName]              NVARCHAR(1024) NULL,
	[ChildActivityId]                NVARCHAR(256) NULL,
	[ChildActivityInstanceId]        NVARCHAR(256) NULL,
	[ChildActivityType]              NVARCHAR(2048) NULL,
	[FaultDetails]	                 NVARCHAR(MAX) NULL,
	[FaultHandlerActivityName]       NVARCHAR(1024) NULL,
	[FaultHandlerActivityId]         NVARCHAR(256) NULL,
	[FaultHandlerActivityInstanceId] NVARCHAR(256) NULL,
	[FaultHandlerActivityType]       NVARCHAR(2048) NULL,
	[SerializedAnnotations]          NVARCHAR(MAX) NULL,
	[TimeCreated]                    DATETIME NOT NULL,
    [rowguid]                        UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_ExtendedActivityEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]                   DATETIME CONSTRAINT [DF_WFTracking_ExtendedActivityEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_WFTracking_ExtendedActivityEvent] PRIMARY KEY ([EventID]),
);
GO

CREATE TABLE [WFTracking].[BookmarkResumptionEvent] ( 
    [EventID]                 INT IDENTITY(1,1) NOT NULL,
    [WorkflowInstanceId]      UNIQUEIDENTIFIER NOT NULL,
    [RecordNumber]            BIGINT NULL,
    [TraceLevelId]            TINYINT NULL,
    [BookmarkName]            NVARCHAR(1024) NULL,
    [BookmarkScope]           UNIQUEIDENTIFIER NULL,
    [OwnerActivityName]       NVARCHAR(1024) NULL,
    [OwnerActivityId]         NVARCHAR(256) NULL,
    [OwnerActivityInstanceId] NVARCHAR(256) NULL,
    [OwnerActivityType]       NVARCHAR(2048) NULL,
    [SerializedAnnotations]   NVARCHAR(MAX) NULL,
    [TimeCreated]             DATETIME NOT NULL,
    [rowguid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_BookmarkResumptionEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]            DATETIME CONSTRAINT [DF_WFTracking_BookmarkResumptionEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_WFTracking_BookmarkResumptionEvent] PRIMARY KEY ([EventID])
);
GO

CREATE TABLE [WFTracking].[CustomTrackingEvent] (
	[EventID]               INT IDENTITY(1,1) NOT NULL,
	[WorkflowInstanceId]    UNIQUEIDENTIFIER NOT NULL,
	[RecordNumber]          BIGINT NULL,
	[TraceLevelId]          TINYINT NULL,
	[CustomRecordName]      NVARCHAR(2048) NULL,
	[ActivityName]          NVARCHAR(1024) NULL,
	[ActivityId]            NVARCHAR(256) NULL,
	[ActivityInstanceId]    NVARCHAR(256) NULL,
	[ActivityType]          NVARCHAR(2048) NULL,
	[SerializedData]        NVARCHAR(MAX) NULL,
	[SerializedAnnotations] NVARCHAR(MAX) NULL,
	[TimeCreated]           DATETIME NOT NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_CustomTrackingEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME CONSTRAINT [DF_WFTracking_CustomTrackingEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_WFTracking_CustomTrackingEvent] PRIMARY KEY ([EventID]),
);
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
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (1, 1, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"},"medium":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"},"small":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"},"extraSmall":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"},"extraLarge":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (2, 1, N'Code', N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, N'{"name":"Code","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"کد حساب"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"کد حساب"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"کد حساب"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"کد حساب"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"کد حساب"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (3, 1, N'FullCode', N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, N'{"name":"FullCode","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"کد کامل حساب"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"کد کامل حساب"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"کد کامل حساب"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"کد کامل حساب"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"کد کامل حساب"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (4, 1, N'Name', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysVisible","title":"نام حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysVisible","title":"نام حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysVisible","title":"نام حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysVisible","title":"نام حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysVisible","title":"نام حساب"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (5, 1, N'Level', N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'{"name":"Level","large":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysHidden","title":"سطح"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysHidden","title":"سطح"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysHidden","title":"سطح"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysHidden","title":"سطح"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"AlwaysHidden","title":"سطح"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (6, 1, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, N'{"name":"Description","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (7, 2, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (8, 2, N'No', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"No","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (9, 2, N'Date', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (10, 2, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (11, 3, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (12, 3, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, N'{"name":"Description","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (13, 3, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (14, 3, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (15, 3, N'CurrencyId', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (16, 3, N'FullAccount', N'System.Object', N'(n/a)', N'object', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (17, 3, N'FullAccount.Account.Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (18, 3, N'FullAccount.DetailAccount.Id', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (19, 3, N'FullAccount.CostCenter.Id', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (20, 3, N'FullAccount.Project.Id', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (21, 4, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (22, 4, N'UserName', N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"UserName","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (23, 4, N'Password', N'System.String', N'nvarchar(32)', N'string', 32, 4, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (24, 4, N'LastLoginDate', N'System.DateTime', N'datetime', N'DateTime', 0, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (25, 4, N'IsEnabled', N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Expression], [Settings]) VALUES (26, 4, N'PersonFirstName', N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, 'Person.FirstName', NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Expression], [Settings]) VALUES (27, 4, N'PersonLastName', N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, 'Person.LastName', NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (28, 5, N'Name', N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (29, 5, N'Description', N'System.String', N'nvarchar(512)', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (30, 6, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (31, 6, N'Code', N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (32, 6, N'FullCode', N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (33, 6, N'Name', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (34, 6, N'Level', N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'{"name":"Level","large":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (35, 6, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (36, 7, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (37, 7, N'Code', N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (38, 7, N'FullCode', N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (39, 7, N'Name', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (40, 7, N'Level', N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'{"name":"Level","large":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (41, 7, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (42, 8, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (43, 8, N'Code', N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (44, 8, N'FullCode', N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (45, 8, N'Name', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (46, 8, N'Level', N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'{"name":"Level","large":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (47, 8, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (48, 9, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (49, 9, N'Name', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (50, 9, N'StartDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (51, 9, N'EndDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (52, 9, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (53, 10, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (54, 10, N'Name', N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (55, 10, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (56, 11, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (57, 11, N'Name', N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (58, 11, N'DbName', N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (59, 11, N'DbPath', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (60, 11, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (61, 11, N'Server', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (62, 11, N'UserName', N'System.String', N'nvarchar', N'string', 32, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (63, 11, N'Password', N'System.String', N'nvarchar', N'string', 32, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (64, 1, N'BranchScope', N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'{"name":"BranchScope","large":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"BranchScope"},"medium":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"BranchScope"},"small":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"BranchScope"},"extraSmall":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"BranchScope"},"extraLarge":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"BranchScope"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (65, 6, N'BranchScope', N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'{"name":"BranchScope","large":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (66, 7, N'BranchScope', N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'{"name":"BranchScope","large":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (67, 8, N'BranchScope', N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'{"name":"BranchScope","large":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (68, 3, N'FullAccount.Account.FullCode', N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (69, 2, N'StatusName', N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (70, 12, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"},"medium":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"},"small":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"},"extraSmall":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"},"extraLarge":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"Id"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (71, 12, N'Name', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":100,"index":1,"designIndex":1,"visibility":"AlwaysVisible","title":"نام گروه حساب"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"AlwaysVisible","title":"نام گروه حساب"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"AlwaysVisible","title":"نام گروه حساب"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"AlwaysVisible","title":"نام گروه حساب"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"AlwaysVisible","title":"نام گروه حساب"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (72, 12, N'Category', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"Category","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"ماهیت"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"ماهیت"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"ماهیت"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"ماهیت"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"ماهیت"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (73, 12, N'Description', N'System.String', N'nvarchar', N'string', 256, 0, 0, 1, 1, 1, N'{"name":"Description","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (74, 1, N'GroupId', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'{"name":"GroupId","large":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"GroupId"},"medium":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"GroupId"},"small":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"GroupId"},"extraSmall":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"GroupId"},"extraLarge":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"GroupId"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (75, 13, N'UserName', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (76, 13, N'Company', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (77, 13, N'Date', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (78, 13, N'Time', N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (79, 13, N'Entity', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (80, 13, N'Operation', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (81, 13, N'OperationResult', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (82, 14, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (83, 14, N'Name', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (84, 14, N'FullCode', N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (85, 1, N'CurrencyId', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'{"name":"CurrencyId","large":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"CurrencyId"},"medium":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"CurrencyId"},"small":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"CurrencyId"},"extraSmall":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"CurrencyId"},"extraLarge":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"CurrencyId"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (86, 1, N'IsActive', N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 1, 1, 1, N'{"name":"IsActive","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"وضعیت"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"وضعیت"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"وضعیت"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"وضعیت"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"وضعیت"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (87, 1, N'IsCurrencyAdjustable', N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 1, 1, 1, N'{"name":"IsCurrencyAdjustable","large":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"وضعیت تسعیر"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"وضعیت تسعیر"},"small":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"وضعیت تسعیر"},"extraSmall":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"وضعیت تسعیر"},"extraLarge":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"وضعیت تسعیر"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (88, 1, N'TurnoverMode', N'System.Int16', N'smallint', N'number', 0, 0, 0, 1, 1, 1, N'{"name":"TurnoverMode","large":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"TurnoverMode"},"medium":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"TurnoverMode"},"small":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"TurnoverMode"},"extraSmall":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"TurnoverMode"},"extraLarge":{"width":100,"index":-1,"designIndex":-1,"visibility":"AlwaysHidden","title":"TurnoverMode"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (89, 15, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (90, 15, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (91, 15, N'VoucherNo', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"VoucherNo","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (92, 15, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (93, 15, N'AccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountName","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (94, 15, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (95, 15, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (96, 15, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (97, 16, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (98, 16, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (99, 16, N'VoucherNo', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"VoucherNo","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (100, 16, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (101, 16, N'AccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountName","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (102, 16, N'DetailAccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"DetailAccountFullCode","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (103, 16, N'DetailAccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"DetailAccountName","large":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"نام تفصیلی شناور"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"نام تفصیلی شناور"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام تفصیلی شناور"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام تفصیلی شناور"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام تفصیلی شناور"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (104, 16, N'CostCenterFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"CostCenterFullCode","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"کد مرکز هزینه"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"کد مرکز هزینه"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد مرکز هزینه"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد مرکز هزینه"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد مرکز هزینه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (105, 16, N'CostCenterName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"CostCenterName","large":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"نام مرکز هزینه"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"نام مرکز هزینه"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام مرکز هزینه"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام مرکز هزینه"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام مرکز هزینه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (106, 16, N'ProjectFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"ProjectFullCode","large":{"width":100,"index":9,"designIndex":9,"visibility":"Visible","title":"کد پروژه"},"medium":{"width":100,"index":9,"designIndex":9,"visibility":"Visible","title":"کد پروژه"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد پروژه"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد پروژه"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد پروژه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (107, 16, N'ProjectName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"ProjectName","large":{"width":100,"index":10,"designIndex":10,"visibility":"Visible","title":"نام پروژه"},"medium":{"width":100,"index":10,"designIndex":10,"visibility":"Visible","title":"نام پروژه"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام پروژه"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام پروژه"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام پروژه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (108, 16, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":11,"designIndex":11,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":11,"designIndex":11,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (109, 16, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":12,"designIndex":12,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":12,"designIndex":12,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (110, 16, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":13,"designIndex":13,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":13,"designIndex":13,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (111, 17, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (112, 17, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (113, 17, N'VoucherNo', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"VoucherNo","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (114, 17, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (115, 17, N'AccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountName","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (116, 17, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (117, 17, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (118, 17, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (119, 18, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (120, 18, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (121, 18, N'VoucherNo', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"VoucherNo","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (122, 18, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (123, 18, N'AccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountName","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (124, 18, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (125, 18, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (126, 18, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (127, 19, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (128, 19, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (129, 19, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (130, 19, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (131, 19, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (132, 20, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (133, 20, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (134, 20, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (135, 20, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (136, 20, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (137, 20, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (138, 15, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (139, 16, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (140, 17, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","AlwaysVisible":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (141, 18, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","AlwaysVisible":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (142, 19, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","AlwaysVisible":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (143, 20, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":6,"designIndex":6,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","AlwaysVisible":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (144, 22, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (145, 22, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (146, 22, N'VoucherNo', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"VoucherNo","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (147, 22, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (148, 22, N'AccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountName","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (149, 22, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (150, 22, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (151, 22, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (152, 23, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (153, 23, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (154, 23, N'VoucherNo', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"VoucherNo","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (155, 23, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (156, 23, N'AccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountName","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (157, 23, N'DetailAccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"DetailAccountFullCode","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"کد تفصیلی شناور"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (158, 23, N'DetailAccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"DetailAccountName","large":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"نام تفصیلی شناور"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"نام تفصیلی شناور"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام تفصیلی شناور"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام تفصیلی شناور"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام تفصیلی شناور"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (159, 23, N'CostCenterFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"CostCenterFullCode","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"کد مرکز هزینه"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"کد مرکز هزینه"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد مرکز هزینه"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد مرکز هزینه"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد مرکز هزینه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (160, 23, N'CostCenterName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"CostCenterName","large":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"نام مرکز هزینه"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"Visible","title":"نام مرکز هزینه"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام مرکز هزینه"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام مرکز هزینه"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام مرکز هزینه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (161, 23, N'ProjectFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"ProjectFullCode","large":{"width":100,"index":9,"designIndex":9,"visibility":"Visible","title":"کد پروژه"},"medium":{"width":100,"index":9,"designIndex":9,"visibility":"Visible","title":"کد پروژه"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد پروژه"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد پروژه"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"کد پروژه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (162, 23, N'ProjectName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"ProjectName","large":{"width":100,"index":10,"designIndex":10,"visibility":"Visible","title":"نام پروژه"},"medium":{"width":100,"index":10,"designIndex":10,"visibility":"Visible","title":"نام پروژه"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام پروژه"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام پروژه"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"نام پروژه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (163, 23, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":11,"designIndex":11,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":11,"designIndex":11,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (164, 23, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":12,"designIndex":12,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":12,"designIndex":12,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (165, 23, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":13,"designIndex":13,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":13,"designIndex":13,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (166, 24, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (167, 24, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (168, 24, N'VoucherNo', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"VoucherNo","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (169, 24, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (170, 24, N'AccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountName","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (171, 24, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (172, 24, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (173, 24, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (174, 25, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (175, 25, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (176, 25, N'VoucherNo', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"VoucherNo","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"small":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraSmall":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"},"extraLarge":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"سند"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (177, 25, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (178, 25, N'AccountName', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountName","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"small":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraSmall":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"},"extraLarge":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"عنوان حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (179, 25, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (180, 25, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (181, 25, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (182, 22, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (183, 23, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":14,"designIndex":14,"visibility":"AlwaysVisible","title":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (184, 24, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (185, 25, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (186, 26, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (187, 26, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (188, 26, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (189, 26, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (190, 26, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (191, 26, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"AlwaysVisible","title":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (192, 21, N'RowNo', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"RowNo","large":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"small":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraSmall":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"},"extraLarge":{"width":100,"index":0,"designIndex":0,"visibility":"AlwaysVisible","title":"ردیف"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (193, 21, N'VoucherDate', N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'{"name":"VoucherDate","large":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"medium":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"small":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraSmall":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"},"extraLarge":{"width":100,"index":1,"designIndex":1,"visibility":"Visible","title":"تاریخ"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (194, 21, N'AccountFullCode', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"AccountFullCode","large":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"شماره حساب"},"medium":{"width":100,"index":2,"designIndex":2,"visibility":"Visible","title":"شماره حساب"},"small":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraSmall":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"},"extraLarge":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شماره حساب"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (195, 21, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Description","large":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"},"medium":{"width":100,"index":3,"designIndex":3,"visibility":"Visible","title":"شرح"},"small":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraSmall":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"},"extraLarge":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"شرح"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (196, 21, N'Debit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Debit","large":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"بدهکار"},"medium":{"width":100,"index":4,"designIndex":4,"visibility":"Visible","title":"بدهکار"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"},"extraLarge":{"width":100,"index":6,"designIndex":6,"visibility":"Visible","title":"بدهکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (197, 21, N'Credit',N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Credit","large":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"بستانکار"},"medium":{"width":100,"index":5,"designIndex":5,"visibility":"Visible","title":"بستانکار"},"small":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraSmall":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"},"extraLarge":{"width":100,"index":7,"designIndex":7,"visibility":"Visible","title":"بستانکار"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (198, 21, N'BranchName',NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'{"name":"BranchName","large":{"width":100,"index":6,"designIndex":6,"visibility":"AlwaysVisible","title":"شعبه"},"medium":{"width":100,"index":6,"designIndex":6,"visibility":"AlwaysVisible","title":"شعبه"},"small":{"width":100,"index":6,"designIndex":6,"visibility":"AlwaysVisible","title":"شعبه"},"extraSmall":{"width":100,"index":6,"designIndex":6,"visibility":"AlwaysVisible","title":"شعبه"},"extraLarge":{"width":100,"index":8,"designIndex":8,"visibility":"AlwaysVisible","title":"شعبه"}}')
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (199, 2, N'Reference',NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column]
    ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings])
	VALUES (200, 2, N'Association',NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, NULL)
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
SET IDENTITY_INSERT [Config].[Setting] OFF


-- Create system records for security

-- admin user is added with password 'Demo1234' (case-sensitive)
SET IDENTITY_INSERT [Auth].[User] ON
INSERT INTO [Auth].[User] (UserID, UserName, PasswordHash, IsEnabled) VALUES (1, N'admin', 'b22f213ec710f0b0e86297d10279d69171f50f01a04edf40f472a563e7ad8576', 1)
SET IDENTITY_INSERT [Auth].[User] OFF

SET IDENTITY_INSERT [Contact].[Person] ON
INSERT INTO [Contact].[Person] (PersonID, UserID, FirstName, LastName) VALUES (1, 1, N'راهبر', N'سیستم')
SET IDENTITY_INSERT [Contact].[Person] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (1, NULL, 1, 1, NULL, N'Accounting', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (2, 1, 1, 1, NULL, N'Accnt-Base', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (3, 1, 1, 1, NULL, N'Accnt-Operation', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (4, 1, 1, 1, NULL, N'Accnt-Report', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (5, 2, 1, 1, NULL, N'Accnt-Base-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (6, 3, 1, 1, NULL, N'Accnt-Operation-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (7, 4, 1, 1, NULL, N'Accnt-Report-QReport', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (8, 3, 1, 1, NULL, N'Voucher-Printing', NULL, 1, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (10, 5, 1, 1, 9, N'Fiscal-Periods', N'fperiods', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (11, 5, 1, 1, 1, N'Accounts', N'accounts', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (12, 5, 1, 1, 6, N'Detail-Accounts', N'faccounts', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (13, 5, 1, 1, 7, N'Cost-Centers', N'ccenters', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (14, 5, 1, 1, 8, N'Projects', N'projects', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (15, 5, 1, 1, 12, N'Account-Groups', N'accgroups', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (16, 5, 1, 1, 11, N'Companies', N'companies', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (17, 5, 1, 1, 10, N'Branches', N'branches', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (18, 5, 1, 1, 4, N'Users', N'users', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (19, 5, 1, 1, 5, N'Roles', N'roles', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (20, 5, 1, 1, 13, N'Operation-Logs', N'oplog', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (22, 7, 1, 1, 15, N'Journal-ByDate-ByRow', N'reports/journal/by-date/by-row', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (23, 7, 1, 1, 16, N'Journal-ByDate-ByRow-Detail', N'reports/journal/by-date/by-row-detail', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (24, 7, 1, 1, 17, N'Journal-ByDate-ByLedger', N'reports/journal/by-date/by-ledger', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (25, 7, 1, 1, 18, N'Journal-ByDate-BySubsidiary', N'reports/journal/by-date/by-subsid', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (26, 7, 1, 1, 19, N'Journal-ByDate-LedgerSummary', N'reports/journal/by-date/summary', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (27, 7, 1, 1, 20, N'Journal-ByDate-LedgerSummary-ByDate', N'reports/journal/by-date/sum-by-date', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (28, 7, 1, 1, 22, N'Journal-ByNo-ByRow', N'reports/journal/by-no/by-row', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (29, 7, 1, 1, 23, N'Journal-ByNo-ByRow-Detail', N'reports/journal/by-no/by-row-detail', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (30, 7, 1, 1, 24, N'Journal-ByNo-ByLedger', N'reports/journal/by-no/by-ledger', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (31, 7, 1, 1, 25, N'Journal-ByNo-BySubsidiary', N'reports/journal/by-no/by-subsid', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (32, 7, 1, 1, 26, N'Journal-ByNo-LedgerSummary', N'reports/journal/by-no/summary', 0, 1, 1, 1, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (33, 7, 1, 1, 2, N'Voucher-Sum-By-Date', N'reports/voucher/sum-by-date', 0, 1, 0, 0, N'RowNo,Voucher,Date,DebitSum,CreditSum,Difference,PreparedBy,BalanceLabel,CheckLabel,Origin')
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (34, 8, 1, 1, 1, N'Voucher-Std-Form', N'reports/voucher/std-form', 0, 1, 0, 0, NULL)
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (35, 8, 1, 1, 1, N'Voucher-Std-Form-Detail', N'reports/voucher/std-form-detail', 0, 1, 1, 0, NULL)
SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [ViewID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (36, 7, 1, 1, 21, N'Journal-ByDate-LedgerSummary-ByMonth', N'reports/journal/by-date/sum-by-month', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (1, 22, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (2, 22, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (3, 23, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (4, 23, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (5, 24, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (6, 24, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (7, 25, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (8, 25, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (9, 26, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (10, 26, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (11, 27, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (12, 27, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (13, 28, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (14, 28, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (15, 29, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (16, 29, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (17, 30, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (18, 30, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (19, 31, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (20, 31, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (21, 32, N'from', N'from', N'GTE', N'System.Int32', N'TextBox', N'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (22, 32, N'to', N'to', N'LTE', N'System.Int32', N'TextBox', N'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (23, 33, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (24, 33, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (25, 36, N'from', N'from', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (26, 36, N'to', N'to', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (1, 1, 1, N'Accounting', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (2, 2, 1, N'حسابداری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (3, 3, 1, N'Accounting', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (4, 4, 1, N'Accounting', NULL)
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
    VALUES (29, 1, 10, N'Fiscal periods')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (30, 2, 10, N'دوره های مالی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (31, 3, 10, N'Fiscal periods')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (32, 4, 10, N'Fiscal periods')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (33, 1, 11, N'Accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (34, 2, 11, N'سرفصل های حسابداری')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (35, 3, 11, N'Accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (36, 4, 11, N'Accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (37, 1, 12, N'Detail accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (38, 2, 12, N'تفصیلی های شناور')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (39, 3, 12, N'Detail accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (40, 4, 12, N'Detail accounts')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (41, 1, 13, N'Cost centers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (42, 2, 13, N'مراکز هزینه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (43, 3, 13, N'Cost centers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (44, 4, 13, N'Cost centers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (45, 1, 14, N'Projects')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (46, 2, 14, N'پروژه ها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (47, 3, 14, N'Projects')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (48, 4, 14, N'Projects')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (49, 1, 15, N'Account groups')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (50, 2, 15, N'گروه های حساب')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (51, 3, 15, N'Account groups')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (52, 4, 15, N'Account groups')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (53, 1, 16, N'Companies')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (54, 2, 16, N'شرکت ها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (55, 3, 16, N'Companies')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (56, 4, 16, N'Companies')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (57, 1, 17, N'Branches')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (58, 2, 17, N'شعب سازمانی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (59, 3, 17, N'Branches')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (60, 4, 17, N'Branches')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (61, 1, 18, N'Users')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (62, 2, 18, N'کاربران')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (63, 3, 18, N'Users')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (64, 4, 18, N'Users')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (65, 1, 19, N'Roles')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (66, 2, 19, N'نقش ها')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (67, 3, 19, N'Roles')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (68, 4, 19, N'Roles')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (69, 1, 20, N'Operation logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (70, 2, 20, N'لاگ های عملیاتی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (71, 3, 20, N'Operation logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (72, 4, 20, N'Operation logs')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (73, 1, 8, N'Voucher Printing')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (74, 2, 8, N'چاپ سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (75, 3, 8, N'Voucher Printing')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (76, 4, 8, N'Voucher Printing')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (77, 1, 22, N'Journal, by date, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (78, 2, 22, N'دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (79, 3, 22, N'Journal, by date, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (80, 4, 22, N'Journal, by date, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (81, 1, 23, N'Journal, by date, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (82, 2, 23, N'دفتر روزنامه، بر حسب تاریخ، مطابق ردیف های سند با سطوح شناور')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (83, 3, 23, N'Journal, by date, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (84, 4, 23, N'Journal, by date, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (85, 1, 24, N'Journal, by date, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (86, 2, 24, N'دفتر روزنامه، بر حسب تاریخ، در سطح کل')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (87, 3, 24, N'Journal, by date, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (88, 4, 24, N'Journal, by date, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (89, 1, 25, N'Journal, by date, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (90, 2, 25, N'دفتر روزنامه، بر حسب تاریخ، در سطح معین')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (91, 3, 25, N'Journal, by date, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (92, 4, 25, N'Journal, by date, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (93, 1, 26, N'Journal, by date, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (94, 2, 26, N'دفتر روزنامه، بر حسب تاریخ، سند خلاصه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (95, 3, 26, N'Journal, by date, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (96, 4, 26, N'Journal, by date, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (97, 1, 27, N'Journal, by date, summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (98, 2, 27, N'دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک تاریخ')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (99, 3, 27, N'Journal, by date, summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100, 4, 27, N'Journal, by date, summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (101, 1, 28, N'Journal, by number, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (102, 2, 28, N'دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (103, 3, 28, N'Journal, by number, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (104, 4, 28, N'Journal, by number, by row')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (105, 1, 29, N'Journal, by number, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (106, 2, 29, N'دفتر روزنامه، بر حسب شماره سند، مطابق ردیف های سند با سطوح شناور')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (107, 3, 29, N'Journal, by number, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (108, 4, 29, N'Journal, by number, by row with details')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (109, 1, 30, N'Journal, by number, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (110, 2, 30, N'دفتر روزنامه، بر حسب شماره سند، در سطح کل')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (111, 3, 30, N'Journal, by number, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (112, 4, 30, N'Journal, by number, by ledger')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (113, 1, 31, N'Journal, by number, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (114, 2, 31, N'دفتر روزنامه، بر حسب شماره سند، در سطح معین')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (115, 3, 31, N'Journal, by number, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (116, 4, 31, N'Journal, by number, by subsidiary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (117, 1, 32, N'Journal, by number, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (118, 2, 32, N'دفتر روزنامه، بر حسب شماره سند، سند خلاصه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (119, 3, 32, N'Journal, by number, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (120, 4, 32, N'Journal, by number, ledger summary')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (121, 1, 33, N'Voucher summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (122, 2, 33, N'خلاصه اسناد بر حسب تاریخ')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (123, 3, 33, N'Voucher summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (124, 4, 33, N'Voucher summary by date')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (125, 1, 34, N'Voucher, standard format')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (126, 2, 34, N'فرم مرسوم سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (127, 3, 34, N'Voucher, standard format')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (128, 4, 34, N'Voucher, standard format')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (129, 1, 35, N'Voucher, standard format, with detail')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (130, 2, 35, N'فرم مرسوم سند - با سطوح شناور')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (131, 3, 35, N'Voucher, standard format, with detail')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (132, 4, 35, N'Voucher, standard format, with detail')
SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (133, 1, 36, N'Journal, by date, summary by month')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (134, 2, 36, N'دفتر روزنامه، بر حسب تاریخ، سند خلاصه به تفکیک ماه')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (135, 3, 36, N'Journal, by date, summary by month')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (136, 4, 36, N'Journal, by date, summary by month')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

-- Sample user settings for UserID = 1 and Account List form (Admin user)...
SET IDENTITY_INSERT [Config].[UserSetting] ON
INSERT [Config].[UserSetting] ([UserSettingID], [SettingID], [ViewID], [UserID], [RoleID], [ModelType], [Values])
    VALUES (1, 4, 1, 1, NULL, N'ListFormViewConfig', N'{"viewId":1,"pageSize":25,"columnViews":[{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}},{"name":"Code","large":{"width":100,"index":0,"designIndex":0,"visibility":"Visible"},"medium":{"width":100,"index":0,"designIndex":0,"visibility":"Visible"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}},{"name":"FullCode","large":{"width":150,"index":1,"designIndex":0,"visibility":"Visible"},"medium":{"width":150,"index":1,"designIndex":0,"visibility":"Visible"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}},{"name":"Name","large":{"width":180,"index":2,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":180,"index":2,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":125,"index":2,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":125,"index":2,"designIndex":0,"visibility":"AlwaysVisible"}},{"name":"Level","large":{"width":50,"index":4,"designIndex":0,"visibility":"Visible"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}},{"name":"Description","large":{"width":360,"index":3,"designIndex":0,"visibility":"Visible"},"medium":{"width":360,"index":3,"designIndex":0,"visibility":"Visible"},"small":{"width":180,"index":3,"designIndex":0,"visibility":"Visible"},"extraSmall":{"width":180,"index":3,"designIndex":0,"visibility":"Visible"}}]}')
SET IDENTITY_INSERT [Config].[UserSetting] OFF

SET IDENTITY_INSERT [Auth].[Role] ON
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (1, N'راهبر سیستم', N'این نقش دارای کلیه دسترسی های تعریف شده در برنامه بوده و قابل اصلاح یا حذف نیست.')
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
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (18, N'ManageEntities,UserReports', N'Report')
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
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (30, 7, N'PrepareEntity,Voucher', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (31, 7, N'ReviewEntity,Voucher', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (32, 7, N'ConfirmEntity,Voucher', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (33, 7, N'ApproveEntity,Voucher', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (34, 7, N'CheckEntity,Voucher', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (35, 7, N'UncheckEntity,Voucher', 512)
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

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (1, NULL, NULL, N'Accounting', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (2, 1, 17, N'FiscalPeriods', N'/fiscalperiod', 'list', 'Ctrl+Shift+F')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (3, 1, 1, N'Accounts', N'/account', 'tasks', 'Ctrl+Shift+A')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (4, 1, 5, N'DetailAccounts', N'/detailAccount', 'list', 'Ctrl+Shift+D')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (5, 1, 9, N'CostCenters', N'/costCenter', 'list', 'Ctrl+Shift+C')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (6, 1, 13, N'Projects', N'/projects', 'list', 'Ctrl+Shift+P')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (7, 1, 56, N'AccountRelations', N'/accountrelations', 'list', 'Ctrl+Shift+R')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (8, 1, 26, N'Vouchers', N'/voucher', 'list', 'Ctrl+Shift+V')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (9, NULL, NULL, N'Organization', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (10, 9, 41, N'Companies', N'/companies', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (11, 9, 36, N'Branches', N'/branches', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (12, NULL, NULL, N'Administration', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (13, 12, 45, N'Users', N'/users', 'user', 'Ctrl+Shift+U')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (14, 12, 49, N'Roles', N'/roles', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (15, NULL, NULL, N'Profile', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (16, 15, NULL, N'ChangePassword', N'/changePassword', 'eye-open', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (17, 15, NULL, N'LogOut', N'/logout', 'log-out', 'Ctrl+Shift+X')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (18, 12, 58, N'RowAccessSettings', N'/viewRowPermission', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (19, 12, 60, N'Settings', N'/settings', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (20, 15, NULL, N'CompanyLogin', N'/login', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (21, 1, NULL, N'AccountGroups', N'/account-groups', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (22, 12, NULL, N'OperationLogs', N'/operation-log', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (23, 1, NULL, N'AccountCollections', N'/account-collection', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (24, 1, NULL, N'AccountingLedgers', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (25, 24, NULL, N'JournalLedger', N'/journal', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (26, 1, NULL, N'VoucherOps', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (27, 26, NULL, N'NewVoucher', N'/vouchers/new', N'list', N'Ctrl+N')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (28, 26, NULL, N'VoucherByNo', N'/vouchers/by-no/{0}', N'list', N'Ctrl+S')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (29, 26, NULL, N'LastVoucher', N'/vouchers/last', N'list', N'Ctrl+L')
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO
