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

CREATE SCHEMA [Finance]
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
    [Template]       NVARCHAR(MAX)    NULL,
    [TemplateLtr]    NVARCHAR(MAX)    NULL,
    [ResourceKeys]   VARCHAR(MAX)     NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_Report_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Reporting_Report_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_Report] PRIMARY KEY CLUSTERED ([ReportID] ASC)
    , CONSTRAINT [FK_Reporting_Report_Reporting_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Reporting].[Report]([ReportID])
    , CONSTRAINT [FK_Reporting_Report_Auth_CreatedBy] FOREIGN KEY ([CreatedByID]) REFERENCES [Auth].[User]([UserID])
    , CONSTRAINT [FK_Reporting_Report_Metadata_ReportView] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[ReportView]([ViewID])
)
GO

CREATE TABLE [Reporting].[Parameter] (
    [ParamID]          INT              IDENTITY (1, 1) NOT NULL,
    [ReportID]         INT              NOT NULL,
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

CREATE TABLE [Config].[ViewSetting] (
    [ViewSettingID]  INT              IDENTITY (1, 1) NOT NULL,
    [SettingID]      INT              NOT NULL,
    [ViewID]         INT              NULL,
    [ModelType]      VARCHAR(128)     NOT NULL,
    [Values]         NTEXT            NOT NULL,
    [DefaultValues]  NTEXT            NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_ViewSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_ViewSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_ViewSetting] PRIMARY KEY CLUSTERED ([ViewSettingID] ASC)
    , CONSTRAINT [FK_Config_ViewSetting_Config_Setting] FOREIGN KEY ([SettingID]) REFERENCES [Config].[Setting]([SettingID])
    , CONSTRAINT [FK_Config_ViewSetting_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
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
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (1, 'Account', 1, 1, N'/lookup/accounts/fp/{0}/branch/{1}')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (2, 'Voucher', 0, 1, N'/lookup/vouchers/fp/{0}/branch/{1}')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (3, 'VoucherLine', 0, 1, N'/lookup/vouchers/lines/fp/{0}/branch/{1}')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (4, 'User', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (5, 'Role', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (6, 'DetailAccount', 1, 1, N'/lookup/faccounts/fp/{0}/branch/{1}')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (7, 'CostCenter', 1, 1, N'/lookup/ccenters/fp/{0}/branch/{1}')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (8, 'Project', 1, 1, N'/lookup/projects/fp/{0}/branch/{1}')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (9, 'FiscalPeriod', 0, 1, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (10, 'Branch', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (11, 'CompanyDb', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (12, 'AccountGroup', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (13, 'OperationLog', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (14, N'AccountCollectionAccount', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (1, 1, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (2, 1, N'Code', N'System.String', N'nvarchar', N'string', 16, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (3, 1, N'FullCode', N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (4, 1, N'Name', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (5, 1, N'Level', N'System.Int16', N'smallint', N'', 0, 0, 0, 0, 1, 1, N'{"name":"Level","large":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Visible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (6, 1, N'Description', N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (7, 2, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (8, 2, N'No', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"No","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (9, 2, N'Date', N'System.DateTime', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL)
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
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (24, 4, N'LastLoginDate', N'System.DateTime', N'datetime', N'Date', 0, 0, 0, 1, 1, 1, NULL)
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
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (50, 9, N'StartDate', N'System.DateTime', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (51, 9, N'EndDate', N'System.DateTime', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL)
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
	VALUES (64, 1, N'BranchScope', N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 0, 1, N'{"name":"BranchScope","large":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"medium":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"small":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"},"extraSmall":{"width":null,"index":null,"designIndex":0,"visibility":"Hidden"}}')
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
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (70, 12, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'{"name":"Id","large":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"medium":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"small":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"},"extraSmall":{"width":0,"index":-1,"designIndex":0,"visibility":"AlwaysHidden"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (71, 12, N'Name', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'{"name":"Name","large":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"medium":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"small":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"},"extraSmall":{"width":null,"index":0,"designIndex":0,"visibility":"AlwaysVisible"}}')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (72, 12, N'Category', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (73, 12, N'Description', N'System.String', N'nvarchar', N'string', 256, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (74, 1, N'GroupId', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (75, 13, N'UserName', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (76, 13, N'Company', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (77, 13, N'Date', N'System.Date', N'int', N'Date', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (78, 13, N'Time', N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (79, 13, N'Entity', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (80, 13, N'Operation', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (81, 13, N'OperationResult', N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (82, 14, N'Id', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (83, 14, N'Name', N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (84, 14, N'FullCode', N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL)
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

SET IDENTITY_INSERT [Config].[ViewSetting] ON
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (1, 5, 1, 'ViewTreeConfig', N'{"viewId":1,"maxDepth":3,"levels":[{"no":1,"name":"LevelGeneral","codeLength":3,"isEnabled": true},{"no":2,"name":"LevelAuxiliary","codeLength":3,"isEnabled": true},{"no":3,"name":"LevelDetail","codeLength":4,"isEnabled": true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}', N'{"viewId":1,"maxDepth":3,"levels":[{"no":1,"name":"LevelGeneral","codeLength":3},{"no":2,"name":"LevelAuxiliary","codeLength":3},{"no":3,"name":"LevelDetail","codeLength":4},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (2, 5, 6, 'ViewTreeConfig', N'{"viewId":6,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}', N'{"viewId":6,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (3, 5, 7, 'ViewTreeConfig', N'{"viewId":7,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}', N'{"viewId":7,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (4, 5, 8, 'ViewTreeConfig', N'{"viewId":8,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}', N'{"viewId":8,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}')
SET IDENTITY_INSERT [Config].[ViewSetting] OFF


-- Create system records for security

-- admin user is added with password 'Admin@Tadbir1395'
SET IDENTITY_INSERT [Auth].[User] ON
INSERT INTO [Auth].[User] (UserID, UserName, PasswordHash, IsEnabled) VALUES (1, N'admin', '5ab4a25e31220c3b103aef3e32596211b90238a0d5933288efbd36c5154b82ff', 1)
SET IDENTITY_INSERT [Auth].[User] OFF

SET IDENTITY_INSERT [Contact].[Person] ON
INSERT INTO [Contact].[Person] (PersonID, UserID, FirstName, LastName) VALUES (1, 1, N'راهبر', N'سیستم')
SET IDENTITY_INSERT [Contact].[Person] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault])
    VALUES (1, NULL, 1, 1, N'Accounting', NULL, 1, 1, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault])
    VALUES (2, 1, 1, 1, N'Accnt-Base', NULL, 1, 1, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault])
    VALUES (3, 1, 1, 1, N'Accnt-Operation', NULL, 1, 1, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault])
    VALUES (4, 3, 1, 1, N'Voucher-Printing', NULL, 1, 1, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [ResourceKeys])
    VALUES (5, 4, 1, 1, N'Voucher-Sum-By-Date', N'reports/voucher/sum-by-date', 0, 1, 1, 'RowNo,Voucher,Date,DebitSum,CreditSum,Difference,PreparedBy,BalanceLabel,CheckLabel,Origin')
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault])
    VALUES (6, 4, 1, 1, N'Voucher-Std-Form', N'reports/voucher/std-form', 0, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault])
    VALUES (7, 4, 1, 1, N'Voucher-Std-Form-Detail', N'reports/voucher/std-form-detail', 0, 1, 1)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (1, 5, N'Date', N'GTE', N'System.DateTime', N'DatePicker', N'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])
    VALUES (2, 5, N'Date', N'LTE', N'System.DateTime', N'DatePicker', N'ToDate')
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
    VALUES (13, 1, 4, N'Voucher printing', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (14, 2, 4, N'چاپ سند', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (15, 3, 4, N'Voucher printing', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (16, 4, 4, N'Voucher printing', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (17, 1, 5, N'Voucher summary by date', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (18, 2, 5, N'خلاصه اسناد بر حسب تاریخ', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (19, 3, 5, N'Voucher summary by date', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (20, 4, 5, N'Voucher summary by date', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (21, 1, 6, N'Voucher - Standard form', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (22, 2, 6, N'فرم مرسوم سند', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (23, 3, 6, N'Voucher - Standard form', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (24, 4, 6, N'Voucher - Standard form', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (25, 1, 7, N'Voucher - Standard form with detail', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (26, 2, 7, N'فرم مرسوم سند با سطوح شناور', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (27, 3, 7, N'Voucher - Standard form with detail', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (28, 4, 7, N'Voucher - Standard form with detail', NULL)
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
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE TABLE [Finance].[AccountCollectionCategory] (
    [AccountCollectionCategoryID]   INT              IDENTITY (1, 1) NOT NULL,    
    [Name]                          NVARCHAR(128)    NOT NULL,
	[rowguid]                       UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountCollectionCategory_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]                  DATETIME         CONSTRAINT [DF_Finance_AccountCollectionCategory_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountCollectionCategory] PRIMARY KEY CLUSTERED ([AccountCollectionCategoryID] ASC)
)
GO

SET IDENTITY_INSERT [Finance].[AccountCollectionCategory] ON 
GO
INSERT [Finance].[AccountCollectionCategory] ([AccountCollectionCategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (1, N'ترازنامه ', N'49e54def-d6b9-4cd3-a065-8feef13026d9', CAST(N'2019-01-15T12:26:01.053' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([AccountCollectionCategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (2, N'سود و زیان', N'21ed1889-c1a5-46dc-b352-60b577f42154', CAST(N'2019-01-15T12:26:13.500' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([AccountCollectionCategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (3, N'خزانه داری  ', N'53757fa5-5e85-495d-ac53-a8f5a4869f6b', CAST(N'2019-01-15T12:26:26.410' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([AccountCollectionCategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (4, N'خرید و فروش', N'2e9ec944-c56b-4350-8489-0e2c24a1757d', CAST(N'2019-01-15T12:26:37.690' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([AccountCollectionCategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (5, N'بستن حساب ها ', N'247284c1-6d8c-4e9c-9fd2-8dc7aeb0d290', CAST(N'2019-01-15T12:26:52.327' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([AccountCollectionCategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (6, N'انبار', N'8c22dd59-094b-4ca8-b49f-094c8e157b50', CAST(N'2019-01-15T12:27:05.800' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([AccountCollectionCategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (7, N'اموال', N'be57a7ce-a837-4b84-9cfa-e6e2b6f638f9', CAST(N'2019-01-15T12:27:19.193' AS DateTime))
GO
SET IDENTITY_INSERT [Finance].[AccountCollectionCategory] OFF
GO


CREATE TABLE [Finance].[AccountCollection] (
    [AccountCollectionID]   INT              IDENTITY (1, 1) NOT NULL,
    [CategoryID]            INT              NOT NULL,    
    [Name]                  NVARCHAR(128)    NOT NULL,
    [MultiSelect]           BIT              NOT NULL,
    [TypeLevel]             SMALLINT         NOT NULL,
    [InventoryMode]         SMALLINT         NOT NULL,
	[rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountCollection_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Finance_AccountCollection_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountCollection] PRIMARY KEY CLUSTERED ([AccountCollectionID] ASC)
    , CONSTRAINT [FK_Finance_AccountCollection_Finance_Category] FOREIGN KEY ([CategoryID]) REFERENCES [Finance].[AccountCollectionCategory]([AccountCollectionCategoryID])
)

SET IDENTITY_INSERT [Finance].[AccountCollection] ON 
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (1, 1, N'داراييهاى جارى  ', 1, 0, 2, N'afc7ae6a-4520-4263-a5a6-b32e0829118b', CAST(N'2019-01-15T12:32:19.127' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (2, 1, N'داراييهاى غيرجارى ', 1, 0, 2, N'7c34827f-3f03-4be0-8d30-ec4ebb1cc6fe', CAST(N'2019-01-15T12:32:37.267' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (3, 1, N'بدهيهاى جارى ', 1, 0, 2, N'3557c04b-fde4-40a7-b8f3-004f4b75cac0', CAST(N'2019-01-15T12:33:01.480' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (4, 1, N'بدهيهاى غيرجارى', 1, 0, 2, N'0f4021af-2e5c-4ce0-8757-26ee3dd64df4', CAST(N'2019-01-15T12:33:33.307' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (5, 1, N'حقوق صاحبان سرمايه ', 1, 0, 2, N'1755af0b-37c9-4758-a451-68f84af5439a', CAST(N'2019-01-15T12:33:49.270' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (6, 1, N'حسابهاى انتظامى', 1, 0, 2, N'1cb5f6d6-3caf-4f67-a091-c63c0b15bbeb', CAST(N'2019-01-15T12:34:08.290' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (9, 2, N'فروش', 1, 0, 2, N'f1210802-59a7-4d86-803b-e90e45d373f9', CAST(N'2019-01-15T12:36:50.283' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (10, 2, N'برگشت  از فروش و تخفیفات ', 1, 0, 2, N'be473c3e-273c-498d-9f27-fbf9c2b70f3c', CAST(N'2019-01-15T12:37:47.480' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (11, 2, N'قيمت تمام شده كالاى فروش رفته', 1, 0, 1, N'8af11bc5-21fb-4af6-bf18-0517fa8c5d40', CAST(N'2019-01-15T12:38:36.130' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (12, 2, N'خرید', 1, 0, 0, N'da24deea-1c98-4ad9-9363-13327e32eaec', CAST(N'2019-01-15T12:39:19.490' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (13, 2, N'برگشت از خرید و تخفیفات ', 1, 0, 0, N'e1b78a0b-e200-415a-818a-fce1048c5735', CAST(N'2019-01-15T12:40:04.470' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (14, 2, N'هزينه هاى عملياتى', 1, 0, 2, N'4bc26140-7c45-4e2a-96d5-5ccf6266f713', CAST(N'2019-01-15T12:40:41.230' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (15, 2, N'سایر هزينه ها و درآمد ها', 1, 0, 2, N'94299081-734d-4f47-a99c-ec7b9ae8580c', CAST(N'2019-01-15T12:41:03.997' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (16, 3, N'صندوق ', 1, 0, 2, N'df0b3009-6075-451f-a235-c26ffae7f06c', CAST(N'2019-01-15T12:45:09.350' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (17, 3, N'بانک ', 1, 0, 2, N'6ed87a7a-848a-4855-8862-4b8e7b7d3f97', CAST(N'2019-01-15T12:45:23.713' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (18, 3, N'اسناد دریافتنی ', 1, 0, 2, N'ad3c0eb5-3cf0-4369-af43-2b3fa514a850', CAST(N'2019-01-15T12:45:40.737' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (19, 3, N'اسناد پرداختنی', 1, 0, 2, N'c7ed884b-e4e5-40ce-86ed-a5c4d8b35323', CAST(N'2019-01-15T12:46:11.560' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (20, 3, N'اسناد دریافتنی تضمینی ', 1, 0, 2, N'2b287b0b-1211-44d7-8176-57d2ad21b3e5', CAST(N'2019-01-15T12:46:37.440' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (21, 3, N'اسناد پرداختنی تضمینی ', 1, 0, 2, N'e859c4d9-a385-415a-91e5-48b0efd9b966', CAST(N'2019-01-15T12:46:54.073' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (22, 3, N'اسناد درجریان وصول ', 1, 0, 2, N'fcfc7d6b-eda1-4aef-b0ce-ae6b38102dac', CAST(N'2019-01-15T12:47:06.490' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (23, 3, N'اسناد برگشتی ', 1, 0, 2, N'ebc53de1-f7a2-47b6-8e12-c597d757c4f0', CAST(N'2019-01-15T12:47:22.033' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (24, 3, N'تنخواه گردان ها ', 1, 0, 2, N'94bc42d6-d86c-432a-9657-4e7e98666eda', CAST(N'2019-01-15T12:47:54.697' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (25, 4, N'فروش ', 1, 0, 2, N'8c4e7347-736f-44f3-b7f8-20dffb73ade2', CAST(N'2019-01-15T12:48:23.877' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (26, 4, N'برگشت از فروش ', 1, 0, 2, N'99906d33-c220-4b91-9f3b-f9dee4da979d', CAST(N'2019-01-15T12:48:36.030' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (27, 4, N'خرید ', 1, 0, 0, N'40805c86-0018-4976-a0d2-0e2dcc820c62', CAST(N'2019-01-15T12:48:50.010' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (28, 4, N'برگشت از خرید ', 1, 0, 0, N'b7d8b020-058b-4487-9a18-2781eb62f75f', CAST(N'2019-01-15T12:49:02.973' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (29, 4, N'اضافات فاکتور فروش ', 1, 0, 2, N'88f8c1ce-d96f-49f2-8702-70402c6fcc29', CAST(N'2019-01-15T12:49:16.573' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (30, 4, N'اضافات فاکتور خرید', 1, 0, 2, N'c9b09d2f-25d9-4192-aa55-5cc6f492348f', CAST(N'2019-01-15T12:49:33.737' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (31, 4, N'بدهکاران تجاری ', 1, 2, 2, N'b85de96f-f48d-4208-bcd0-78e8bd9d2c44', CAST(N'2019-01-15T12:50:00.620' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (32, 4, N'بستانکاران تجاری ', 1, 2, 2, N'56d36c06-fe0c-4a88-84b6-09c4cee7c646', CAST(N'2019-01-15T12:50:32.900' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (33, 4, N'تخفیفات فروش ', 0, 0, 2, N'43eaf605-e1c4-48a6-b532-761dee9e64db', CAST(N'2019-01-15T12:50:51.310' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (34, 4, N'تخفیفات خرید ', 0, 0, 0, N'83b0e472-c21a-4800-b479-65b25d476716', CAST(N'2019-01-15T12:51:41.607' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (35, 4, N'قیمت تمام شده ', 0, 0, 2, N'a41051cf-3919-4eef-a9bb-f5e100afd6d0', CAST(N'2019-01-15T12:52:06.367' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (36, 4, N'فروشنده / خریدار  متفرقه ', 0, 1, 2, N'b20122c9-8a39-445d-bbe0-3f424c0529e2', CAST(N'2019-01-15T12:52:29.343' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (37, 4, N'مالیات پرداختنی ', 0, 0, 2, N'cb889886-cc01-4583-9ad2-267adfff47b2', CAST(N'2019-01-15T12:52:51.373' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (38, 4, N'عوارض پرداختنی', 0, 0, 2, N'60332921-671e-4f04-af3e-3c0b9ebadebd', CAST(N'2019-01-15T12:53:11.953' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (39, 4, N'مالیات دریافتنی', 0, 0, 2, N'c7a96c21-8160-417b-8185-7e25846d5247', CAST(N'2019-01-15T12:53:31.907' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (40, 4, N'عوارض دریافتنی', 0, 0, 2, N'521498a4-28c1-477d-89e6-95365e01e4fb', CAST(N'2019-01-15T12:53:55.050' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (41, 5, N'افتتاحیه ', 0, 1, 2, N'de1f3fb4-383f-4222-af54-62f3211b5d61', CAST(N'2019-01-15T12:54:36.443' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (42, 5, N'اختتامیه ', 0, 1, 2, N'7670e838-690b-4011-90e1-5b4370f932af', CAST(N'2019-01-15T12:54:51.660' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (43, 5, N'عملکرد ', 0, 1, 2, N'44c8cf51-a426-433d-a950-e8d62653f553', CAST(N'2019-01-15T12:55:11.510' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (44, 5, N'سود و زیان سال جاری', 0, 1, 2, N'3b1cab2a-7f71-4088-a35d-82fc44d42f44', CAST(N'2019-01-15T12:55:30.237' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (45, 5, N'سود و زیان انباشته ', 0, 1, 2, N'4119925a-e825-4a3a-b86f-1899c9ddaf33', CAST(N'2019-01-15T12:55:50.610' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (46, 6, N'موجودی کالا ', 0, 2, 2, N'af71d08f-dc59-4de7-a745-04563f083aa9', CAST(N'2019-01-15T12:56:51.717' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (47, 6, N'کنترل دستمزد', 0, 0, 2, N'67f35b34-d512-47cf-9602-f02d4b7d1cd1', CAST(N'2019-01-15T13:26:26.080' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (48, 6, N'کنترل سربار ', 0, 0, 2, N'082edb78-743f-4f6c-b70a-a989421ed780', CAST(N'2019-01-15T13:27:04.580' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (49, 7, N'اموال ', 0, 0, 2, N'70db4c8e-1763-4f92-a0fa-f478c2af259a', CAST(N'2019-01-15T13:27:42.067' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (50, 7, N'سود و زیان عملیات اموال', 0, 0, 2, N'05cd9415-9a94-42a2-9321-189eb3677e0d', CAST(N'2019-01-15T13:28:04.187' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([AccountCollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (51, 7, N'اموال انتقالی ', 0, 0, 2, N'0058abb5-6531-44f7-b1a1-fa78c0d67a30', CAST(N'2019-01-15T13:28:52.073' AS DateTime))
GO
SET IDENTITY_INSERT [Finance].[AccountCollection] OFF
GO
