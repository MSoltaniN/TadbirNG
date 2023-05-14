
-- 1.2.1376
UPDATE [Config].[ViewSetting]
SET [DefaultValues] = N'{"viewId":1,"maxDepth":3,"levels":[{"no":1,"name":"LevelGeneral","codeLength":3,"isEnabled":true,"isUsed":true},{"no":2,"name":"LevelAuxiliary","codeLength":3,"isEnabled":true,"isUsed":true},{"no":3,"name":"LevelDetail","codeLength":4,"isEnabled":true,"isUsed":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}'
WHERE ViewID = 1

UPDATE [Config].[ViewSetting]
SET [DefaultValues] = N'{"viewId":6,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}'
WHERE ViewID = 6

UPDATE [Config].[ViewSetting]
SET [DefaultValues] = N'{"viewId":7,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}'
WHERE ViewID = 7

UPDATE [Config].[ViewSetting]
SET [DefaultValues] = N'{"viewId":8,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}'
WHERE ViewID = 8
GO

-- 1.2.1424
CREATE SCHEMA [Reporting]
GO

CREATE TABLE [Reporting].[Dashboard] (
    [DashboardID]      INT              IDENTITY (1, 1) NOT NULL,
    [UserID]           INT              NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_Dashboard_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_Dashboard_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_Dashboard] PRIMARY KEY CLUSTERED ([DashboardID] ASC)
)
GO

CREATE TABLE [Reporting].[DashboardTab] (
    [DashboardTabID]   INT              IDENTITY (1, 1) NOT NULL,
    [DashboardID]      INT              NOT NULL,
    [Index]            INT              NOT NULL,
	[Title]            NVARCHAR(128)    NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_DashboardTab_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_DashboardTab_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_DashboardTab] PRIMARY KEY CLUSTERED ([DashboardTabID] ASC)
    , CONSTRAINT [FK_Reporting_DashboardTab_Reporting_Dashboard] FOREIGN KEY ([DashboardID]) REFERENCES [Reporting].[Dashboard]([DashboardID])
)
GO

CREATE TABLE [Reporting].[WidgetFunction] (
    [WidgetFunctionID] INT              IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR(64)     NOT NULL,
    [ServiceUrl]   NVARCHAR(512)    NULL,
    [Description]  NVARCHAR(512)    NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_WidgetFunction_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Reporting_WidgetFunction_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_WidgetFunction] PRIMARY KEY CLUSTERED ([WidgetFunctionID] ASC)
)
GO

CREATE TABLE [Reporting].[WidgetType] (
    [WidgetTypeID]     INT              IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_WidgetType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_WidgetType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_WidgetType] PRIMARY KEY CLUSTERED ([WidgetTypeID] ASC)
)
GO

CREATE TABLE [Reporting].[WidgetParameter] (
    [WidgetParameterID]  INT              IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR(64)     NOT NULL,
    [Alias]         NVARCHAR(64)     NOT NULL,
    [Type]          NVARCHAR(64)     NOT NULL,
    [DefaultValue]  NVARCHAR(128)    NOT NULL,
    [Description]   NVARCHAR(512)    NULL,
    [rowguid]       UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_WidgetParameter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_Reporting_WidgetParameter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_WidgetParameter] PRIMARY KEY CLUSTERED ([WidgetParameterID] ASC)
)
GO

CREATE TABLE [Reporting].[Widget] (
    [WidgetID]         INT              IDENTITY (1, 1) NOT NULL,
    [CreatedByID]      INT              NOT NULL,
    [FunctionID]       INT              NOT NULL,
    [TypeID]           INT              NOT NULL,
    [Title]            NVARCHAR(128)    NOT NULL,
    [DefaultSettings]  NVARCHAR(1024)   NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_Widget_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_Widget_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_Widget] PRIMARY KEY CLUSTERED ([WidgetID] ASC)
    , CONSTRAINT [FK_Reporting_Widget_Reporting_WidgetFunction] FOREIGN KEY ([FunctionID]) REFERENCES [Reporting].[WidgetFunction]([WidgetFunctionID])
    , CONSTRAINT [FK_Reporting_Widget_Reporting_WidgetType] FOREIGN KEY ([TypeID]) REFERENCES [Reporting].[WidgetType]([WidgetTypeID])
)
GO

CREATE TABLE [Reporting].[WidgetAccount] (
    [WidgetAccountID]  INT              IDENTITY (1, 1) NOT NULL,
    [WidgetID]         INT              NOT NULL,
    [AccountID]        INT              NULL,
    [DetailAccountID]  INT              NULL,
    [CostCenterID]     INT              NULL,
    [ProjectID]        INT              NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_WidgetAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_WidgetAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_WidgetAccount] PRIMARY KEY CLUSTERED ([WidgetAccountID] ASC)
    , CONSTRAINT [FK_Reporting_WidgetAccount_Reporting_Widget] FOREIGN KEY ([WidgetID]) REFERENCES [Reporting].[Widget]([WidgetID])
    , CONSTRAINT [FK_Reporting_WidgetAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Reporting_WidgetAccount_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
    , CONSTRAINT [FK_Reporting_WidgetAccount_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Reporting_WidgetAccount_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
)
GO

CREATE TABLE [Reporting].[UsedWidgetParameter] (
    [WidgetParameterID] INT              IDENTITY (1, 1) NOT NULL,
    [WidgetID]          INT              NOT NULL,
    [ParameterID]       INT              NOT NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_UsedWidgetParameter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Reporting_UsedWidgetParameter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_UsedWidgetParameter] PRIMARY KEY CLUSTERED ([WidgetParameterID] ASC)
    , CONSTRAINT [FK_Reporting_UsedWidgetParameter_Reporting_Widget] FOREIGN KEY ([WidgetID]) REFERENCES [Reporting].[Widget]([WidgetID])
    , CONSTRAINT [FK_Reporting_UsedWidgetParameter_Reporting_Parameter] FOREIGN KEY ([ParameterID]) REFERENCES [Reporting].[WidgetParameter]([WidgetParameterID])
)
GO

CREATE TABLE [Reporting].[TabWidget] (
    [TabWidgetID]     INT              IDENTITY (1, 1) NOT NULL,
    [TabID]           INT              NOT NULL,
    [WidgetID]        INT              NOT NULL,
    [Settings]        NVARCHAR(1024)   NOT NULL,
    [DefaultSettings] NVARCHAR(1024)   NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_TabWidget_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Reporting_TabWidget_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_TabWidget] PRIMARY KEY CLUSTERED ([TabWidgetID] ASC)
    , CONSTRAINT [FK_Reporting_TabWidget_Reporting_Widget] FOREIGN KEY ([WidgetID]) REFERENCES [Reporting].[Widget]([WidgetID])
    , CONSTRAINT [FK_Reporting_TabWidget_Reporting_DashboardTab] FOREIGN KEY ([TabID]) REFERENCES [Reporting].[DashboardTab]([DashboardTabID])
)
GO

CREATE TABLE [Auth].[RoleWidget] (
    [RoleWidgetID] INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]       INT              NOT NULL,
    [WidgetID]     INT              NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RoleWidget_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Auth_RoleWidget_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RoleWidget] PRIMARY KEY CLUSTERED ([RoleWidgetID] ASC)
    , CONSTRAINT [FK_Auth_RoleWidget_Reporting_Widget] FOREIGN KEY ([WidgetID]) REFERENCES [Reporting].[Widget]([WidgetID])
)
GO

-- 1.2.1425
UPDATE [Config].[Setting] 
SET  [Values] =  N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendar":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true,"inventoryMode": 1}' ,
    [DefaultValues] =  N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendar":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true,"inventoryMode": 1}'
WHERE ModelType = 'SystemConfig'

-- 1.2.1428
SET IDENTITY_INSERT [Reporting].[WidgetFunction] ON 
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name], [ServiceUrl]) VALUES (1, N'Function_DebitTurnover', N'debit-to')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name], [ServiceUrl]) VALUES (2, N'Function_CreditTurnover', N'credit-to')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name], [ServiceUrl]) VALUES (3, N'Function_NetTurnover', N'net-to')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name], [ServiceUrl]) VALUES (4, N'Function_Balance', N'balance')
SET IDENTITY_INSERT [Reporting].[WidgetFunction] OFF

SET IDENTITY_INSERT [Reporting].[WidgetType] ON 
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (1, N'Chart_ColumnChart')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (2, N'Chart_BarChart')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (3, N'Chart_LineGraph')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (4, N'Chart_PieChart')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (5, N'Chart_AreaGraph')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (6, N'Chart_ScatterPlot')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (7, N'Chart_BubbleChart')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (8, N'Chart_StackedColumnChart')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (9, N'Chart_StackedBarChart')
SET IDENTITY_INSERT [Reporting].[WidgetType] OFF

SET IDENTITY_INSERT [Reporting].[WidgetParameter] ON 
INSERT [Reporting].[WidgetParameter] ([WidgetParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (1, N'FromDate', N'from', N'System.DateTime', N'FiscalPeriodStart')
INSERT [Reporting].[WidgetParameter] ([WidgetParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (2, N'ToDate', N'to', N'System.DateTime', N'FiscalPeriodEnd')
INSERT [Reporting].[WidgetParameter] ([WidgetParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (3, N'DateUnit', N'unit', N'System.Int32', N'Monthly')
SET IDENTITY_INSERT [Reporting].[WidgetParameter] OFF

-- 1.2.1429
ALTER TABLE [Reporting].[WidgetFunction]
DROP COLUMN [ServiceUrl]
GO

-- 1.2.1435
DROP TABLE [Reporting].[UsedWidgetParameter]
GO

DROP TABLE [Reporting].[WidgetParameter]
GO

CREATE TABLE [Reporting].[FunctionParameter] (
    [FunctionParameterID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR(64)     NOT NULL,
    [Alias]                 NVARCHAR(64)     NOT NULL,
    [Type]                  NVARCHAR(64)     NOT NULL,
    [DefaultValue]          NVARCHAR(128)    NOT NULL,
    [Description]           NVARCHAR(512)    NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_FunctionParameter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Reporting_FunctionParameter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_FunctionParameter] PRIMARY KEY CLUSTERED ([FunctionParameterID] ASC)
)
GO

CREATE TABLE [Reporting].[UsedParameter] (
    [UsedParameterID]   INT              IDENTITY (1, 1) NOT NULL,
    [ParameterID]       INT              NOT NULL,
    [FunctionID]        INT              NOT NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_UsedParameter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Reporting_UsedParameter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_UsedParameter] PRIMARY KEY CLUSTERED ([UsedParameterID] ASC)
    , CONSTRAINT [FK_Reporting_UsedParameter_Reporting_Parameter] FOREIGN KEY ([ParameterID]) REFERENCES [Reporting].[FunctionParameter]([FunctionParameterID])
    , CONSTRAINT [FK_Reporting_UsedParameter_Reporting_Function] FOREIGN KEY ([FunctionID]) REFERENCES [Reporting].[WidgetFunction]([WidgetFunctionID])
)
GO

SET IDENTITY_INSERT [Reporting].[FunctionParameter] ON
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (1, N'FromDate', N'from', N'System.DateTime', N'FiscalPeriodStart')
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (2, N'ToDate', N'to', N'System.DateTime', N'FiscalPeriodEnd')
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (3, N'DateUnit', N'unit', N'System.Int32', N'Monthly')
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (4, N'MinValue', N'min', N'System.Int32', N'0')
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (5, N'MaxValue', N'max', N'System.Int32', N'100')
SET IDENTITY_INSERT [Reporting].[FunctionParameter] OFF

SET IDENTITY_INSERT [Reporting].[UsedParameter] ON
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (1, 1, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (2, 1, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (3, 1, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (4, 2, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (5, 2, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (6, 2, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (7, 3, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (8, 3, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (9, 3, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (10, 4, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (11, 4, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (12, 4, 3)
SET IDENTITY_INSERT [Reporting].[UsedParameter] OFF

-- 1.2.1437
SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (19, N'DashboardTab')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (20, N'Widget')
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (194, 1, 3, NULL, 19, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (195, 1, 3, NULL, 19, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (196, 1, 3, NULL, 19, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (197, 1, 3, NULL, 19, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (198, 1, 3, NULL, 19, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (199, 1, 3, NULL, 20, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (200, 1, 3, NULL, 20, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (201, 1, 3, NULL, 20, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (202, 1, 3, NULL, 20, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (203, 1, 3, NULL, 20, 54, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1446
SET IDENTITY_INSERT [Reporting].[WidgetType] ON
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (10, N'Gauge_Circular')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (11, N'Gauge_Digital')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (12, N'Gauge_Linear')
SET IDENTITY_INSERT [Reporting].[WidgetType] OFF

SET IDENTITY_INSERT [Reporting].[WidgetFunction] ON 
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (5, N'Function_LiquidRatio')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (6, N'Function_GrossSales')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (7, N'Function_NetSales')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (8, N'Function_GrossNetSales')
SET IDENTITY_INSERT [Reporting].[WidgetFunction] OFF

SET IDENTITY_INSERT [Reporting].[UsedParameter] ON
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (13, 5, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (14, 5, 4)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (15, 5, 5)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (16, 6, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (17, 6, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (18, 6, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (19, 7, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (20, 7, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (21, 7, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (22, 8, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (23, 8, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (24, 8, 3)
SET IDENTITY_INSERT [Reporting].[UsedParameter] OFF

-- 1.2.1447
DELETE [Reporting].[UsedParameter]
WHERE FunctionID >= 5

DELETE [Reporting].[Widget]
WHERE FunctionID >= 5

DELETE [Reporting].[WidgetFunction]
WHERE WidgetFunctionID >= 5

SET IDENTITY_INSERT [Reporting].[WidgetFunction] ON
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (5, N'FunctionXB_LiquidRatio')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (6, N'FunctionXT_GrossSales')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (7, N'FunctionXT_NetSales')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (8, N'FunctionXB_BankBalance')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (9, N'FunctionXB_CashBalance')
SET IDENTITY_INSERT [Reporting].[WidgetFunction] OFF

SET IDENTITY_INSERT [Reporting].[UsedParameter] ON
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (13, 5, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (14, 5, 4)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (15, 5, 5)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (16, 6, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (17, 6, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (18, 6, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (19, 7, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (20, 7, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (21, 7, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (22, 8, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (23, 8, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (24, 8, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (25, 9, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (26, 9, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (27, 9, 3)
SET IDENTITY_INSERT [Reporting].[UsedParameter] OFF

-- 1.2.1450
SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (11, 'UserProfileSettings', 3, 1, 'UserProfileConfig', N'{}', N'{}', 'UserProfileSettingsDescription', 0)
SET IDENTITY_INSERT [Config].[Setting] OFF

-- 1.2.1468
DELETE FROM [Auth].[RoleWidget]
WHERE [WidgetID] IN(
    SELECT [WidgetID]
    FROM [Reporting].[Widget]
    WHERE [TypeID] NOT IN(1, 2, 3, 4, 10))

DELETE FROM [Reporting].[TabWidget]
WHERE [WidgetID] IN(
    SELECT [WidgetID]
    FROM [Reporting].[Widget]
    WHERE [TypeID] NOT IN(1, 2, 3, 4, 10))

DELETE FROM [Reporting].[WidgetAccount]
WHERE [WidgetID] IN(
    SELECT [WidgetID]
    FROM [Reporting].[Widget]
    WHERE [TypeID] NOT IN(1, 2, 3, 4, 10))

DELETE FROM [Reporting].[Widget]
WHERE [TypeID] NOT IN(1, 2, 3, 4, 10)

DELETE FROM [Reporting].[WidgetType]
WHERE [WidgetTypeID] NOT IN(1, 2, 3, 4, 10)

-- 1.2.1482
CREATE SCHEMA [CashFlow]
GO

CREATE SCHEMA [Check]
GO

SET IDENTITY_INSERT [Metadata].[Subsystem] ON
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (2, N'Treasury')
SET IDENTITY_INSERT [Metadata].[Subsystem] OFF

-- 1.2.1483
DELETE FROM [Config].[LogSetting]

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (1, 1, 1, NULL, 1, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (2, 1, 1, NULL, 1, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (3, 1, 1, NULL, 1, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (4, 1, 1, NULL, 1, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (5, 1, 1, NULL, 1, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (6, 1, 1, NULL, 1, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (7, 1, 1, NULL, 1, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (8, 1, 1, NULL, 1, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (9, 1, 1, NULL, 1, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (10, 1, 1, NULL, 2, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (11, 1, 1, NULL, 2, 7, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (12, 1, 1, NULL, 4, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (13, 1, 1, NULL, 4, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (14, 1, 1, NULL, 4, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (15, 1, 1, NULL, 4, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (16, 1, 1, NULL, 4, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (17, 1, 1, NULL, 4, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (18, 1, 1, NULL, 4, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (19, 1, 1, NULL, 4, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (20, 1, 1, NULL, 4, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (21, 1, 1, NULL, 5, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (22, 1, 1, NULL, 5, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (23, 1, 1, NULL, 5, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (24, 1, 1, NULL, 5, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (25, 1, 1, NULL, 5, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (26, 1, 1, NULL, 5, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (27, 1, 1, NULL, 5, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (28, 1, 1, NULL, 5, 35, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (29, 1, 1, NULL, 5, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (30, 1, 1, NULL, 5, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (31, 1, 1, NULL, 6, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (32, 1, 1, NULL, 6, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (33, 1, 1, NULL, 6, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (34, 1, 1, NULL, 6, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (35, 1, 1, NULL, 6, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (36, 1, 1, NULL, 6, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (37, 1, 1, NULL, 6, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (38, 1, 1, NULL, 6, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (39, 1, 1, NULL, 6, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (40, 1, 1, NULL, 7, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (41, 1, 1, NULL, 7, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (42, 1, 1, NULL, 7, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (43, 1, 1, NULL, 7, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (44, 1, 1, NULL, 7, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (45, 1, 1, NULL, 7, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (46, 1, 1, NULL, 7, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (47, 1, 1, NULL, 7, 40, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (48, 1, 1, NULL, 7, 41, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (49, 1, 1, NULL, 7, 42, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (50, 1, 1, NULL, 7, 43, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (51, 1, 1, NULL, 7, 44, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (52, 1, 1, NULL, 7, 45, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (53, 1, 1, NULL, 7, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (54, 1, 1, NULL, 7, 55, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (55, 1, 1, NULL, 7, 56, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (56, 1, 1, NULL, 7, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (57, 1, 1, NULL, 9, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (58, 1, 1, NULL, 9, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (59, 1, 1, NULL, 9, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (60, 1, 1, NULL, 9, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (61, 1, 1, NULL, 9, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (62, 1, 1, NULL, 9, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (63, 1, 1, NULL, 9, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (64, 1, 1, NULL, 9, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (65, 1, 1, NULL, 9, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (66, 1, 1, NULL, 10, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (67, 1, 1, NULL, 10, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (68, 1, 1, NULL, 10, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (69, 1, 1, NULL, 10, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (70, 1, 1, NULL, 10, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (71, 1, 1, NULL, 10, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (72, 1, 1, NULL, 10, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (73, 1, 1, NULL, 10, 35, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (74, 1, 1, NULL, 10, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (75, 1, 1, NULL, 10, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (76, 1, 2, NULL, 11, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (77, 1, 2, NULL, 11, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (78, 1, 2, NULL, 11, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (79, 1, 2, NULL, 11, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (80, 1, 2, NULL, 11, 8, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (81, 1, 2, NULL, 11, 30, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (82, 1, 2, NULL, 11, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (83, 1, 2, NULL, 11, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (84, 1, 1, NULL, 12, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (85, 1, 1, NULL, 12, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (86, 1, 1, NULL, 12, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (87, 1, 1, NULL, 12, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (88, 1, 1, NULL, 12, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (89, 1, 1, NULL, 12, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (90, 1, 1, NULL, 12, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (91, 1, 1, NULL, 12, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (92, 1, 1, NULL, 12, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (93, 1, 1, NULL, 15, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (94, 1, 1, NULL, 15, 7, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (95, 1, 1, NULL, 15, 31, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (96, 1, 2, NULL, 17, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (97, 1, 2, NULL, 17, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (98, 1, 2, NULL, 17, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (99, 1, 2, NULL, 17, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100, 1, 2, NULL, 17, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (101, 1, 2, NULL, 17, 11, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (102, 1, 2, NULL, 17, 12, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (103, 1, 2, NULL, 17, 13, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (104, 1, 2, NULL, 17, 14, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (105, 1, 2, NULL, 17, 15, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (106, 1, 2, NULL, 17, 16, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (107, 1, 2, NULL, 17, 17, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (108, 1, 2, NULL, 17, 18, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (109, 1, 2, NULL, 17, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (110, 1, 2, NULL, 17, 36, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (111, 1, 2, NULL, 17, 37, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (112, 1, 2, NULL, 17, 38, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (113, 1, 2, NULL, 17, 39, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (114, 1, 2, NULL, 17, 46, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (115, 1, 2, NULL, 17, 47, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (116, 1, 2, NULL, 17, 48, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (117, 1, 2, NULL, 17, 49, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (118, 1, 2, NULL, 17, 50, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (119, 1, 2, NULL, 17, 51, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (120, 1, 2, NULL, 17, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (121, 1, 2, NULL, 18, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (122, 1, 2, NULL, 18, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (123, 1, 2, NULL, 18, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (124, 1, 2, NULL, 18, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (125, 1, 2, NULL, 18, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (126, 1, 2, NULL, 18, 11, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (127, 1, 2, NULL, 18, 12, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (128, 1, 2, NULL, 18, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (129, 1, 2, NULL, 18, 36, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (130, 1, 2, NULL, 18, 37, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (131, 1, 2, NULL, 18, 38, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (132, 1, 2, NULL, 18, 39, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (133, 1, 2, NULL, 18, 46, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (134, 1, 2, NULL, 18, 47, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (135, 1, 2, NULL, 18, 52, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (136, 1, 2, NULL, 18, 53, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (137, 1, 2, NULL, 18, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (138, 1, 3, NULL, 19, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (139, 1, 3, NULL, 19, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (140, 1, 3, NULL, 19, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (141, 1, 3, NULL, 19, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (142, 1, 3, NULL, 19, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (143, 1, 3, NULL, 20, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (144, 1, 3, NULL, 20, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (145, 1, 3, NULL, 20, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (146, 1, 3, NULL, 20, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (147, 1, 3, NULL, 20, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (148, 1, 3, NULL, 20, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (149, 1, 3, NULL, 20, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (150, 1, 3, NULL, 20, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (151, 1, 3, 1, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (152, 1, 3, 1, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (153, 1, 3, 1, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (154, 1, 3, 1, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (155, 1, 3, 1, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (156, 1, 3, 2, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (157, 1, 3, 2, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (158, 1, 3, 2, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (159, 1, 3, 2, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (160, 1, 3, 2, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (161, 1, 3, 3, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (162, 1, 3, 3, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (163, 1, 3, 3, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (164, 1, 3, 3, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (165, 1, 3, 3, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (166, 1, 3, 4, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (167, 1, 3, 4, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (168, 1, 3, 4, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (169, 1, 3, 4, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (170, 1, 3, 4, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (171, 1, 3, 5, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (172, 1, 3, 5, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (173, 1, 3, 5, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (174, 1, 3, 5, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (175, 1, 3, 5, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (176, 1, 3, 6, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (177, 1, 3, 6, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (178, 1, 3, 6, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (179, 1, 3, 6, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (180, 1, 3, 6, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (181, 1, 1, 9, NULL, 32, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (182, 1, 1, 9, NULL, 33, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (183, 1, 1, 9, NULL, 34, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (184, 1, 3, 10, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (185, 1, 3, 10, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (186, 1, 3, 10, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (187, 1, 3, 10, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (188, 1, 3, 10, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (189, 1, 1, 11, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (190, 1, 1, 11, NULL, 7, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (191, 1, 3, 12, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (192, 1, 3, 12, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (193, 1, 3, 12, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (194, 1, 3, 12, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (195, 1, 3, 12, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (196, 1, 3, 13, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (197, 1, 3, 13, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (198, 1, 3, 13, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (199, 1, 3, 13, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (200, 1, 3, 13, NULL, 58, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1485
ALTER TABLE [Config].[LogSetting]
DROP CONSTRAINT [FK_Config_LogSetting_Metadata_Subsystem]

UPDATE [Config].[LogSetting]
SET [SubsystemID] = 3
WHERE [SubsystemID] = 2

UPDATE [Config].[LogSetting]
SET [SubsystemID] = 2
WHERE [SubsystemID] = 1

DELETE FROM [Metadata].[Subsystem]

SET IDENTITY_INSERT [Metadata].[Subsystem] ON
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (2, N'Accounting')
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (3, N'Treasury')
SET IDENTITY_INSERT [Metadata].[Subsystem] OFF

ALTER TABLE [Config].[LogSetting]
ADD CONSTRAINT [FK_Config_LogSetting_Metadata_Subsystem] FOREIGN KEY ([SubsystemID]) REFERENCES [Metadata].[Subsystem]([SubsystemID])

-- 1.2.1487
CREATE TABLE [CashFlow].[CashRegister] (
    [CashRegisterID]   INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]         INT              NOT NULL,
    [FiscalPeriodID]   INT              NOT NULL,
    [BranchScope]      SMALLINT         NOT NULL,
    [Name]             NVARCHAR(256)    NOT NULL,
    [Description]      NVARCHAR(256)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_CashRegister_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_CashFlow_CashRegister_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_CashRegister] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC)
    , CONSTRAINT [FK_CashFlow_CashRegister_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_CashFlow_CashRegister_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
)
GO

CREATE TABLE [CashFlow].[UserCashRegister] (
    [UserCashRegisterID]   INT              IDENTITY (1, 1) NOT NULL,
    [CashRegisterID]       INT              NOT NULL,
    [UserID]               INT              NOT NULL,
    [rowguid]              UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_UserCashRegister_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_CashFlow_UserCashRegister_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_UserCashRegister] PRIMARY KEY CLUSTERED ([UserCashRegisterID] ASC)
    , CONSTRAINT [FK_CashFlow_UserCashRegister_CashFlow_CashRegister] FOREIGN KEY ([CashRegisterID]) REFERENCES [CashFlow].[CashRegister]([CashRegisterID])
)
GO

DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 22

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (22, N'CashRegister', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (66, N'AssignCashRegisterUser')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (214, 3, 1, NULL, 22, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (215, 3, 1, NULL, 22, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (216, 3, 1, NULL, 22, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (217, 3, 1, NULL, 22, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (218, 3, 1, NULL, 22, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (219, 3, 1, NULL, 22, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (220, 3, 1, NULL, 22, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (221, 3, 1, NULL, 22, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (222, 3, 1, NULL, 22, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (223, 3, 1, NULL, 22, 66, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1495
CREATE TABLE [Check].[CheckBook] (
    [CheckBookID]     INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]        INT              NOT NULL,
    [AccountID]       INT              NULL,
    [DetailAccountID] INT              NULL,
    [CostCenterID]    INT              NULL,
    [ProjectID]       INT              NULL,
    [FiscalPeriodID]  INT              CONSTRAINT [DF_Check_CheckBook_FiscalPeriodID] DEFAULT (0) NULL,
    [CheckBookNo]     NVARCHAR(32)     NOT NULL,
    [Name]            NVARCHAR(64)     NOT NULL,
    [IssueDate]       DATETIME         NOT NULL,
    [StartNo]         NVARCHAR(32)     NOT NULL,
    [EndNo]           NVARCHAR(32)     NOT NULL,
    [BankName]        NVARCHAR(32)     NULL,
    [IsArchived]      BIT              NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Check_CheckBook_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Check_CheckBook_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Check_CheckBook] PRIMARY KEY CLUSTERED ([CheckBookID] ASC)
    , CONSTRAINT [FK_Check_CheckBook_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Check_CheckBook_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Check_CheckBook_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
    , CONSTRAINT [FK_Check_CheckBook_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Check_CheckBook_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
)
GO

CREATE TABLE [Check].[CheckBookPage] (
    [CheckBookPageID]   INT              IDENTITY (1, 1) NOT NULL,
    [CheckBookID]       INT              NOT NULL,
    [SerialNo]          NVARCHAR(64)     NOT NULL,
    [Status]            SMALLINT         NULL,
    [CheckId]           INT              NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Check_CheckBookPage_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Check_CheckBookPage_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Check_CheckBookPage] PRIMARY KEY CLUSTERED ([CheckBookPageID] ASC)
    , CONSTRAINT [FK_Check_CheckBookPage_Check_CheckBook] FOREIGN KEY ([CheckBookID]) REFERENCES [Check].[CheckBook]([CheckBookID])
)
GO

DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 21

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (21, N'CheckBook', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (60, N'CreatePages')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (61, N'DeletePages')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (62, N'CancelPage')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (63, N'UndoCancelPage')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (64, N'ConnectToCheck')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (65, N'DisconnectFromCheck')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (201, 3, 2, NULL, 21, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (202, 3, 2, NULL, 21, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (203, 3, 2, NULL, 21, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (204, 3, 2, NULL, 21, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (205, 3, 2, NULL, 21, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (206, 3, 2, NULL, 21, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (207, 3, 2, NULL, 21, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (208, 3, 2, NULL, 21, 60, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (209, 3, 2, NULL, 21, 61, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (210, 3, 2, NULL, 21, 62, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (211, 3, 2, NULL, 21, 63, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (212, 3, 2, NULL, 21, 64, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (213, 3, 2, NULL, 21, 65, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1496
ALTER TABLE [Check].[CheckBook]
ALTER COLUMN [AccountID] INT NOT NULL

-- 1.2.1497
SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (15, N'CheckBook', NULL)
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (67, N'UndoArchive')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (224, 3, 3, 15, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (225, 3, 3, 15, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (226, 3, 3, 15, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (227, 3, 3, 15, NULL, 8, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (228, 3, 3, 15, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (229, 3, 3, 15, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (230, 3, 3, 15, NULL, 67, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1509
ALTER TABLE [Check].[CheckBook]
DROP COLUMN [CheckBookNo]
GO

ALTER TABLE [Check].[CheckBook]
ADD [CheckBookNo] INT NULL,
    [CreatedByID] INT CONSTRAINT [DF_Check_CheckBook_CreatedByID] DEFAULT (1) NOT NULL,
	[ModifiedByID] INT CONSTRAINT [DF_Check_CheckBook_ModifiedByID] DEFAULT (1) NOT NULL,
	[SeriesNo] NVARCHAR(32) CONSTRAINT [DF_Check_CheckBook_SeriesNo] DEFAULT 'A' NOT NULL,
	[SayyadStartNo] NVARCHAR(16) CONSTRAINT [DF_Check_CheckBook_SayyadStartNo] DEFAULT '1' NOT NULL,
	[CreatedDate] DATETIME CONSTRAINT [DF_Check_CheckBook_CreatedDate] DEFAULT (getdate()) NOT NULL
GO

ALTER TABLE [Check].[CheckBookPage]
ADD [SayyadNo] NVARCHAR(16) CONSTRAINT [DF_Check_CheckBookPage_SeriesNo] DEFAULT ('1') NOT NULL

-- 1.2.1510
DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 23

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (23, N'SourceApp', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (231, 3, 1, NULL, 23, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (232, 3, 1, NULL, 23, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (233, 3, 1, NULL, 23, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (234, 3, 1, NULL, 23, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (235, 3, 1, NULL, 23, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (236, 3, 1, NULL, 23, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (237, 3, 1, NULL, 23, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (238, 3, 1, NULL, 23, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (239, 3, 1, NULL, 23, 58, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1512
CREATE TABLE [Config].[UserValueCategory] (
    [CategoryID]   INT              IDENTITY (1, 1) NOT NULL,
    [NameKey]      NVARCHAR(64)     NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Config_UserValueCategory_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Config_UserValueCategory_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_UserValueCategory] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
)
GO

CREATE TABLE [Config].[UserValue] (
    [ValueID]      INT              IDENTITY (1, 1) NOT NULL,
    [CategoryID]   INT              NOT NULL,
    [Value]        NVARCHAR(512)    NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Config_UserValue_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Config_UserValue_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_UserValue] PRIMARY KEY CLUSTERED ([ValueID] ASC)
    , CONSTRAINT [FK_Config_UserValue_Config_Category] FOREIGN KEY ([CategoryID]) REFERENCES [Config].[UserValueCategory]([CategoryID])
)
GO

SET IDENTITY_INSERT [Config].[UserValueCategory] ON
INSERT INTO [Config].[UserValueCategory] ([CategoryID], [NameKey])
    VALUES (1, 'BankName')
SET IDENTITY_INSERT [Config].[UserValueCategory] OFF

-- 1.2.1513
CREATE TABLE [CashFlow].[SourceApp] (
    [SourceAppID]    INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]       INT              NOT NULL,
    [FiscalPeriodID] INT              NOT NULL,
    [BranchScope]    SMALLINT         NOT NULL,
    [Code]           NVARCHAR(64)     NOT NULL,
    [Name]           NVARCHAR(256)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [Type]           SMALLINT         NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_SourceApp_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_CashFlow_SourceApp_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_SourceApp] PRIMARY KEY CLUSTERED ([SourceAppID] ASC)
    , CONSTRAINT [FK_CashFlow_SourceApp_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_CashFlow_SourceApp_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
)
GO

DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 24

DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 25

-- 1.2.1514
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (68, N'Register')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (24, N'Payment', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (25, N'Receival', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (240, 3, 2, NULL, 24, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (241, 3, 2, NULL, 24, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (242, 3, 2, NULL, 24, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (243, 3, 2, NULL, 24, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (244, 3, 2, NULL, 24, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (245, 3, 2, NULL, 24, 13, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (246, 3, 2, NULL, 24, 14, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (247, 3, 2, NULL, 24, 15, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (248, 3, 2, NULL, 24, 16, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (249, 3, 2, NULL, 24, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (250, 3, 2, NULL, 24, 68, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (251, 3, 2, NULL, 25, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (252, 3, 2, NULL, 25, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (253, 3, 2, NULL, 25, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (254, 3, 2, NULL, 25, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (255, 3, 2, NULL, 25, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (256, 3, 2, NULL, 25, 13, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (257, 3, 2, NULL, 25, 14, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (258, 3, 2, NULL, 25, 15, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (259, 3, 2, NULL, 25, 16, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (260, 3, 2, NULL, 25, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (261, 3, 2, NULL, 25, 68, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

CREATE TABLE [CashFlow].[PayReceive] (
    [PayReceiveID]      INT              IDENTITY (1, 1) NOT NULL,
    [FiscalPeriodID]    INT              NOT NULL,
    [BranchID]          INT              NOT NULL,
    [IssuedByID]        INT              NOT NULL,
    [ModifiedByID]      INT              NOT NULL,
    [ConfirmedByID]     INT              NULL,
    [ApprovedByID]      INT              NULL,
    [Type]              SMALLINT         NOT NULL,
    [PayReceiveNo]                NVARCHAR(16)     NOT NULL,
    [Reference]         NVARCHAR(64)     NULL,
    [Date]              DATETIME         NOT NULL,
    [CurrencyID]        INT              NULL,
    [CurrencyRate]      money            NULL,
    [Description]       NVARCHAR(1024)   NULL,
    [CreatedDate]       DATETIME         NOT NULL,
    [IssuedByName]      NVARCHAR(64)     NOT NULL,
    [ModifiedByName]    NVARCHAR(64)     NOT NULL,
    [ConfirmedByName]   NVARCHAR(64)     NULL,
    [ApprovedByName]    NVARCHAR(64)     NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_PayReceive_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_CashFlow_PayReceive_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_PayReceive] PRIMARY KEY CLUSTERED ([PayReceiveID] ASC)
    , CONSTRAINT [FK_CashFlow_PayReceive_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_CashFlow_PayReceive_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
	, CONSTRAINT [FK_CashFlow_PayReceive_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
)
GO