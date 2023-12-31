﻿USE [NGTadbirSys]
GO

-- 1.2.1482
CREATE TABLE [Metadata].[Subsystem] (
    [SubsystemID]    INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Subsystem_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Subsystem_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Subsystem] PRIMARY KEY CLUSTERED ([SubsystemID] ASC)
)
GO

SET IDENTITY_INSERT [Metadata].[Subsystem] ON
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (1, N'Administration')
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (2, N'Accounting')
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (3, N'Treasury')
SET IDENTITY_INSERT [Metadata].[Subsystem] OFF

ALTER TABLE [Reporting].[Report]
ADD CONSTRAINT [FK_Reporting_Report_Metadata_Subsystem] FOREIGN KEY ([SubsystemID]) REFERENCES [Metadata].[Subsystem]([SubsystemID])

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (93, NULL, 1, NULL, 3, 'Treasury', NULL, 1, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (94, 93, 1, NULL, 3, 'Treasury-Base', NULL, 1, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (95, 93, 1, NULL, 3, 'Treasury-Operation', NULL, 1, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (96, 93, 1, NULL, 3, 'Treasury-Report', NULL, 1, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (97, 94, 1, NULL, 3, 'Treasury-Base-QReport', NULL, 1, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (98, 95, 1, NULL, 3, 'Treasury-Operation-QReport', NULL, 1, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (99, 96, 1, NULL, 3, 'Treasury-Report-QReport', NULL, 1, 1, 0, 0)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (277, 1, 93, 'Treasury', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (278, 2, 93, N'خزانه‌داری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (279, 1, 94, 'Base Data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (280, 2, 94, N'اطلاعات پایه', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (281, 1, 95, 'Operational Data', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (282, 2, 95, N'اطلاعات عملیاتی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (283, 1, 96, 'Reports', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (284, 2, 96, N'گزارشات', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (285, 1, 97, 'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (286, 2, 97, N'گزارش فوری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (287, 1, 98, 'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (288, 2, 98, N'گزارش فوری', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (289, 1, 99, 'Quick Report', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (290, 2, 99, N'گزارش فوری', NULL)
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (52, NULL, NULL, N'Treasury', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (53, 52, NULL, N'BaseData', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (54, 52, NULL, N'CheckOperations', NULL, N'folder-close', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (55, 52, NULL, N'CheckReports', NULL, N'folder-close', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.2.1483
DELETE FROM [Metadata].[OperationSource]
WHERE OperationSourceID = 10

SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name])
    VALUES (14, N'SystemSettings')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

-- 1.2.1487
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (70, 'CashRegister', N'CashRegister', N'Base', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (704, 70, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (705, 70, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (706, 70, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (707, 70, 'FiscalPeriodId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (708, 70, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (709, 70, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (710, 70, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName], [Description])
    VALUES (38, N'ManageEntities,CashRegisters', N'CashRegister', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
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
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (101, 97, 1, 70, 3, N'', N'cash-registers', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (293, 1, 101, N'Cash Register List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (294, 2, 101, N'فهرست صندوقهای اسناد')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (59, 53, 225, N'CashRegisters', N'/treasury/cash-register', 'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.2.1488
UPDATE [Metadata].[Column]
SET [IsNullable] = 1
WHERE ColumnID = 706

UPDATE [Reporting].[LocalReport]
SET [Caption] = N'فهرست صندوق‌های اسناد'
WHERE [LocalReportID] = 294

-- 1.2.1493
CREATE TABLE [Metadata].[OperationSourceType] (
    [OperationSourceTypeID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR(64)     NOT NULL,
    [rowguid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_OperationSourceType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]            DATETIME         CONSTRAINT [DF_Metadata_OperationSourceType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_OperationSourceType] PRIMARY KEY CLUSTERED ([OperationSourceTypeID] ASC)
)
GO

SET IDENTITY_INSERT [Metadata].[OperationSourceType] ON
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (1, N'BaseData')
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (2, N'OperationalForms')
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (3, N'Reports')
SET IDENTITY_INSERT [Metadata].[OperationSourceType] OFF

ALTER TABLE [Auth].[Permission]
DROP CONSTRAINT [FK_Auth_Permission_Auth_PermissionGroup]

DELETE FROM [Auth].[PermissionGroup]

ALTER TABLE [Auth].[PermissionGroup]
ADD [SubsystemID] INT NOT NULL

ALTER TABLE [Auth].[PermissionGroup]
ADD CONSTRAINT [FK_Auth_PermissionGroup_Metadata_Subsystem] FOREIGN KEY ([SubsystemID]) REFERENCES [Metadata].[Subsystem]([SubsystemID])

ALTER TABLE [Auth].[PermissionGroup]
ADD [SourceTypeID] INT NOT NULL

ALTER TABLE [Auth].[PermissionGroup]
ADD CONSTRAINT [FK_Auth_PermissionGroup_Metadata_SourceType] FOREIGN KEY ([SourceTypeID]) REFERENCES [Metadata].[OperationSourceType]([OperationSourceTypeID])

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
    VALUES (38, 3, 1, N'ManageEntities,CashRegisters', N'CashRegister', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

ALTER TABLE [Auth].[Permission]
ADD CONSTRAINT [FK_Auth_Permission_Auth_PermissionGroup] FOREIGN KEY ([GroupID]) REFERENCES [Auth].[PermissionGroup] ([PermissionGroupID])
GO

-- 1.2.1495
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (69, 'CheckBookPage', N'CheckBookPage', N'Core', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (697, 69, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (698, 69, 'SerialNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (699, 69, 'Status', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (700, 69, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (701, 69, 'CheckBookPageID', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (702, 69, 'CheckBookID', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (703, 69, 'CheckID', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (37, 3, 2, N'ManageEntities,CheckBooks', N'CheckBook', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
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
    VALUES (218, 37, N'Navigate', 64, NULL)
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
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100, 96, 1, 69, 3, N'', N'check-books', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (291, 1, 100, N'Check Book')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (292, 2, 100, N'دسته چک')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (56, 54, 215, N'NewCheckBook', N'/treasury/check-books/new', NULL, NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (57, 54, 218, N'LastCheckBook', N'/treasury/check-books/last', NULL, NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (58, 54, 212, N'CheckBookByName', N'/treasury/check-books/by-name', NULL, NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.2.1496
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (71, 'CheckBook', N'CheckBook', N'Operational', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (711, 71, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (712, 71, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (713, 71, 'CheckBookNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (714, 71, 'BankName', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (715, 71, 'IssueDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (716, 71, 'StartNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (717, 71, 'EndNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (718, 71, 'IsArchived', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 1, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (719, 71, 'AccountId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (720, 71, 'DetailAccountId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (721, 71, 'CostCenterId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (722, 71, 'ProjectId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (723, 71, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (724, 71, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1497
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (72, 'CheckBookReport', N'CheckBookReport', NULL, NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (725, 72, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (726, 72, 'CheckBookNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (727, 72, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (728, 72, 'BankName', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (729, 72, 'IssueDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (730, 72, 'StartNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (731, 72, 'EndNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (732, 72, 'AccountCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (733, 72, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (734, 72, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (735, 72, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (736, 72, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (737, 72, 'IsArchived', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (738, 72, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (39, 3, 3, N'CheckBookReport', N'CheckBookReport', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
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
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (102, 99, 1, 72, 3, N'', N'check-book-report', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (295, 1, 102, N'Check Book Report')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (296, 2, 102, N'دفتر دسته‌چک')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (60, 55, 233, N'CheckBookReport', N'/treasury/check-book-report', NULL, NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.2.1498
UPDATE [Metadata].[Column]
SET [Name] = 'StatusName', [StorageType] = 'nvarchar', [DotNetType] = 'System.String', [ScriptType] = 'string', [Length] = 32
WHERE [ViewID] = 69 AND [Name] = 'Status'

UPDATE [Metadata].[Column]
SET [Visibility] = N'AlwaysHidden', [DisplayIndex] = -1
WHERE [ViewID] = 69 AND [Name] = 'CheckID'

UPDATE [Metadata].[Column]
SET [IsNullable] = 1
WHERE [ViewID] = 71 AND [Name] = 'EndNo'

-- 1.2.1500
UPDATE [Auth].[Permission]
SET [Name] = 'NavigateEntities,CheckBooks'
WHERE [GroupID] = 37 AND [Name] = 'Navigate'

UPDATE [Metadata].[Column]
SET [Name] = 'FullAccount.Account.Id'
WHERE [ViewID] = 71 AND [Name] = 'AccountId'

UPDATE [Metadata].[Column]
SET [Name] = 'FullAccount.DetailAccount.Id'
WHERE [ViewID] = 71 AND [Name] = 'DetailAccountId'

UPDATE [Metadata].[Column]
SET [Name] = 'FullAccount.CostCenter.Id'
WHERE [ViewID] = 71 AND [Name] = 'CostCenterId'

UPDATE [Metadata].[Column]
SET [Name] = 'FullAccount.Project.Id'
WHERE [ViewID] = 71 AND [Name] = 'ProjectId'

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (739, 71, 'FullAccount', NULL, NULL, 'System.Object', '(n/a)', 'object', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (740, 71, 'FullAccount.Account.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (741, 71, 'FullAccount.Account.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (742, 71, 'FullAccount.DetailAccount.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (743, 71, 'FullAccount.DetailAccount.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (744, 71, 'FullAccount.CostCenter.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (745, 71, 'FullAccount.CostCenter.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (746, 71, 'FullAccount.Project.FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (747, 71, 'FullAccount.Project.Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 15, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (748, 71, 'PageCount', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'Hidden', 16, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1502
ALTER TABLE [Metadata].[Command]
ADD [Index] INT NULL
GO

UPDATE [Metadata].[Command]
SET [Index] = 0
WHERE [TitleKey] = N'Profile'

UPDATE [Metadata].[Command]
SET [Index] = 1
WHERE [TitleKey] = N'Accounting'

UPDATE [Metadata].[Command]
SET [Index] = 2
WHERE [TitleKey] = N'Treasury'

UPDATE [Metadata].[Command]
SET [Index] = 3
WHERE [TitleKey] = N'Organization'

UPDATE [Metadata].[Command]
SET [Index] = 4
WHERE [TitleKey] = N'Administration'

UPDATE [Metadata].[Command]
SET [Index] = 5
WHERE [TitleKey] = N'Tools'

-- 1.2.1503
Update [Metadata].[Column] 
Set [Name] = N'IsArchivedName', [DotNetType] = N'System.String', [StorageType] = N'nvarchar',
    [ScriptType] = N'string', [IsNullable] = 1
Where ColumnID = 737

-- 1.2.1508
CREATE TABLE [Config].[UserValueCategory] (
    [CategoryID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR(64)     NOT NULL,
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

DELETE FROM [Metadata].[Column]
WHERE [ViewID] = 71 AND [Name] LIKE 'FullAccount.%'

DELETE FROM [Metadata].[Column]
WHERE [ViewID] = 71 AND [Name] IN('PageCount', 'FullAccount')

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (719, 71, 'PageCount', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'Hidden', 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (720, 71, 'FullAccount', NULL, NULL, 'System.Object', '(n/a)', 'object', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1509
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (747, 71, 'SeriesNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (748, 71, 'SayyadStartNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (749, 69, 'SayyadNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 3, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1510
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (73, 'SourceApp', N'SourceApp', N'Base', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (739, 73, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (740, 73, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (741, 73, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (742, 73, 'Type', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (743, 73, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (744, 73, 'FiscalPeriodId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (745, 73, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (746, 73, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (40, 2, 1, N'ManageEntities,SourceApps', N'SourceApp', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
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
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (103, 97, 1, 73, 3, N'', N'/source-apps', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (297, 1, 103, N'Source and Application List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (298, 2, 103, N'فهرست منابع و مصارف')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (61, 53, 239, N'ManageSourceApps', '/treasury/source-apps', 'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.2.1512
DROP TABLE [Config].[UserValue]
GO

DROP TABLE [Config].[UserValueCategory]
GO

-- 1.2.1516
UPDATE [Auth].[PermissionGroup]
SET [SubsystemID] = 3
WHERE [PermissionGroupID] = 40

UPDATE [Metadata].[Column]
SET [Visibility] = N'AlwaysHidden', [DisplayIndex] = -1
WHERE ColumnID = 742

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (750, 73, 'TypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (751, 73, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1521
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (74, 'Payment', N'Payment', N'Operational', NULL, NULL, 0, 0)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (75, 'Receipt', N'Receipt', N'Operational', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
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
    VALUES (764, 75, 'PayReceiveNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
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
    VALUES (778, 74, 'PayReceiveNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (779, 74, 'Reference', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, 2, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (41, 3, 2, N'Payments', N'Payment', NULL)
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (42, 3, 2, N'Receipts', N'Receipt', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (246, 41, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (247, 41, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (248, 41, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (249, 41, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (250, 41, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (251, 41, N'NavigateEntities,Payments', 32, NULL)
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
    VALUES (258, 42, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (259, 42, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (260, 42, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (261, 42, N'Delete', 64, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (262, 42, N'NavigateEntities,Receipts', 32, NULL)
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
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (62, 52, NULL, N'PaymentOperations', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (63, 52, NULL, N'ReceiptOperations', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (64, 62, 248, N'NewPaymentForm', N'/treasury/payments/new', 'plus', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (65, 63, 259, N'NewReceiptForm', N'/treasury/receipts/new', 'plus', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (66, 62, 251, N'LastPaymentForm', N'/treasury/payments/last', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (67, 63, 262, N'LastReceiptForm', N'/treasury/receipts/last', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (68, 62, 246, N'PaymentFormbyNo', N'/treasury/payments/by-no', 'search', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (69, 63, 257, N'ReceiptFormbyNo', N'/treasury/receipts/by-no', 'search', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.2.1523
UPDATE [Metadata].[View]
SET [Name] = 'Receipt', EntityName = N'Receipt'
WHERE ViewID = 75

UPDATE [Auth].[PermissionGroup]
SET [Name] = N'Receipts', EntityName = N'Receipt'
WHERE PermissionGroupID = 42

UPDATE [Auth].[Permission]
SET [Name] = N'NavigateEntities,Receipts'
WHERE PermissionID = 262

UPDATE [Metadata].[Command]
SET TitleKey = REPLACE(TitleKey, 'Receival', 'Receipt'), RouteUrl = REPLACE(RouteUrl, 'receival', 'receipt')
WHERE TitleKey LIKE '%Receival%' 

-- 1.2.1524
UPDATE [Metadata].[Column]
SET IsNullable = 1
WHERE ColumnID = 750

-- 1.2.1532
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (76, 'PayReceiveAccount', N'PayReceiveAccount', N'Core', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (780, 76, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (781, 76, 'FullAccount', NULL, NULL, 'System.Object', '(n/a)', 'object', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (782, 76, 'Amount', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (783, 76, 'PayReceiveId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (784, 76, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (785, 76, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (786, 76, 'AccountId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (787, 76, 'DetailAccountId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (788, 76, 'CostCenterId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (789, 76, 'ProjectId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1536
UPDATE [Metadata].[Command]
SET [TitleKey] = 'SourceApps'
WHERE [TitleKey] = 'ManageSourceApps'

-- 1.2.1537
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (790, 72, 'SayyadStartNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (791, 72, 'SeriesNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

UPDATE [Metadata].[Column]
SET [DisplayIndex] = [DisplayIndex] + 2
WHERE [ViewID] = 72 and [ColumnID] > 726 and [ColumnID] < 738 

-- 1.2.1539
DELETE FROM [Metadata].[Column]
WHERE ViewID = 76

SET IDENTITY_INSERT [Metadata].[Column] ON
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
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1542
UPDATE [Metadata].[Column]
SET [Name] = N'PersonFullName', Expression = N'Person.FullName'
WHERE ViewID = 4 AND [Name] = N'PersonFirstName'

DELETE FROM [Metadata].[Column]
WHERE ViewID = 4 AND [Name] = N'PersonLastName'

-- 1.2.1543
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
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+T' WHERE TitleKey = 'TestBalance'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+I' WHERE TitleKey = 'ItemBalance'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+B' WHERE TitleKey = 'BalanceByAccount'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+R' WHERE TitleKey = 'ProfitLoss'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+K' WHERE TitleKey = 'BalanceSheet'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+C'   WHERE TitleKey = 'Companies'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+B' WHERE TitleKey = 'Branches'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+H'   WHERE TitleKey = 'Roles'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+W'   WHERE TitleKey = 'RowAccessSettings'
UPDATE Metadata.Command SET HotKey = 'Ctrl+K'       WHERE TitleKey = 'Settings'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+O' WHERE TitleKey = 'OperationLogs'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Shift+S' WHERE TitleKey = 'SystemIssue'
UPDATE Metadata.Command SET HotKey = 'Ctrl+Alt+N'   WHERE TitleKey = 'NewVoucher'
UPDATE Metadata.Command SET HotKey = NULL           WHERE TitleKey = 'ReportManagement'

-- 1.2.1545
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (77, 'PayReceiveCashAccount', N'PayReceiveCashAccount', N'Core', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (796, 77, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (797, 77, 'FullAccount', NULL, NULL, 'System.Object', 'int', 'Object', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (798, 77, 'IsBank', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 0, 1, 1, NULL, -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (799, 77, 'PayReceiveId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (800, 77, 'SourceAppId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, 1, N'(not set)', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (801, 77, 'BankOrderNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, 1, NULL, -1, NULL)
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
    VALUES (816, 77, 'Remarks', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, 1, NULL, 10, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1549
UPDATE [Metadata].[Column]
SET [DisplayIndex] = 11
WHERE ColumnID = 816 AND [Name] = N'Remarks'

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (817, 77, 'SourceApp', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, 1, NULL, 10, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1550
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (54, 'Export', NULL)
SET IDENTITY_INSERT [Metadata].[Operation] OFF

DELETE FROM [Config].[SysLogSetting]
GO

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

-- 1.2.1551
UPDATE [Metadata].[Column]
SET [Visibility] = N'AlwaysHidden'
WHERE ColumnID = 798 and [Name] = N'IsBank'

UPDATE [Metadata].[Column]
SET [DisplayIndex] = 12
WHERE ColumnID = 816 AND [Name] = N'Remarks'

UPDATE [Metadata].[Column]
SET [DisplayIndex] = 11
WHERE ColumnID = 801 AND [Name] = N'BankOrderNo'

-- 1.2.1552
UPDATE [Metadata].[Column]
SET [Visibility] = N'AlwaysHidden'
WHERE ColumnID = 800 AND [Name] = N'SourceAppId'

-- 1.2.1553
UPDATE [Reporting].[Report]
SET [ServiceUrl] = N'fperiods'
WHERE ReportID = 21 AND [Code] = N'Fiscal-Periods'

-- 1.2.1555
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+T' Where TitleKey = 'TestBalance'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+R' Where TitleKey = 'ProfitLoss'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+K' Where TitleKey = 'Users'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+L' Where TitleKey = 'OperationLogs'
UPDATE [Metadata].[ShortcutCommand] Set HotKey = 'Ctrl+Shift+Insert' where [Name] = 'NewRecord'
UPDATE Metadata.Command set HotKey = 'Ctrl+Alt+E' Where TitleKey = 'Branches'
UPDATE Metadata.Command set HotKey = NULL Where TitleKey = 'ReportManagement'

-- 1.2.1556
UPDATE [Metadata].[ShortcutCommand] Set HotKey = 'Ctrl+Shift+Delete' where [Name] = 'DeleteRecord'

-- 1.2.1559

-- Add permissions for new generic operations (Deactivate and Reactivate)...
SET IDENTITY_INSERT [Auth].[Permission] ON
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
SET IDENTITY_INSERT [Auth].[Permission] OFF

UPDATE [Metadata].[View]
SET [SearchUrl] = '/fperiods'
WHERE [Name] = 'FiscalPeriod'

UPDATE [Metadata].[View]
SET [SearchUrl] = '/branches'
WHERE [Name] = 'Branch'

DELETE FROM [Metadata].[Column]
WHERE ViewID = 1 AND [Name] = 'IsActive'

DELETE FROM [Metadata].[Column]
WHERE ViewID = 30 AND [Name] = 'IsActive'

UPDATE [Metadata].[Column]
SET [DisplayIndex] = [DisplayIndex] - 1
WHERE ViewID = 30 AND ColumnID >= 274 AND ColumnID <= 277

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (11, 1, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (278, 30, 'State', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, NULL, 7, NULL)
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
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1560
UPDATE [Metadata].[Column]
SET [Name] = 'SourceAppName'
WHERE ColumnID = 817 AND [Name] = N'SourceApp'

-- 1.2.1562
DELETE FROM [Reporting].[LocalReport]
WHERE ReportID = 12

DELETE FROM [Reporting].[Report]
WHERE ReportID = 12 AND Code = 'Operation-Logs'

-- 1.2.1566
SET IDENTITY_INSERT [Auth].[Permission] ON
-- افزودن دسترسی برگشت از ثبت مالی برای فرم دریافت و فرم پرداخت
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (282, 41, N'UndoRegister', 2048, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (283, 42, N'UndoRegister', 2048, NULL)
SET IDENTITY_INSERT [Auth].[Permission] OFF

-- 1.2.1567
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (855, 3, 'SourceAppName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'Hidden', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (856, 3, 'SourceAppId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF
GO

UPDATE [Metadata].[Column]
SET [DisplayIndex] = [DisplayIndex] + 1
WHERE [ColumnID] IN (43, 44, 45, 46, 47)

UPDATE [Core].[Version]
SET Number = '1.2.1567'

-- 1.2.1568
SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (104, 98, 1, 74, 3, N'', N'payments/{0}/receiver/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (105, 98, 1, 75, 3, N'', N'receipts/{0}/payer/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (106, 98, 1, 74, 3, N'', N'payments/{0}/cash/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (107, 98, 1, 75, 3, N'', N'receipts/{0}/cash/articles', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (299, 1, 104, N'Recipients List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (300, 2, 104, N'لیست دریافت کنندگان')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (301, 1, 105, N'Payers List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (302, 2, 105, N'لیست پرداخت کنندگان')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (303, 1, 106, N'Cash Accounts List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (304, 2, 106, N'لیست حساب‌های نقدی')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (305, 1, 107, N'Cash Accounts List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (306, 2, 107, N'لیست حساب‌های نقدی')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (182, 104, N'paymentId', N'paymentId', N'EQ', 'System.Int32', N'NumberBox', N'PaymentId', NULL, 1, NULL, N'PaymentId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (183, 105, N'receiptId', N'receiptId', N'EQ', 'System.Int32', N'NumberBox', N'ReceiptId', NULL, 1, NULL, N'ReceiptId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (184, 106, N'paymentId', N'paymentId', N'EQ', 'System.Int32', N'NumberBox', N'PaymentId', NULL, 1, NULL, N'PaymentId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (185, 107, N'receiptId', N'receiptId', N'EQ', 'System.Int32', N'NumberBox', N'ReceiptId', NULL, 1, NULL, N'ReceiptId', N'Route')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

-- 1.2.1571
SET IDENTITY_INSERT [Metadata].[Column] ON
-- افزودن فیلدهای راهبری صندوق اسناد
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (823, 70, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (824, 70, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (825, 70, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (826, 70, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)

-- افزودن فیلدهای راهبری منابع/مصارف
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (827, 73, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (828, 73, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (829, 73, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (830, 73, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)

-- افزودن فیلدهای راهبری ارز
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (831, 30, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (832, 30, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (833, 30, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 11, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (834, 30, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 12, NULL)

-- افزودن فیلدهای راهبری نرخ ارز
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (835, 31, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (836, 31, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (837, 31, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 10, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (838, 31, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 11, NULL)
	
-- افزودن فیلدهای راهبری حساب
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (839, 1, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 9, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (840, 1, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (841, 1, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 11, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (842, 1, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 12, NULL)

-- افزودن فیلدهای راهبری مرکز هزینه
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (843, 7, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (844, 7, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (845, 7, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (846, 7, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)

-- افزودن فیلدهای راهبری تفصیلی شناور
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (847, 6, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (848, 6, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (849, 6, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (850, 6, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)

-- افزودن فیلدهای راهبری پروژه
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (851, 8, 'CreatedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (852, 8, 'CreatedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (853, 8, 'ModifiedByName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (854, 8, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1572
UPDATE [Metadata].[Column]
SET [Name] = N'TextNo', [DotNetType] = 'System.Int64', [StorageType] = 'bigint', [Length] = 0, ScriptType ='number'
WHERE ColumnID IN(764,778) AND [Name] = N'PayReceiveNo'

UPDATE [Metadata].[Column]
SET [Name] = N'TextNo', [DotNetType] = 'System.Int64', [StorageType] = 'bigint', [Length] = 0, ScriptType = 'number'
WHERE ColumnID IN(713,726) AND [Name] = N'CheckBookNo'

-- 1.2.1575
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (78, 'VouchersByDate', N'Voucher', N'Operational', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
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
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.2.1576
UPDATE [Metadata].[Column]
SET [Visibility] = N'AlwaysHidden', [DisplayIndex] = -1
WHERE ColumnID = 100 AND [Name] = 'Password'

-- 1.2.1577
UPDATE [Metadata].[Column]
SET [DisplayIndex] = 5
WHERE ColumnID = 97 AND [Name] = 'Description'

UPDATE [Metadata].[Column]
SET [DisplayIndex] = 3
WHERE ColumnID = 98 AND [Name] = 'Server'

UPDATE [Metadata].[Column]
SET [DisplayIndex] = 4
WHERE ColumnID = 99 AND [Name] = 'UserName'

-- 1.2.1579
UPDATE [Metadata].[View]
SET [EntityName] = N'VouchersByDate'
WHERE ViewId = 78 and [Name] = 'VouchersByDate'

-- 1.2.1581
-- آپدیت مشکلات رفع شده متادیتا لوک آپ سند برای ثبت مالی
UPDATE [Metadata].[Column]
SET [Visibility] = NULL
WHERE ColumnID in (859, 860, 861)

UPDATE [Metadata].[Column]
SET [Type] = NULL
WHERE ColumnID = 859

-- 1.2.1583
-- افزودن دسترسی ها برای کنترل سیستم
SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (284, 30, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (285, 30, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (286, 30, N'Export', 8, NULL)
SET IDENTITY_INSERT [Auth].[Permission] OFF

-- اسکریپت‌های جامانده چاپ گزارش‌ها در کنترل سیستم
SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (108, 20, 1, 42, 2, N'', NULL, 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (109, 20, 1, 41, 2, N'', NULL, 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF
SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (307, 1, 108, N'Voucher Lines')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (308, 2, 108, N'آرتیکل‌های سند')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (309, 1, 109, N'Missing Voucher Numbers')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (310, 2, 109, N'شماره سندهای مفقود')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

-- 1.2.1585
UPDATE [Auth].[Permission]
SET [Flag] = 2
WHERE [Name] = 'Print' and PermissionID = 247

UPDATE [Auth].[Permission]
SET [Flag] = 4
WHERE [Name] = 'Create' and PermissionID = 248

UPDATE [Auth].[Permission]
SET [Flag] = 8
WHERE [Name] = 'Edit' and PermissionID = 249

UPDATE [Auth].[Permission]
SET [Flag] = 16
WHERE [Name] = 'Delete' and PermissionID = 250

UPDATE [Auth].[Permission]
SET [Flag] = 2
WHERE [Name] = 'Print' and PermissionID = 258

UPDATE [Auth].[Permission]
SET [Flag] = 4
WHERE [Name] = 'Create' and PermissionID = 259

UPDATE [Auth].[Permission]
SET [Flag] = 8
WHERE [Name] = 'Edit' and PermissionID = 260

UPDATE [Auth].[Permission]
SET [Flag] = 16
WHERE [Name] = 'Delete' and PermissionID = 261

-- 1.2.1588
DELETE FROM [Metadata].[Command]
WHERE [CommandID] >= 62 and [CommandID] <= 69

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (62, 52, NULL, N'ReceiptOperations', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (63, 52, NULL, N'PaymentOperations', NULL, 'folder-close', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (64, 62, 259, N'NewReceiptForm', N'/treasury/receipts/new', 'plus', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (65, 62, 262, N'LastReceiptForm', N'/treasury/receipts/last', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (66, 62, 257, N'ReceiptFormbyNo', N'/treasury/receipts/by-no', 'search', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (67, 63, 248, N'NewPaymentForm', N'/treasury/payments/new', 'plus', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (68, 63, 251, N'LastPaymentForm', N'/treasury/payments/last', 'list', NULL)
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (69, 63, 246, N'PaymentFormbyNo', N'/treasury/payments/by-no', 'search', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.2.1589
UPDATE [Auth].[PermissionGroup] 
SET [Name] = N'Receipts', [EntityName] = N'Receipt'
WHERE [PermissionGroupID] = 41

UPDATE [Auth].[PermissionGroup] 
SET [Name] = N'Payments', [EntityName] = N'Payment'
WHERE [PermissionGroupID] = 42

UPDATE [Auth].[Permission] 
SET [Name] = N'NavigateEntities,Receipts'
WHERE [PermissionID] = 251

UPDATE [Auth].[Permission] 
SET [Name] = N'NavigateEntities,Payments'
WHERE [PermissionID] = 262

DELETE [Reporting].[LocalReport]
WHERE [LocalReportID] >= 299 AND [LocalReportID] <= 306

DELETE [Reporting].[Parameter]
WHERE [ParamID] >= 182 AND [ParamID] <= 185

DELETE [Reporting].[Report]
WHERE [ReportID] >= 104 AND [ReportID] <= 107

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (104, 98, 1, 75, 3, N'', N'receipts/{0}/payer/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (105, 98, 1, 75, 3, N'', N'receipts/{0}/cash/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (106, 98, 1, 74, 3, N'', N'payments/{0}/receiver/articles', 0, 1, 1, 1, NULL)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (107, 98, 1, 74, 3, N'', N'payments/{0}/cash/articles', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (182, 104, N'receiptId', N'receiptId', N'EQ', 'System.Int32', N'NumberBox', N'ReceiptId', NULL, 1, NULL, N'ReceiptId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (183, 105, N'receiptId', N'receiptId', N'EQ', 'System.Int32', N'NumberBox', N'ReceiptId', NULL, 1, NULL, N'ReceiptId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (184, 106, N'paymentId', N'paymentId', N'EQ', 'System.Int32', N'NumberBox', N'PaymentId', NULL, 1, NULL, N'PaymentId', N'Route')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [Source])
    VALUES (185, 107, N'paymentId', N'paymentId', N'EQ', 'System.Int32', N'NumberBox', N'PaymentId', NULL, 1, NULL, N'PaymentId', N'Route')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
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
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

-- 1.2.1594
DELETE FROM [Metadata].[Command]
WHERE [CommandID] >= 62 and [CommandID] <= 69

SET IDENTITY_INSERT [Metadata].[Command] ON
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
SET IDENTITY_INSERT [Metadata].[Command] OFF





















































































































































































































































































































































-- 2.2.0
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (100001, 'Brand', N'Brand', N'Base', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
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
SET IDENTITY_INSERT [Metadata].[Column] OFF


SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (100001, 100000, 1, N'ManageEntities,Brands', N'Brand', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
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
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100007, 100001, 1, 100001, 100000, N'', N'brands', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100014, 1, 100007, N'Brnads List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100015, 2, 100007, N'فهرست برندها')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (100004, 100001, 100001, N'Brand', N'brand', NULL, NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF


-- 2.2.1
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (100002, 'Unit', N'Unit', N'Base', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
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
SET IDENTITY_INSERT [Metadata].[Column] OFF


SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (100002, 100000, 1, N'ManageEntities,Units', N'Unit', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
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
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100008, 100001, 1, 100002, 100000, N'', N'units', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100016, 1, 100008, N'Unit list')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100017, 2, 100008, N'لیست واحدها')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (100005, 100001, 100008, N'Units', N'/product-scope/products', NULL, NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF


-- 2.2.2
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (100003, 'Property', N'Property', N'Base', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
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


SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (100003, 100000, 1, N'ManageEntities,Properties', N'Property', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
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

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100009, 100001, 1, 100003, 100000, N'', N'properties', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100018, 1, 100009, N'Properties List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100019, 2, 100009, N'لیست ویژگی ها')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

UPDATE [Metadata].[Command]
SET [PermissionID] = 100001, [TitleKey] = N'Brands', [RouteUrl] = N'/product-scope/brands', [IconName] = 'list', [HotKey] = NULL
WHERE [CommandID] = 100004

UPDATE [Metadata].[Command]
SET [PermissionID] = 100008, [TitleKey] = N'Units', [RouteUrl] = N'/product-scope/units', [IconName] = NULL, [HotKey] = NULL
WHERE [CommandID] = 100005

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (100006, 100001, 100015, N'Property', N'/product-scope/properties', 'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF


-- 2.2.3
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (100004, 'Attribute', N'Attribute', N'Base', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100034, 100004, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100035, 100004, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100036, 100004, 'EnName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100037, 100004, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 1024, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100038, 100004, 'Type', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100039, 100004, 'IsActive', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100040, 100004, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100041, 100004, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100042, 100004, 'RowGuid', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100043, 100004, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF


SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (100004, 100000, 1, N'ManageEntities,Attributes', N'Attribute', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100022, 100004, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100023, 100004, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100024, 100004, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100025, 100004, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100026, 100004, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100027, 100004, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100028, 100004, N'Delete', 64, NULL)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100010, 100001, 1, 100004, 3, N'', N'attributes', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100020, 1, 100010, N'Attribute List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100021, 2, 100010, N'لیست خصوصیت ها')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (100007, 100001, 100022, N'Attribute', N'/product-scope/attributes', 'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF


-- 2.2.4
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], [FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])
    VALUES (100005, 'File', N'File', N'Base', NULL, NULL, 0, 0)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100044, 100005, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100045, 100005, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 2048, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100046, 100005, 'UniqeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 2048, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100047, 100005, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 2048, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100048, 100005, 'IsActive', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100049, 100005, 'FormFile', NULL, NULL, 'Microsoft.AspNetCore.Http.IFormFile', '', '', 0, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100050, 100005, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100051, 100005, 'RowGuid', NULL, NULL, 'System.Guid', '', '', 0, 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100052, 100005, 'ModifiedDate', NULL, 'Default', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF


SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], [Name], [EntityName], [Description])
    VALUES (100005, 100000, 1, N'ManageEntities,files', N'file', NULL)
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100029, 100005, N'View', 1, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100030, 100005, N'Filter', 2, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100031, 100005, N'Print', 4, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100032, 100005, N'Export', 8, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100033, 100005, N'Create', 16, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100034, 100005, N'Edit', 32, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])
    VALUES (100035, 100005, N'Delete', 64, NULL)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], [ResourceKeys])
    VALUES (100011, 100001, 1, 100005, 3, N'', N'Files', 0, 1, 1, 1, NULL)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100022, 1, 100011, N'File List')
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])
    VALUES (100023, 2, 100011, N'فهرست فایل ها')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], [RouteUrl], [IconName], [HotKey])
    VALUES (100008, 100001, 100029, N'Files', N'/product-scope/files', 'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

