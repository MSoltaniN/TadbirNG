--USE [TadbirSysDemo]
--GO

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
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (17, 3, N'FullAccount.AccountId', N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (18, 3, N'FullAccount.DetailId', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (19, 3, N'FullAccount.CostCenterId', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Settings]) VALUES (20, 3, N'FullAccount.ProjectId', N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, NULL)
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
SET IDENTITY_INSERT [Metadata].[Column] OFF


-- Create configuration records...
SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (1, 'AccountRelationsSettings', 2, 1, 'RelationsConfig', N'{"useLeafAccounts": true, "useLeafDetails": true, "useLeafCostCenters": true,"useLeafProjects": true}', N'{"useLeafAccounts": true, "useLeafDetails": true, "useLeafCostCenters": true,"useLeafProjects": true}', 'AccountRelationsSettingsDescription')
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
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

