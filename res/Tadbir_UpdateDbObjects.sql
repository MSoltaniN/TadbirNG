
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
