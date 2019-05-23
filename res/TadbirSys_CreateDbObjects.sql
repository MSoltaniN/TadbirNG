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
	[Visibility]       NVARCHAR(32)     NULL,
	[DisplayIndex]     SMALLINT         NOT NULL,
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
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (24, 3, N'CurrencyId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'948526ec-fab1-4ff3-835a-0f6442e39c03', CAST(N'2019-05-20T13:53:18.153' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (25, 3, N'FullAccount', NULL, N'System.Object', N'(n/a)', N'object', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'9a20e8e0-ff89-400b-ac1e-f0d2522df3d0', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (26, 3, N'FullAccount.Account.Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'0d836f6c-bdd3-415b-9303-d1b6e1cbd1a1', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (27, 3, N'FullAccount.DetailAccount.Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'de3351d4-237c-4e41-a5ec-869d793e44ed', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (28, 3, N'FullAccount.CostCenter.Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'c8d97331-82b3-4690-aae1-4c7e1942cae7', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (29, 3, N'FullAccount.Project.Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL, N'db0bf32d-1a15-4319-b59c-f937127fa454', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (30, 3, N'FullAccount.Account.FullCode', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL, 0, NULL, N'9ecee01c-b787-430e-a109-86b7d7ec8e13', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (31, 4, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL, N'a10315f9-4a19-4b14-bf61-42632ca839de', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (32, 4, N'UserName', NULL, N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL, N'3fb41b0f-fb58-4f46-b96a-0a43680199ba', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (33, 4, N'Password', NULL, N'System.String', N'nvarchar(32)', N'string', 32, 4, 0, 0, 1, 1, NULL, 1, NULL, N'52e7d18b-eae4-4740-a441-daa8f81e9e09', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (34, 4, N'LastLoginDate', NULL, N'System.DateTime', N'datetime', N'DateTime', 0, 0, 0, 1, 1, 1, NULL, 2, NULL, N'8bfaaaed-1f88-49f3-9062-284ad4fe23db', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (35, 4, N'IsEnabled', NULL, N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 0, 1, 1, NULL, 3, NULL, N'2d889074-ac7a-40ff-bb9c-5a15b3688d55', CAST(N'2019-05-20T13:53:18.157' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (36, 4, N'PersonFirstName', NULL, N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, N'Person.FirstName', N'9c4507f8-ee9a-4c0a-bba1-e67dfed92cb4', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (37, 4, N'PersonLastName', NULL, N'System.String', N'nvarchar(64)', N'string', 64, 0, 0, 0, 1, 1, NULL, 5, N'Person.LastName', N'55a34f6a-79f0-48c3-9880-104b5b5bb1d3', CAST(N'2019-05-20T13:53:18.160' AS DateTime))
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
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (82, 13, N'Company', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL, N'f6602676-bad1-43ef-9c63-ec051c89e3a5', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (83, 13, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 2, NULL, N'94e921a5-02bb-4a35-a4a7-3641c33ef169', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (84, 13, N'Time', NULL, N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, NULL, 3, NULL, N'efbc0f74-a84b-4e29-8ca8-da189e353106', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (85, 13, N'Entity', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL, N'46b1a759-936a-43df-a878-1f70b697e108', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (86, 13, N'Operation', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL, N'9b97e674-39f5-44ce-ad73-cb2c62273eab', CAST(N'2019-05-20T13:53:18.170' AS DateTime))
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression], [rowguid], [ModifiedDate]) VALUES (87, 13, N'OperationResult', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL, N'db86193b-1b84-4b09-92d9-062dc157a69e', CAST(N'2019-05-20T13:53:18.173' AS DateTime))
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
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (28, 26, NULL, N'VoucherByNo', N'/vouchers/by-no', N'list', N'Ctrl+S')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (29, 26, NULL, N'LastVoucher', N'/vouchers/last', N'list', N'Ctrl+L')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (30, NULL, NULL, N'Tools', NULL, N'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (31, 30, NULL, N'ReportManagement', NULL, N'list', N'Ctrl+R')
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO
