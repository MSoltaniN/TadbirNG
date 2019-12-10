-- 1.1.666
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (30, 'Currency', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
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
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.670
UPDATE [Metadata].[Command]
SET ParentID = NULL
GO

DELETE FROM [Metadata].[Command]
GO

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
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (38, 37, 78, N'ReportManagement', N'/reports', N'list', N'Ctrl+R')
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.1.679
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (22, N'CurrencyRate', N'CurrencyRate')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
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

-- 1.1.680
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (31, 'CurrencyRate', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
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


-- 1.1.682
update metadata.command set RouteUrl = '/organization/fiscalperiod' where RouteUrl = '/fiscalperiod'
update metadata.command set RouteUrl = '/organization/branches' where RouteUrl = '/branches'
update metadata.command set RouteUrl = '/organization/companies' where RouteUrl = '/companies'
update metadata.command set RouteUrl = '/tadbir/reports' where RouteUrl = '/reports'
update metadata.command set RouteUrl = '/config/settings' where RouteUrl = '/settings'
update metadata.command set RouteUrl = '/admin/operation-log' where RouteUrl = '/operation-log'
update metadata.command set RouteUrl = '/admin/roles' where RouteUrl = '/roles'
update metadata.command set RouteUrl = '/admin/users' where RouteUrl = '/users'
update metadata.command set RouteUrl = '/admin/changePassword' where RouteUrl = '/changePassword'
update metadata.command set RouteUrl = '/admin/viewRowPermission' where RouteUrl = '/viewRowPermission'
update metadata.command set RouteUrl = '/finance/account' where RouteUrl = '/account'
update metadata.command set RouteUrl = '/finance/account-collection' where RouteUrl = '/account-collection'
update metadata.command set RouteUrl = '/finance/account-groups' where RouteUrl = '/account-groups'
update metadata.command set RouteUrl = '/finance/accountrelations' where RouteUrl = '/accountrelations'
update metadata.command set RouteUrl = '/finance/costCenter' where RouteUrl = '/costCenter'
update metadata.command set RouteUrl = '/finance/detailAccount' where RouteUrl = '/detailAccount'
update metadata.command set RouteUrl = '/finance/projects' where RouteUrl = '/projects'
update metadata.command set RouteUrl = '/finance/voucher' where RouteUrl = '/voucher'
update metadata.command set RouteUrl = '/finance/vouchers/new' where RouteUrl = '/vouchers/new'
update metadata.command set RouteUrl = '/finance/vouchers/by-no' where RouteUrl = '/vouchers/by-no'
update metadata.command set RouteUrl = '/finance/vouchers/last' where RouteUrl = '/vouchers/last'
update metadata.command set RouteUrl = '/finance/account-book' where RouteUrl = '/account-book'
update metadata.command set RouteUrl = '/finance/journal' where RouteUrl = '/journal'

-- 1.1.687
ALTER TABLE [Metadata].[Column]
ADD [GroupName] NVARCHAR(64) NULL
GO

SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (32, 'TestBalance2Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (33, 'TestBalance4Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (34, 'TestBalance6Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (35, 'TestBalance8Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (36, 'TestBalance10Column', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (266, 32, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (267, 32, N'AccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (268, 32, N'AccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (269, 32, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (270, 32, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (271, 32, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (272, 33, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (273, 33, N'AccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (274, 33, N'AccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (275, 33, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (276, 33, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (277, 33, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (278, 33, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (279, 33, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (280, 34, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (281, 34, N'AccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (282, 34, N'AccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (283, 34, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (284, 34, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (285, 34, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (286, 34, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (287, 34, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (288, 34, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (289, 34, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (290, 35, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (291, 35, N'AccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (292, 35, N'AccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (293, 35, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (294, 35, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (295, 35, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (296, 35, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (297, 35, N'OperationSumDebit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (298, 35, N'OperationSumCredit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (299, 35, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (300, 35, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (301, 35, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (302, 36, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (303, 36, N'AccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (304, 36, N'AccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (305, 36, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (306, 36, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (307, 36, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (308, 36, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (309, 36, N'OperationSumDebit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (310, 36, N'OperationSumCredit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (311, 36, N'CorrectionsDebit', N'Corrections', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (312, 36, N'CorrectionsCredit', N'Corrections', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (313, 36, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (314, 36, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (315, 36, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.688
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (316, 31, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.690
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (23, N'TestBalanceReport', N'TestBalance')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (103, 23, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (104, 23, N'Lookup', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (105, 23, N'Filter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (106, 23, N'Print', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (107, 23, N'ViewByBranch', 16)
SET IDENTITY_INSERT [Auth].[Permission] OFF

INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 103)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 104)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 105)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 106)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 107)

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (39, 1, NULL, N'FinancialReports', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (40, 39, 103, N'TestBalance', N'/finance/balance', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.1.692
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (317, 30, N'TaxCode', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.694
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (318, 31, N'BranchScope', NULL, N'System.Int16', N'smallint', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.695
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (319, 6, N'CurrencyId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.705
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (24, N'CurrencyBookReport', N'CurrencyBook')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON 
GO
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (108, 24, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (109, 24, N'Lookup', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (110, 24, N'Filter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (111, 24, N'Print', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (112, 24, N'Mark', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (113, 24, N'ViewByBranch', 32)
SET IDENTITY_INSERT [Auth].[Permission] OFF
GO

INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 108)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 109)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 110)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 111)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 112)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 113)

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (41, 20, 108, N'CurrencyBook', N'/finance/currency-book', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (37, N'CurrencyBook', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (38, N'CurrencyBookSingle', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (39, N'CurrencyBookSingleSummary', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (40, N'CurrencyBookSummary', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON 
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (320, 37, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (321, 37, N'CurrencyName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (322, 37, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (323, 37, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (324, 37, N'Balance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (325, 38, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (326, 38, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (327, 38, N'VoucherNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (328, 38, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (329, 38, N'Reference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (330, 38, N'BaseCurrencyDebit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (331, 38, N'BaseCurrencyCredit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (332, 38, N'BaseCurrencyBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (333, 38, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (334, 38, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (335, 38, N'Balance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (336, 38, N'CurrencyRate', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (337, 38, N'Mark', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (338, 38, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (339, 39, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (340, 39, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (341, 39, N'VoucherNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (342, 39, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (343, 39, N'Reference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (344, 39, N'BaseCurrencyDebit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (345, 39, N'BaseCurrencyCredit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (346, 39, N'BaseCurrencyBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (347, 39, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (348, 39, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (349, 39, N'Balance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (350, 39, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (351, 40, N'RowNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (352, 40, N'LineCount', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (353, 40, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (354, 40, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (355, 40, N'BaseCurrencyDebit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (356, 40, N'BaseCurrencyCredit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (357, 40, N'BaseCurrencyBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (358, 40, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (359, 40, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (360, 40, N'Balance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (361, 40, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 10, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.714
SET IDENTITY_INSERT [Reporting].Report ON

insert into Reporting.Report(ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
values(46,19,1,32,1,'TestBalance2Column','',0,1,1,1)

insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(1,46,'Test balance 2 columns')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(2,46,N'تراز آزمایشی ۲ ستونی')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(3,46,'Test balance 2 columns')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(4,46,'Test balance 2 columns')

insert into Reporting.Report(ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
values(47,19,1,33,1,'TestBalance4Column','',0,1,1,1)

insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(1,47,'Test balance 4 columns')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(2,47,N'تراز آزمایشی ۴ ستونی')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(3,47,'Test balance 4 columns')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(4,47,'Test balance 4 columns')

insert into Reporting.Report(ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
values(48,19,1,34,1,'TestBalance6Column','',0,1,1,1)

insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(1,48,'Test balance 6 columns')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(2,48,N'تراز آزمایشی ۶ ستونی')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(3,48,'Test balance 6 columns')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(4,48,'Test balance 6 columns')


insert into Reporting.Report(ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
values(49,19,1,35,1,'TestBalance8Column','',0,1,1,1)

insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(1,49,'Test balance 8 columns')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(2,49,N'تراز آزمایشی ۸ ستونی')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(3,49,'Test balance 8 columns')
insert into Reporting.LocalReport(LocaleID,ReportID,Caption)
values(4,49,'Test balance 8 columns')

SET IDENTITY_INSERT [Reporting].Report OFF
--'/testbal/ledger/6-col'


INSERT [Reporting].[Parameter] ([ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 46, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate')
GO
INSERT [Reporting].[Parameter] ([ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 46, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 46, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 46, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 46, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus')


INSERT [Reporting].[Parameter] ([ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 47, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate')
GO
INSERT [Reporting].[Parameter] ([ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 47, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 47, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 47, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 47, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus')


INSERT [Reporting].[Parameter] ([ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 48, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate')
GO
INSERT [Reporting].[Parameter] ([ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 48, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 48, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 48, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 48, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus')


INSERT [Reporting].[Parameter] ([ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 49, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate')
GO
INSERT [Reporting].[Parameter] ([ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 49, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 49, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 49, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo')
GO
INSERT [Reporting].[Parameter] ( [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES ( 49, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus')

-- 1.1.721
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (362, 32, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (363, 33, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (364, 34, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (365, 35, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (366, 36, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.723
ALTER TABLE Config.Setting 
ADD IsStandalone bit NOT NULL DEFAULT 1
GO

UPDATE Config.Setting
SET IsStandalone=0
WHERE TitleKey='ViewTreeSettings'

SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
VALUES (8, 'SystemConfigurationSettings', 2, 1, 'SystemConfig', N'{}', N'{}', 'SystemConfigurationDescription', 1)
SET IDENTITY_INSERT [Config].[Setting] OFF

-- 1.1.724
SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (9, 'TestBalanceSettings', 2, 1, 'TestBalanceConfig', N'{"addOpeningVoucherToInitBalance": false}', N'{"addOpeningVoucherToInitBalance": false}', 'TestBalanceSettingsDescription', 1)
SET IDENTITY_INSERT [Config].[Setting] OFF

-- 1.1.730
 UPDATE [Config].[Setting]
 SET DefaultValues=N'{"DefaultCurrencyNameKey":"CUnit_IranianRial","DefaultDecimalCount":2,"DefaultCalendar":0,"IsUseDefaultCoding":true}',
     [Values]=N'{"DefaultCurrencyNameKey":"CUnit_IranianRial","DefaultDecimalCount":2,"DefaultCalendar":0,"IsUseDefaultCoding":true}'
 where ModelType='SystemConfig'

-- 1.1.731
ALTER TABLE Config.CompanyDb
DROP COLUMN DbPath
GO

DELETE Metadata.[Column]
WHERE ColumnID=72

-- 1.1.733
UPDATE [Metadata].[Column]
SET DotNetType = N'System.String', ScriptType = N'string', StorageType = N'nvarchar', [Length] = 32
WHERE ViewID = 1 AND [Name] = 'TurnoverMode'

-- 1.1.737
DELETE FROM [Auth].[RolePermission]
WHERE RoleID = 1

DELETE FROM [Auth].[RoleCompany]
WHERE RoleID = 1

-- 1.1.744
DELETE FROM [Auth].[RolePermission]
WHERE PermissionID IN(42, 43, 44)

UPDATE [Auth].[Permission]
SET [Name] = N'View'
WHERE [Name] LIKE N'ViewEntities%'

UPDATE [Auth].[Permission]
SET [Name] = N'Create'
WHERE [Name] LIKE N'CreateEntity%'

UPDATE [Auth].[Permission]
SET [Name] = N'Edit'
WHERE [Name] LIKE N'EditEntity%'

UPDATE [Auth].[Permission]
SET [Name] = N'Delete'
WHERE [Name] LIKE N'DeleteEntity%'

UPDATE [Auth].[Permission]
SET [Name] = N'Manage'
WHERE [Name] LIKE N'ManageEntities%'

UPDATE [Auth].[Permission]
SET [Name] = N'Save'
WHERE [Name] LIKE N'SaveEntity%'

UPDATE [Auth].[Permission]
SET [Name] = N'SetDefault'
WHERE [Name] LIKE N'SetDefault%'

-- 1.1.746
DELETE FROM [Auth].[ViewRowPermission]
WHERE RoleID = 1

-- 1.1.762
DELETE FROM [Config].[UserSetting]
WHERE SettingID NOT IN(4,7)

DELETE FROM [Config].[Setting]
WHERE SettingID NOT IN(4,7)

-- 1.1.768
SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (42, 27, NULL, N'SystemIssue', N'/finance/system-issue', N'tasks', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Reporting].[SystemIssue] (
    [SystemIssueID]   INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]        INT              NULL,
    [PermissionID]    INT              NULL,
    [ViewID]          INT              NULL,
    [TitleKey]        NVARCHAR(64)     NOT NULL,
    [ApiUrl]      NVARCHAR(128)    NULL,	
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_SystemIssue_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Reporting_SystemIssue_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_SystemIssue] PRIMARY KEY CLUSTERED ([SystemIssueID] ASC)
    , CONSTRAINT [FK_Reporting_SystemIssue_Reporting_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Reporting].[SystemIssue]([SystemIssueID])
    , CONSTRAINT [FK_Reporting_SystemIssue_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission]([PermissionID])
    , CONSTRAINT [FK_Reporting_SystemIssue_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
)
GO

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (25, N'SystemIssueReport', N'SystemIssue')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (114, 25, N'View', 1)
SET IDENTITY_INSERT [Auth].[Permission] OFF


SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (41, N'NumberList', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (42, N'VoucherLineDetail', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON 
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (367, 41, N'Number', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) 
VALUES (368, 42, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (369, 42, N'VoucherDate', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (370, 42, N'VoucherNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
VALUES (371, 42, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) 
VALUES (372, 42, N'Debit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
 VALUES (373, 42, N'Credit', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) 
VALUES (374, 42, N'Description', NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 1, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
 VALUES (375, 42, N'AccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 1, 1, NULL, 0, NULL)
 INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) 
 VALUES (376, 42, N'AccountName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
 VALUES (377, 42, N'DetailAccountFullCode', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
 VALUES (378, 42, N'DetailAccountName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
 VALUES (379, 42, N'CostCenterFullCode', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
 VALUES (380, 42, N'CostCenterName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
 VALUES (381, 42, N'ProjectFullCode', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
 VALUES (382, 42, N'ProjectName', NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'Hidden', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) 
 VALUES (383, 42, N'CurrencyName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, N'Hidden', 14, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression]) 
 VALUES (384, 42, N'CurrencyValue', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 1, 1, 1, N'Hidden', 15, NULL)
 INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (385, 42, N'BranchId', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.771
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (26, N'ItemBalanceReport', N'ItemBalance')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (115, 26, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (116, 26, N'Lookup', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (117, 26, N'Filter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (118, 26, N'Print', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (119, 26, N'ViewByBranch', 16)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (43, 39, 115, N'ItemBalance', N'/finance/itembalance', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (43, 'DetailAccountBalance2Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (44, 'DetailAccountBalance4Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (45, 'DetailAccountBalance6Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (46, 'DetailAccountBalance8Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (47, 'DetailAccountBalance10Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (48, 'CostCenterBalance2Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (49, 'CostCenterBalance4Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (50, 'CostCenterBalance6Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (51, 'CostCenterBalance8Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (52, 'CostCenterBalance10Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (53, 'ProjectBalance2Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (54, 'ProjectBalance4Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (55, 'ProjectBalance6Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (56, 'ProjectBalance8Column', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (57, 'ProjectBalance10Column', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (386, 43, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (387, 43, N'DetailAccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (388, 43, N'DetailAccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (389, 43, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (390, 43, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (391, 43, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (392, 44, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (393, 44, N'DetailAccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (394, 44, N'DetailAccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (395, 44, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (396, 44, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (397, 44, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (398, 44, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (399, 44, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (400, 45, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (401, 45, N'DetailAccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (402, 45, N'DetailAccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (403, 45, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (404, 45, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (405, 45, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (406, 45, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (407, 45, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (408, 45, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (409, 45, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (410, 46, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (411, 46, N'DetailAccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (412, 46, N'DetailAccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (413, 46, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (414, 46, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (415, 46, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (416, 46, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (417, 46, N'OperationSumDebit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (418, 46, N'OperationSumCredit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (419, 46, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (420, 46, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (421, 46, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (422, 47, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (423, 47, N'DetailAccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (424, 47, N'DetailAccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (425, 47, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (426, 47, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (427, 47, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (428, 47, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (429, 47, N'OperationSumDebit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (430, 47, N'OperationSumCredit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (431, 47, N'CorrectionsDebit', N'Corrections', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (432, 47, N'CorrectionsCredit', N'Corrections', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (433, 47, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (434, 47, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (435, 47, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)

INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (436, 48, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (437, 48, N'CostCenterFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (438, 48, N'CostCenterName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (439, 48, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (440, 48, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (441, 48, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (442, 49, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (443, 49, N'CostCenterFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (444, 49, N'CostCenterName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (445, 49, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (446, 49, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (447, 49, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (448, 49, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (449, 49, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (450, 50, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (451, 50, N'CostCenterFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (452, 50, N'CostCenterName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (453, 50, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (454, 50, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (455, 50, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (456, 50, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (457, 50, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (458, 50, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (459, 50, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (460, 51, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (461, 51, N'CostCenterFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (462, 51, N'CostCenterName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (463, 51, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (464, 51, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (465, 51, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (466, 51, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (467, 51, N'OperationSumDebit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (468, 51, N'OperationSumCredit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (469, 51, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (470, 51, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (471, 51, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (472, 52, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (473, 52, N'CostCenterFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (474, 52, N'CostCenterName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (475, 52, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (476, 52, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (477, 52, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (478, 52, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (479, 52, N'OperationSumDebit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (480, 52, N'OperationSumCredit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (481, 52, N'CorrectionsDebit', N'Corrections', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (482, 52, N'CorrectionsCredit', N'Corrections', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (483, 52, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (484, 52, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (485, 52, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)

INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (486, 53, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (487, 53, N'ProjectFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (488, 53, N'ProjectName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (489, 53, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (490, 53, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (491, 53, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (492, 54, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (493, 54, N'ProjectFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (494, 54, N'ProjectName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (495, 54, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (496, 54, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (497, 54, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (498, 54, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (499, 54, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (500, 55, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (501, 55, N'ProjectFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (502, 55, N'ProjectName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (503, 55, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (504, 55, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (505, 55, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (506, 55, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (507, 55, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (508, 55, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (509, 55, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (510, 56, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (511, 56, N'ProjectFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (512, 56, N'ProjectName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (513, 56, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (514, 56, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (515, 56, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (516, 56, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (517, 56, N'OperationSumDebit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (518, 56, N'OperationSumCredit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (519, 56, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (520, 56, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (521, 56, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (522, 57, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (523, 57, N'ProjectFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (524, 57, N'ProjectName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (525, 57, N'StartBalanceDebit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (526, 57, N'StartBalanceCredit', N'StartBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (527, 57, N'TurnoverDebit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (528, 57, N'TurnoverCredit', N'Turnover', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (529, 57, N'OperationSumDebit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (530, 57, N'OperationSumCredit', N'OperationSum', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (531, 57, N'CorrectionsDebit', N'Corrections', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (532, 57, N'CorrectionsCredit', N'Corrections', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (533, 57, N'EndBalanceDebit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (534, 57, N'EndBalanceCredit', N'EndBalance', N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (535, 57, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 13, NULL)

INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (536, 43, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (537, 44, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (538, 45, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (539, 46, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (540, 47, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (541, 48, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (542, 49, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (543, 50, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (544, 51, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (545, 52, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (546, 53, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (547, 54, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (548, 55, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (549, 56, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (550, 57, N'VoucherReference', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF
