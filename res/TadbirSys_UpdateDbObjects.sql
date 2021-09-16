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
WHERE ColumnID = 72

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

-- 1.1.773
ALTER TABLE [Reporting].[SystemIssue]
ADD DeleteApiUrl NVARCHAR(128) NULL

-- 1.1.776
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

CREATE TABLE [Core].[SysOperationLog] (
    [SysOperationLogID]   INT              IDENTITY (1, 1) NOT NULL,
    [OperationID]         INT              NOT NULL,
    [SourceID]            INT              NOT NULL,
    [EntityTypeID]        INT              NOT NULL,
    [SourceListID]        INT              NOT NULL,
    [UserID]              INT              NOT NULL,
    [Date]                DATETIME         NOT NULL,
    [Time]                TIME(7)          NOT NULL,
    [EntityId]            INT              NULL,
    [Description]         NVARCHAR(MAX)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Core_SysOperationLog_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Core_SysOperationLog_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_SysOperationLog] PRIMARY KEY CLUSTERED ([SysOperationLogID] ASC)
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Core_SysOperationLog_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

-- 1.1.777
SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (1, N'CompanyDb')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (2, N'Role')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (3, N'RoleCompany')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (4, N'Setting')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (5, N'SysOperationLog')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (6, N'User')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (7, N'UserRole')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (8, N'ViewRowPermission')
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (1, N'View')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (2, N'Create')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (3, N'Edit')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (4, N'Delete')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (5, N'Filter')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (6, N'Print')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (7, N'Save')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (8, N'Archive')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

-- 1.1.778
DROP TABLE [Reporting].[SystemIssue]

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

SET IDENTITY_INSERT [Reporting].[SystemIssue] ON 
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (1, NULL, NULL, NULL, N'Accounting', NULL, NULL, 0)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (2, 1, NULL, NULL, N'VoucherIssues', NULL, NULL, 0)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (3, 1, NULL, NULL, N'AccountIssues', NULL, NULL, 0)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (4, 2, 26, 2, N'UnbalancedVouchers', N'/vouchers/unbalanced', N'/vouchers', 1)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (5, 2, 26, 2, N'VouchersWithNoArticle', N'/vouchers/no-article', N'/vouchers', 1)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (6, 2, 26, 42, N'ArticlesHavingZeroAmount', N'/vouchers/articles/sys-issue/zero-amount', N'/vouchers/articles', 1)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (7, 2, 26, 42, N'ArticlesWithMissingAccount', N'/vouchers/articles/sys-issue/miss-acc', N'/vouchers/articles', 1)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (8, 2, 26, 42, N'ArticlesWithInvalidAccountItems', N'/vouchers/articles/sys-issue/invalid-acc', N'/vouchers/articles', 1)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (9, 2, NULL, 41, N'MissingVoucherNumbers', N'/vouchers/miss-number', NULL, 0)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (10, 3, 26, 42, N'AccountsWithInvalidBalance', N'/vouchers/articles/sys-issue/invalid-acc-balance', NULL, 0)
GO
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
VALUES (11, 3, 26, 42, N'AccountsWithInvalidPeriodTurnover', N'/vouchers/articles/sys-issue/invalid-acc-turnover', NULL, 0)
GO
SET IDENTITY_INSERT [Reporting].[SystemIssue] OFF
GO

-- 1.1.782
DELETE FROM [Metadata].[Column]
WHERE ViewID = 13

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (81, 13, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (82, 13, N'UserName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (83, 13, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (84, 13, N'FiscalPeriodName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (85, 13, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (86, 13, N'Time', NULL, N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (87, 13, N'EntityTypeName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (551, 13, N'SourceName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (552, 13, N'SourceListName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (553, 13, N'EntityNo', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (554, 13, N'EntityCode', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (555, 13, N'EntityRef', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 1, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (556, 13, N'OperationName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (557, 13, N'Description', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (558, 13, N'CompanyName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 13, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

GO

-- 1.1.784
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (27, N'BalanceByAccountReport', N'BalanceByAccount')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (120, 27, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (121, 27, N'Lookup', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (122, 27, N'Filter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (123, 27, N'Print', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (124, 27, N'ViewByBranch', 16)
SET IDENTITY_INSERT [Auth].[Permission] OFF


SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (44, 39, 120, N'BalanceByAccount', N'/finance/balance-by-account',
 N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF


SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (58, N'BalanceByAccount', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF


SET IDENTITY_INSERT [Metadata].[Column] ON

INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (559, 58, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (560, 58, N'AccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (561, 58, N'AccountName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (562, 58, N'DetailAccountFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (563, 58, N'DetailAccountName', NULL, NULL,N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (564, 58, N'CostCenterFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (565, 58, N'CostCenterName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (566, 58, N'ProjectFullCode', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (567, 58, N'ProjectName', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (568, 58, N'Description', NULL, NULL, N'System.String', N'nvarchar', N'string', 512, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (569, 58, N'StartBalance', NULL,  N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (570, 58, N'Debit', NULL, N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (571, 58, N'Credit', NULL, N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (572, 58, N'EndBalance', NULL,  N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (573, 58, N'BranchName', NULL, NULL, N'System.String', N'nvarchar', N'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 14, NULL)

SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.788
DROP TABLE [Core].[OperationLog]
GO

DROP TABLE [Core].[SysOperationLog]
GO

CREATE TABLE [Core].[SysOperationLog] (
    [SysOperationLogID]   INT              IDENTITY (1, 1) NOT NULL,
    [OperationID]         INT              NOT NULL,
    [SourceID]            INT              NULL,
    [EntityTypeID]        INT              NULL,
    [SourceListID]        INT              NULL,
    [CompanyID]           INT              NOT NULL,
    [UserID]              INT              NOT NULL,
    [Date]                DATETIME         NOT NULL,
    [Time]                TIME(7)          NOT NULL,
    [EntityId]            INT              NULL,
    [Description]         NVARCHAR(MAX)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Core_SysOperationLog_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Core_SysOperationLog_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_SysOperationLog] PRIMARY KEY CLUSTERED ([SysOperationLogID] ASC)
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Core_SysOperationLog_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb]([CompanyID])
    , CONSTRAINT [FK_Core_SysOperationLog_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

-- 1.1.791
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (59, N'SysOperationLog', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (574, 59, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (575, 59, N'UserName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (576, 59, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (577, 59, N'Time', NULL, N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (578, 59, N'EntityTypeName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (579, 59, N'SourceName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (580, 59, N'SourceListName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (581, 59, N'OperationName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (582, 59, N'Description', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (583, 59, N'CompanyName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.792
CREATE TABLE [Core].[SysOperationLogArchive] (
    [SysOperationLogArchiveID]   INT              NOT NULL,
    [OperationID]                INT              NOT NULL,
    [SourceID]                   INT              NULL,
    [SourceListID]               INT              NULL,
    [EntityTypeID]               INT              NULL,
    [UserID]                     INT              NOT NULL,
    [CompanyID]                  INT              NOT NULL,
    [Date]                       DATETIME         NOT NULL,
    [Time]                       TIME(7)          NOT NULL,
    [EntityId]                   INT              NULL,
    [Description]                NVARCHAR(MAX)    NULL,
    [rowguid]                    UNIQUEIDENTIFIER CONSTRAINT [DF_Core_SysOperationLogArchive_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]               DATETIME         CONSTRAINT [DF_Core_SysOperationLogArchive_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_SysOperationLogArchive] PRIMARY KEY CLUSTERED ([SysOperationLogArchiveID] ASC)
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb]([CompanyID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

-- 1.1.797
DELETE FROM [Metadata].[Column]
WHERE ViewID = 13

SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (60, N'SysOperationLogArchive', 0, 0, N'')
INSERT INTO [Metadata].[View] (ViewID, Name, IsHierarchy, IsCartableIntegrated, FetchUrl) VALUES (61, N'OperationLogArchive', 0, 0, N'')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (584, 60, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (585, 60, N'UserName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (586, 60, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (587, 60, N'Time', NULL, N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (588, 60, N'EntityTypeName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (589, 60, N'SourceName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (590, 60, N'SourceListName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (591, 60, N'OperationName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (592, 60, N'Description', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (593, 60, N'CompanyName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)

INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (594, 61, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (595, 61, N'UserName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (596, 61, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (597, 61, N'FiscalPeriodName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (598, 61, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (599, 61, N'Time', NULL, N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (600, 61, N'EntityTypeName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (601, 61, N'SourceName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (602, 61, N'SourceListName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (603, 61, N'OperationName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (604, 61, N'Description', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (605, 61, N'CompanyName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 10, NULL)

INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (81, 13, N'Id', NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (82, 13, N'UserName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (83, 13, N'BranchName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (84, 13, N'FiscalPeriodName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (85, 13, N'Date', NULL, N'System.Date', N'datetime', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (86, 13, N'Time', NULL, N'System.TimeSpan', N'int', N'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (87, 13, N'EntityTypeName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (551, 13, N'SourceName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (552, 13, N'SourceListName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (553, 13, N'OperationName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (554, 13, N'Description', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (555, 13, N'CompanyName', NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 1, 1, NULL, 10, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.801
DELETE FROM [Metadata].[Column]
GO

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
    VALUES (5, 1, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (6, 1, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (7, 1, 'FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (8, 1, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (9, 1, 'Level', NULL, NULL, 'System.Int16', 'smallint', '', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (10, 1, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (11, 1, 'IsActive', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, N'Hidden', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (12, 1, 'IsCurrencyAdjustable', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, N'Hidden', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (13, 1, 'TurnoverMode', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, N'Hidden', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (14, 2, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (15, 2, 'StatusId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (16, 2, 'Type', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (17, 2, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (18, 2, 'No', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
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
    VALUES (34, 3, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (43, 3, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (44, 3, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (45, 3, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (46, 3, 'CurrencyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (47, 3, 'CurrencyValue', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysVisible', 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (48, 4, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (49, 4, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (50, 4, 'UserName', NULL, NULL, 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (51, 4, 'LastLoginDate', NULL, NULL, 'System.DateTime', 'datetime', 'DateTime', 0, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (52, 4, 'IsEnabled', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (53, 4, 'PersonFirstName', NULL, NULL, 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 0, 1, 1, NULL, 4, 'Person.FirstName')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (54, 4, 'PersonLastName', NULL, NULL, 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 0, 1, 1, NULL, 5, 'Person.LastName')
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (55, 5, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (62, 6, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (70, 7, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (78, 8, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (84, 9, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (90, 10, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (91, 10, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (92, 10, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (93, 11, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (94, 11, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (95, 11, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (96, 11, 'DbName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (97, 11, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (98, 11, 'Server', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (99, 11, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, N'AlwaysVisible', 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (100, 11, 'Password', NULL, NULL, 'System.String', 'nvarchar', 'string', 32, 0, 0, 1, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (101, 12, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (102, 12, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (103, 12, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (104, 12, 'Category', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (105, 12, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 1, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (106, 13, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (107, 13, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (108, 13, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (109, 13, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (110, 13, 'FiscalPeriodName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (111, 13, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (112, 13, 'Time', NULL, NULL, 'System.TimeSpan', 'int', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (113, 13, 'EntityTypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (114, 13, 'SourceName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (115, 13, 'SourceListName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (116, 13, 'OperationName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (117, 13, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (118, 13, 'CompanyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (119, 14, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (120, 14, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (121, 14, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (122, 14, 'FullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 256, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (123, 15, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (124, 15, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (125, 15, 'VoucherNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
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
    VALUES (133, 16, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (134, 16, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (135, 16, 'VoucherNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
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
    VALUES (149, 17, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (150, 17, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (151, 17, 'VoucherNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
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
    VALUES (158, 18, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (159, 18, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (160, 18, 'VoucherNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
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
    VALUES (167, 19, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (174, 20, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (182, 21, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (190, 22, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (191, 22, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (192, 22, 'VoucherNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
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
    VALUES (200, 23, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (201, 23, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (202, 23, 'VoucherNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
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
    VALUES (216, 24, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (217, 24, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (218, 24, 'VoucherNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
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
    VALUES (225, 25, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (226, 25, 'VoucherDate', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (227, 25, 'VoucherNo', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
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
    VALUES (234, 26, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (241, 27, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (247, 27, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (248, 27, 'Mark', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (249, 27, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (250, 28, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (256, 28, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (257, 28, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (258, 29, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (264, 29, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
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
    VALUES (270, 30, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (271, 30, 'Name', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (272, 30, 'Code', NULL, NULL, 'System.String', 'nvarchar', 'string', 8, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (273, 30, 'Country', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (274, 30, 'MinorUnit', NULL, NULL, 'System.String', 'nvarchar', 'string', 16, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (275, 30, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (276, 30, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (277, 30, 'DecimalCount', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'Hidden', 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (278, 30, 'IsActive', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 0, 1, 1, N'Hidden', 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (279, 31, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (280, 31, 'CurrencyId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (281, 31, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (282, 31, 'BranchScope', NULL, NULL, 'System.Int16', 'smallint', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (283, 31, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (284, 31, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (285, 31, 'Time', NULL, NULL, 'System.TimeSpan', 'time', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (286, 31, 'Multiplier', NULL, NULL, 'System.decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (287, 31, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (288, 31, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 1, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (289, 32, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (296, 33, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (305, 34, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (316, 35, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (329, 36, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (344, 37, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (345, 37, 'CurrencyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (346, 37, 'Debit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (347, 37, 'Credit', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (348, 37, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (349, 38, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (363, 39, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (375, 40, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (386, 41, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (387, 41, 'Number', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (388, 42, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (389, 42, 'BranchId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (390, 42, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (407, 43, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (414, 44, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (423, 45, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (434, 46, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (447, 47, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (462, 48, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (469, 49, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (478, 50, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (489, 51, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (502, 52, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (517, 53, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (524, 54, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (533, 55, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (544, 56, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (557, 57, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
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
    VALUES (572, 58, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (573, 58, 'AccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (574, 58, 'AccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (575, 58, 'DetailAccountFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (576, 58, 'DetailAccountName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (577, 58, 'CostCenterFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (578, 58, 'CostCenterName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (579, 58, 'ProjectFullCode', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (580, 58, 'ProjectName', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (581, 58, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 9, NULL)
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
    VALUES (587, 59, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (588, 59, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (589, 59, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (590, 59, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (591, 59, 'Time', NULL, NULL, 'System.TimeSpan', 'int', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (592, 59, 'EntityTypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (593, 59, 'SourceName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (594, 59, 'SourceListName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (595, 59, 'OperationName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (596, 59, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (597, 59, 'CompanyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (598, 60, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (599, 60, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (600, 60, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (601, 60, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (602, 60, 'Time', NULL, NULL, 'System.TimeSpan', 'int', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (603, 60, 'EntityTypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (604, 60, 'SourceName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (605, 60, 'SourceListName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (606, 60, 'OperationName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (607, 60, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (608, 60, 'CompanyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (609, 61, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (610, 61, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (611, 61, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (612, 61, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (613, 61, 'FiscalPeriodName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (614, 61, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (615, 61, 'Time', NULL, NULL, 'System.TimeSpan', 'int', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (616, 61, 'EntityTypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (617, 61, 'SourceName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (618, 61, 'SourceListName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (619, 61, 'OperationName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (620, 61, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 10, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (621, 61, 'CompanyName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 11, NULL)

SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.805
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT [Metadata].[Operation] ([OperationID], [Name]) VALUES(21, N'GroupDelete')
INSERT [Metadata].[Operation] ([OperationID], [Name]) VALUES(22, N'FailedLogin')
INSERT [Metadata].[Operation] ([OperationID], [Name]) VALUES(23, N'CompanyLogin')
INSERT [Metadata].[Operation] ([OperationID], [Name]) VALUES(24, N'SwitchFiscalPeriod')
INSERT [Metadata].[Operation] ([OperationID], [Name]) VALUES(25, N'SwitchBranch')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

DROP TABLE [Core].[SysOperationLog]
GO

DROP TABLE [Core].[SysOperationLogArchive]
GO

CREATE TABLE [Core].[SysOperationLog] (
    [SysOperationLogID]   INT              IDENTITY (1, 1) NOT NULL,
    [OperationID]         INT              NOT NULL,
    [SourceID]            INT              NULL,
    [EntityTypeID]        INT              NULL,
    [SourceListID]        INT              NULL,
    [CompanyID]           INT              NULL,
    [UserID]              INT              NULL,
    [Date]                DATETIME         NOT NULL,
    [Time]                TIME(7)          NOT NULL,
    [EntityId]            INT              NULL,
    [Description]         NVARCHAR(MAX)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Core_SysOperationLog_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Core_SysOperationLog_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_SysOperationLog] PRIMARY KEY CLUSTERED ([SysOperationLogID] ASC)
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Core_SysOperationLog_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Core_SysOperationLog_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb]([CompanyID])
    , CONSTRAINT [FK_Core_SysOperationLog_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

CREATE TABLE [Core].[SysOperationLogArchive] (
    [SysOperationLogArchiveID]   INT              NOT NULL,
    [OperationID]                INT              NOT NULL,
    [SourceID]                   INT              NULL,
    [SourceListID]               INT              NULL,
    [EntityTypeID]               INT              NULL,
    [UserID]                     INT              NULL,
    [CompanyID]                  INT              NULL,
    [Date]                       DATETIME         NOT NULL,
    [Time]                       TIME(7)          NOT NULL,
    [EntityId]                   INT              NULL,
    [Description]                NVARCHAR(MAX)    NULL,
    [rowguid]                    UNIQUEIDENTIFIER CONSTRAINT [DF_Core_SysOperationLogArchive_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]               DATETIME         CONSTRAINT [DF_Core_SysOperationLogArchive_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_SysOperationLogArchive] PRIMARY KEY CLUSTERED ([SysOperationLogArchiveID] ASC)
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[View]([ViewID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb]([CompanyID])
    , CONSTRAINT [FK_Core_SysOperationLogArchive_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

-- 1.1.813
update [Metadata].[Column]
set [Name]=N'AccountDescription'
where ColumnId=581

update [Metadata].[Column]
set [Visibility] = N'AlwaysVisible'
where ColumnId in (573,574,575,576,577,578,579,580)

delete [Config].[UserSetting]
where ViewID=58

-- 1.1.816
UPDATE [Metadata].[Column]
SET [IsNullable] = 1
WHERE [Name] = 'RowNo'

-- 1.1.822
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (26, N'AssignRole')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (27, N'AssignUser')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (28, N'BranchAccess')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (29, N'FiscalPeriodAccess')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (7, N'AppLogin')
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (8, N'AppEnvironment')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

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

SET IDENTITY_INSERT [Config].[SysLogSetting] ON
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (1, NULL, 1, 1, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (2, NULL, 1, 2, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (3, NULL, 1, 3, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (4, NULL, 1, 4, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (5, NULL, 6, 1, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (6, NULL, 6, 2, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (7, NULL, 6, 3, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (8, NULL, 6, 26, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (9, NULL, 2, 1, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (10, NULL, 2, 2, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (11, NULL, 2, 3, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (12, NULL, 2, 4, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (13, NULL, 2, 27, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (14, NULL, 2, 28, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (15, NULL, 2, 29, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (16, NULL, 4, 1, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (17, NULL, 4, 7, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (18, NULL, 8, 1, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (19, NULL, 8, 7, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (20, NULL, 5, 1, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (21, NULL, 5, 4, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (22, NULL, 5, 8, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (23, NULL, 5, 6, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (24, 7, NULL, 22, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (25, 8, NULL, 23, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (26, 8, NULL, 24, 0)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (27, 8, NULL, 25, 0)
SET IDENTITY_INSERT [Config].[SysLogSetting] OFF

-- 1.1.826
UPDATE [Metadata].[Column]
SET [DotNetType] = 'System.Int32', [StorageType] = 'int', [ScriptType] = 'number', [Length] = 0
WHERE ViewID = 2 AND [Name] = 'No'

UPDATE [Metadata].[Column]
SET [AllowFiltering] = 0, [AllowSorting] = 0
WHERE [Name] = 'RowNo'

-- 1.1.828
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName])
    VALUES (28, N'SysOperationLog', N'SysOperationLog')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName])
    VALUES (29, N'LogSetting', N'LogSetting')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (125, 17, N'Archive', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (126, 17, N'ViewArchive', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (127, 17, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (128, 28, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (129, 28, N'Archive', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (130, 28, N'ViewArchive', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (131, 28, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (132, 29, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag])
    VALUES (133, 29, N'Manage', 2)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (45, 27, 125, N'LogSettings', N'/admin/log-settings', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.1.832
SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (10, N'SystemSettings')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

-- 1.1.833
UPDATE [Metadata].[Command]
SET PermissionID = 132
WHERE CommandID = 45

/* Delete Company permissions that are Admin-Only */
DELETE [Auth].[Permission]
WHERE [GroupID] = 9 AND Flag <> 1

-- 1.1.834
/* Delete Delete Log permissions that are Admin-Only */
DELETE [Auth].[Permission]
WHERE PermissionID IN (127, 131)

-- 1.1.835
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (30, N'ViewArchive')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[SysLogSetting] ON
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (28, NULL, 5, 30, 0)
SET IDENTITY_INSERT [Config].[SysLogSetting] OFF

-- 1.1.844
DELETE FROM [Metadata].[EntityType]
WHERE [Name] IN ('RoleCompany', 'UserRole')

-- 1.1.845
ALTER TABLE [Core].[SysOperationLog]
ADD [EntityCode] NVARCHAR(256) NULL
GO

ALTER TABLE [Core].[SysOperationLog]
ADD [EntityName] NVARCHAR(256) NULL
GO

ALTER TABLE [Core].[SysOperationLog]
ADD [EntityDescription] NVARCHAR(1024) NULL
GO

ALTER TABLE [Core].[SysOperationLog]
ADD [EntityNo] INT NULL
GO

ALTER TABLE [Core].[SysOperationLog]
ADD [EntityDate] DATETIME NULL
GO

ALTER TABLE [Core].[SysOperationLogArchive]
ADD [EntityCode] NVARCHAR(256) NULL
GO

ALTER TABLE [Core].[SysOperationLogArchive]
ADD [EntityName] NVARCHAR(256) NULL
GO

ALTER TABLE [Core].[SysOperationLogArchive]
ADD [EntityDescription] NVARCHAR(1024) NULL
GO

ALTER TABLE [Core].[SysOperationLogArchive]
ADD [EntityNo] INT NULL
GO

ALTER TABLE [Core].[SysOperationLogArchive]
ADD [EntityDate] DATETIME NULL
GO

-- 1.1.846
DELETE FROM [Metadata].[Column]
WHERE ViewID = 4 AND Name = 'Password'

DELETE FROM [Metadata].[Column]
WHERE ViewID IN(13, 59, 60, 61)

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (106, 13, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 1, 1, 1, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (107, 13, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (108, 13, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (109, 13, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (110, 13, 'FiscalPeriodName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (111, 13, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (112, 13, 'Time', NULL, NULL, 'System.TimeSpan', 'int', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
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
    VALUES (595, 59, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (596, 59, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (597, 59, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (598, 59, 'Time', NULL, NULL, 'System.TimeSpan', 'int', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
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
    VALUES (611, 60, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (612, 60, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (613, 60, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (614, 60, 'Time', NULL, NULL, 'System.TimeSpan', 'int', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 3, NULL)
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
    VALUES (627, 61, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (628, 61, 'UserName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (629, 61, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (630, 61, 'FiscalPeriodName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (631, 61, 'Date', NULL, NULL, 'System.Date', 'datetime', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (632, 61, 'Time', NULL, NULL, 'System.TimeSpan', 'int', 'Date', 0, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)
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
SET IDENTITY_INSERT [Metadata].[Column] OFF


-- 1.1.860

Update Metadata.Command set RouteUrl = '/finance/vouchers/opening-voucher' Where TitleKey = 'IssueOpeningVoucher'
Update Metadata.Command set RouteUrl = '/finance/vouchers/closing-voucher' Where TitleKey = 'IssueClosingVoucher'

-- 1.1.862
UPDATE [Config].[SysLogSetting]
SET IsEnabled = 1

-- 1.1.884
Update [MetaData].[Column] set AllowFiltering = 0 where ViewId in (13,59,60,61) And [Name] = 'RowNo'

-- 1.1.891
Update [MetaData].[Command] set RouteUrl = '/finance/vouchers/close-temp-accounts' Where TitleKey = 'ClosingTempAccounts'

-- 1.1.911
UPDATE [MetaData].[Column]
SET [Type] = 'Money'
WHERE ViewID = 31 AND [Name] = 'Multiplier'


-- 1.1.915
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (647, 2, 'IsBalanced', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (648, 2, 'ConfirmerName', NULL, NULL, 'System.String', 'nvarchar', 'string', 120, 0, 0, 1, 1, 1, NULL, 9, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (649, 2, 'ApproverName', NULL, NULL, 'System.String', 'nvarchar', 'string', 120, 0, 0, 1, 1, 1, NULL, 10, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description]) VALUES (134, 7, N'GroupCheck', 65536, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description]) VALUES (135, 7, N'UndoGroupCheck', 131072, NULL)
SET IDENTITY_INSERT [Auth].[Permission] OFF

-- 1.1.916
SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description]) VALUES (136, 7, N'GroupFinalize', 262144, NULL)
SET IDENTITY_INSERT [Auth].[Permission] OFF

-- 1.1.917
SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description]) VALUES (137, 7, N'ConfirmGroup', 1048576, NULL)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description]) VALUES (138, 7, N'UnConfirmGroup', 2097152, NULL)
SET IDENTITY_INSERT [Auth].[Permission] OFF

-- 1.1.929
SET IDENTITY_INSERT [Metadata].[View] ON 
INSERT INTO [Metadata].[View] ([ViewID], [Name], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (62, 'ProfitLoss', 0, 0, NULL, '', NULL)
SET IDENTITY_INSERT [Metadata].[View] OFF 

SET IDENTITY_INSERT [Metadata].[Column] ON 
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (650, 62, 'Category', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (651, 62, 'Account', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (652, 62, 'StartBalance', NULL, NULL, 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (653, 62, 'PeriodTurnover', NULL, NULL, 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (654, 62, 'EndBalance', NULL, NULL, 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (655, 62, 'Balance', NULL, NULL, 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, 'Hidden', 5, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF 

-- 1.1.930
ALTER TABLE [Config].[CompanyDb] 
ADD [IsActive] BIT NOT NULL
CONSTRAINT DF_Config_CompanyDb_IsActive DEFAULT 1
WITH VALUES;
GO

-- 1.1.931
SET IDENTITY_INSERT [Metadata].[Column] ON 
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (656, 2, 'IsVerified', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, NULL, 11, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (657, 2, 'IsApproved', NULL, NULL, 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 1, 1, 1, NULL, 12, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (658, 2, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 120, 0, 0, 1, 1, 1, NULL, 13, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (659, 2, 'IssuerName', NULL, NULL, 'System.String', 'nvarchar', 'string', 120, 0, 0, 1, 1, 1, NULL, 14, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.936
UPDATE [Metadata].[Column]
SET Name = 'Group'
WHERE ViewID = 62 AND Name = 'Category'

-- 1.1.952
UPDATE [Metadata].[Column]
SET Name = 'IsConfirmed'
WHERE ViewID = 2 AND Name = 'IsVerified'

-- 1.1.959
SET IDENTITY_INSERT [Metadata].[View] ON 
INSERT INTO [Metadata].[View] ([ViewID], [Name], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (63, 'GroupActionResult', 0, 0, NULL, '', NULL)
SET IDENTITY_INSERT [Metadata].[View] OFF 

SET IDENTITY_INSERT [Metadata].[Column] ON 
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
SET IDENTITY_INSERT [Metadata].[Column] OFF 

-- 1.1.962
SET IDENTITY_INSERT [Metadata].[View] ON 
INSERT INTO [Metadata].[View] ([ViewID], [Name], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (64, 'ProfitLossSimple', 0, 0, NULL, '', NULL)
SET IDENTITY_INSERT [Metadata].[View] OFF 

SET IDENTITY_INSERT [Metadata].[Column] ON 
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (666, 64, 'Category', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (667, 64, 'Account', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 1, 0, 0, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (668, 64, 'Balance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 2, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF 

-- 1.1.970

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT Reporting.Report(ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
    VALUES (50,19,1,43,1,'DetailAccountBalance2Column','',0,1,1,1)
INSERT Reporting.Report(ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
    VALUES (51,19,1,44,1,'DetailAccountBalance4Column','',0,1,1,1)
INSERT Reporting.Report(ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
    VALUES (52,19,1,45,1,'DetailAccountBalance6Column','',0,1,1,1)
INSERT Reporting.Report(ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
    VALUES (53,19,1,46,1,'DetailAccountBalance8Column','',0,1,1,1)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (197, 1,50,'Detail account balance 8 columns')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (198, 2,50,N'گردش مانده سطوح شناور')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (199, 3,50,'Detail account 8 columns')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (200, 4,50,'Detail account 8 columns')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (201, 1,51,'Detail account balance 8 columns')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (202, 2,51,N'گردش مانده سطوح شناور')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (203, 3,51,'Detail account 8 columns')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (204, 4,51,'Detail account 8 columns')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (205, 1,52,'Detail account balance 8 columns')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (206, 2,52,N'گردش مانده سطوح شناور')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (207, 3,52,'Detail account 8 columns')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (208, 4,52,'Detail account 8 columns')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (209, 1,53,'Detail account balance 8 columns')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (210, 2,53,N'گردش مانده سطوح شناور')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (211, 3,53,'Detail account 8 columns')
INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
    VALUES (212, 4,53,'Detail account 8 columns')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (91, 50, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (92, 50, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (93, 50, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (94, 50, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (95, 50, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus')

INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (96, 51, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (97, 51, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (98, 51, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (99, 51, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (100, 51, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus')

INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (101, 52, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (102, 52, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (103, 52, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (104, 52, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (105, 52, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus')

INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (106, 53, N'fromDate', N'from', N'EQ', N'System.DateTime', N'QueryString', N'FromDate', NULL, NULL, NULL, N'FromDate')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (107, 53, N'toDate', N'to', N'EQ', N'System.DateTime', N'QueryString', N'ToDate', NULL, NULL, NULL, N'ToDate')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (108, 53, N'fromNo', N'from', N'EQ', N'System.Int32', N'QueryString', N'FromNo', NULL, NULL, NULL, N'FromNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (109, 53, N'toNo', N'to', N'EQ', N'System.Int32', N'QueryString', N'ToNo', NULL, NULL, NULL, N'ToNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey]) VALUES (110, 53, N'VoucherStatus', N'VoucherStatusId', N'EQ', N'System.Int32', N'TextBox', N'VoucherStatus', NULL, NULL, NULL, N'VoucherStatus')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

-- 1.1.975
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (669, 2, 'OriginName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 15, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.977
-- Refreshing security metadata...

ALTER TABLE [Metadata].[Command]
DROP CONSTRAINT [FK_Metadata_Command_Metadata_Parent]
GO

DELETE FROM [Metadata].[Command]

DELETE FROM [Reporting].[SystemIssue]

DELETE FROM [Auth].[RolePermission]

DELETE FROM [Auth].[Permission]

DELETE FROM [Auth].[PermissionGroup]

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (1, N'ManageEntities,Accounts', N'Account')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (2, N'ManageEntities,DetailAccounts', N'DetailAccount')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (3, N'ManageEntities,CostCenters', N'CostCenter')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (4, N'ManageEntities,Projects', N'Project')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (5, N'ManageEntities,FiscalPeriods', N'FiscalPeriod')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (6, N'ManageEntities,Currencies', N'Currency')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (7, N'Vouchers', N'Voucher')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (8, N'ManageEntities,Vouchers', N'Voucher')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (9, N'ManageEntities,Branches', N'Branch')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (10, N'ManageEntities,Companies', N'Company')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (11, N'ManageEntities,Users', N'User')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (12, N'ManageEntities,Roles', N'Role')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (13, N'ManageEntities,AccountGroups', N'AccountGroup')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (14, N'ManageEntities,AccountCollections', N'AccountCollection')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (15, N'ManageEntities,OperationLogs', N'OperationLog')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (16, N'ManageEntities,SysOperationLogs', N'SysOperationLog')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (17, N'ManageEntities,Reports', N'Report')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (18, N'ManageEntities,UserReports', N'UserReport')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (19, N'AccountRelations', N'AccountRelations')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (20, N'Settings', N'Setting')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (21, N'LogSetting', N'LogSetting')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (22, N'RowAccessSettings', N'RowAccess')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (23, N'CurrencyRate', N'CurrencyRate')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (24, N'JournalReport', N'Journal')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (25, N'AccountBookReport', N'AccountBook')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (26, N'TestBalanceReport', N'TestBalance')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (27, N'CurrencyBookReport', N'CurrencyBook')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (28, N'ItemBalanceReport', N'ItemBalance')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (29, N'BalanceByAccountReport', N'BalanceByAccount')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (30, N'SystemIssueReport', N'SystemIssue')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (31, N'ProfitLossReport', N'ProfitLoss')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (1, 1, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (2, 1, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (3, 1, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (4, 1, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (5, 1, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (6, 1, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (7, 2, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (8, 2, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (9, 2, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (10, 2, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (11, 2, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (12, 2, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (13, 3, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (14, 3, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (15, 3, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (16, 3, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (17, 3, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (18, 3, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (19, 4, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (20, 4, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (21, 4, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (22, 4, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (23, 4, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (24, 4, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (25, 5, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (26, 5, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (27, 5, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (28, 5, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (29, 5, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (30, 5, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (31, 5, N'AssignRolesToEntity,FiscalPeriod', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (32, 6, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (33, 6, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (34, 6, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (35, 6, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (36, 6, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (37, 6, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (38, 6, N'ChangeStatus', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (39, 7, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (40, 7, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (41, 7, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (42, 7, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (43, 7, N'Print', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (44, 7, N'CreateLine', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (45, 7, N'EditLine', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (46, 7, N'DeleteLine', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (47, 7, N'Check', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (48, 7, N'UndoCheck', 512)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (49, 7, N'Confirm', 1024)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (50, 7, N'UndoConfirm', 2048)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (51, 7, N'Approve', 4096)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (52, 7, N'UndoApprove', 8192)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (53, 7, N'Finalize', 16384)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (54, 7, N'NavigateEntities,Vouchers', 32768)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (55, 8, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (56, 8, N'Print', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (57, 8, N'GroupCheck', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (58, 8, N'GroupUndoCheck', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (59, 8, N'GroupConfirm', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (60, 8, N'GroupUndoConfirm', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (61, 8, N'GroupFinalize', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (62, 9, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (63, 9, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (64, 9, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (65, 9, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (66, 9, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (67, 9, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (68, 9, N'AssignRolesToEntity,Branch', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (69, 10, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (70, 10, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (71, 10, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (72, 11, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (73, 11, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (74, 11, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (75, 11, N'Filter', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (76, 11, N'Print', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (77, 11, N'AssignRolesToEntity,User', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (78, 12, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (79, 12, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (80, 12, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (81, 12, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (82, 12, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (83, 12, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (84, 12, N'AssignEntityToRole,User', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (85, 12, N'AssignEntityToRole,Branch', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (86, 12, N'AssignEntityToRole,FiscalPeriod', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (87, 13, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (88, 13, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (89, 13, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (90, 13, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (91, 13, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (92, 13, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (93, 14, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (94, 14, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (95, 15, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (96, 15, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (97, 15, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (98, 15, N'Archive', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (99, 15, N'ViewArchive', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (100, 16, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (101, 16, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (102, 16, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (103, 16, N'Archive', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (104, 16, N'ViewArchive', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (105, 17, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (106, 17, N'Design', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (107, 17, N'QuickReportDesign', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (108, 18, N'Save', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (109, 18, N'Delete', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (110, 18, N'SetDefault', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (111, 19, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (112, 19, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (113, 20, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (114, 20, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (115, 21, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (116, 21, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (117, 22, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (118, 22, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (119, 23, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (120, 23, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (121, 23, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (122, 23, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (123, 23, N'Filter', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (124, 23, N'Print', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (125, 24, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (126, 24, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (127, 24, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (128, 24, N'Mark', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (129, 24, N'ViewByBranch', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (130, 25, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (131, 25, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (132, 25, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (133, 25, N'Mark', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (134, 25, N'ViewByBranch', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (135, 26, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (136, 26, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (137, 26, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (138, 26, N'ViewByBranch', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (139, 27, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (140, 27, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (141, 27, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (142, 27, N'Mark', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (143, 27, N'ViewByBranch', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (144, 28, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (145, 28, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (146, 28, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (147, 28, N'ViewByBranch', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (148, 29, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (149, 29, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (150, 29, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (151, 29, N'ViewByBranch', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (152, 30, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (153, 31, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (154, 31, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (155, 31, N'Print', 4)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Auth].[RolePermission] ON 

INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (1, 2, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (2, 2, 2)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (3, 2, 3)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (4, 2, 4)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (5, 2, 5)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (6, 2, 6)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (7, 2, 7)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (8, 2, 8)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (9, 2, 9)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (10, 2, 10)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (11, 2, 11)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (12, 2, 12)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (13, 2, 13)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (14, 2, 14)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (15, 2, 15)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (16, 2, 16)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (17, 2, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (18, 2, 23)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (19, 2, 24)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (20, 2, 25)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (21, 2, 26)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (22, 2, 27)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (23, 2, 28)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (24, 2, 29)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (25, 2, 30)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (26, 2, 17)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (27, 2, 36)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (28, 2, 41)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (29, 2, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (30, 2, 49)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (31, 2, 56)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (32, 2, 57)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (33, 2, 60)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (34, 2, 62)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (35, 2, 63)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (36, 2, 64)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (37, 2, 65)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (38, 2, 66)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (39, 2, 67)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (40, 3, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (41, 3, 5)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (42, 3, 9)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (43, 3, 13)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (44, 3, 17)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (45, 3, 18)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (46, 3, 19)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (47, 3, 20)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (48, 3, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (49, 3, 26)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (50, 3, 31)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (51, 3, 34)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (52, 3, 35)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (53, 3, 36)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (54, 3, 41)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (55, 3, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (56, 3, 49)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (57, 3, 56)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (58, 3, 57)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (59, 3, 60)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (60, 3, 62)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (61, 3, 66)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (62, 3, 69)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (63, 3, 71)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (64, 4, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (65, 4, 5)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (66, 4, 9)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (67, 4, 13)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (68, 4, 17)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (69, 4, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (70, 4, 26)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (71, 4, 32)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (72, 4, 35)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (73, 4, 41)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (74, 4, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (75, 4, 49)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (76, 4, 60)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (77, 4, 69)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (78, 4, 70)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (79, 4, 71)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (80, 5, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (81, 5, 5)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (82, 5, 9)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (83, 5, 13)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (84, 5, 17)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (85, 5, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (86, 5, 26)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (87, 5, 32)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (88, 5, 33)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (89, 5, 35)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (90, 5, 41)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (91, 5, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (92, 5, 49)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (93, 5, 60)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (94, 5, 69)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (95, 5, 70)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (96, 5, 71)

SET IDENTITY_INSERT [Auth].[RolePermission] OFF

SET IDENTITY_INSERT [Reporting].[SystemIssue] ON 

INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (1, NULL, NULL, NULL, N'Accounting', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (2, 1, NULL, NULL, N'VoucherIssues', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (3, 1, NULL, NULL, N'AccountIssues', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (4, 2, 39, 2, N'UnbalancedVouchers', N'/vouchers/unbalanced', N'/vouchers', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (5, 2, 39, 2, N'VouchersWithNoArticle', N'/vouchers/no-article', N'/vouchers', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (6, 2, 39, 42, N'ArticlesHavingZeroAmount', N'/vouchers/articles/sys-issue/zero-amount', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (7, 2, 39, 42, N'ArticlesWithMissingAccount', N'/vouchers/articles/sys-issue/miss-acc', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (8, 2, 39, 42, N'ArticlesWithInvalidAccountItems', N'/vouchers/articles/sys-issue/invalid-acc', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (9, 2, NULL, 41, N'MissingVoucherNumbers', N'/vouchers/miss-number', NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (10, 3, 39, 42, N'AccountsWithInvalidBalance', N'/vouchers/articles/sys-issue/invalid-acc-balance', NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (11, 3, 39, 42, N'AccountsWithInvalidPeriodTurnover', N'/vouchers/articles/sys-issue/invalid-acc-turnover', NULL, 0)

SET IDENTITY_INSERT [Reporting].[SystemIssue] OFF

ALTER TABLE [Metadata].[Command]
ADD CONSTRAINT [FK_Metadata_Command_Metadata_Parent] FOREIGN KEY ([ParentID])
    REFERENCES [Metadata].[Command]([CommandID]);
GO

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (1, NULL, NULL, N'Accounting', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (2, 1, NULL, N'BaseData', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (3, 2, 87, N'AccountGroup', N'/finance/account-groups', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (4, 2, 1, N'Account', N'/finance/account', 'tasks', 'Ctrl+Shift+A')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (5, 2, 7, N'DetailAccount', N'/finance/detailAccount', 'list', 'Ctrl+Shift+D')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (6, 2, 13, N'CostCenter', N'/finance/costCenter', 'list', 'Ctrl+Shift+C')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (7, 2, 19, N'Project', N'/finance/projects', 'list', 'Ctrl+Shift+P')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (8, 2, 111, N'AccountRelations', N'/finance/accountrelations', 'list', 'Ctrl+Shift+R')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (9, 2, 93, N'AccountCollections', N'/finance/account-collection', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (10, 2, 32, N'Currency', N'/finance/currency', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (11, 1, NULL, N'VoucherOps', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (12, 11, 40, N'NewVoucher', N'/finance/vouchers/new', N'list', N'Ctrl+N')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (13, 11, 41, N'VoucherByNo', N'/finance/vouchers/by-no', N'list', N'Ctrl+S')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (14, 11, 41, N'LastVoucher', N'/finance/vouchers/last', N'list', N'Ctrl+L')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (15, 11, 55, N'Vouchers', N'/finance/voucher', 'list', 'Ctrl+Shift+V')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (16, 1, NULL, N'SpecialOps', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (17, 16, NULL, N'IssueOpeningVoucher', N'/finance/opening-voucher', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (18, 16, NULL, N'IssueClosingVoucher', N'/finance/closing-voucher', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (19, 16, NULL, N'ClosingTempAccounts', N'/finance/close-temp-accounts', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (20, 1, NULL, N'AccountingLedgers', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (21, 20, 125, N'JournalLedger', N'/finance/journal', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (22, 20, 130, N'AccountBook', N'/finance/account-book', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (23, NULL, NULL, N'Organization', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (24, 23, 69, N'Companies', N'/organization/companies', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (25, 23, 62, N'Branches', N'/organization/branches', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (26, 23, 25, N'FiscalPeriods', N'/organization/fiscalperiod', 'list', 'Ctrl+Shift+F')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (27, NULL, NULL, N'Administration', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (28, 27, 72, N'Users', N'/admin/users', 'user', 'Ctrl+Shift+U')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (29, 27, 78, N'Roles', N'/admin/roles', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (30, 27, 117, N'RowAccessSettings', N'/admin/viewRowPermission', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (31, 27, 113, N'Settings', N'/config/settings', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (32, 27, 95, N'OperationLogs', N'/admin/operation-log', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (33, NULL, NULL, N'Profile', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (34, 33, NULL, N'ChangePassword', N'/changePassword', 'eye-open', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (35, 33, NULL, N'LogOut', N'/logout', 'log-out', 'Ctrl+Shift+X')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (36, 33, NULL, N'ChangeCompany', N'/login', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (37, NULL, NULL, N'Tools', NULL, N'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (38, 37, 105, N'ReportManagement', N'reports', N'list', N'Ctrl+R')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (39, 1, NULL, N'FinancialReports', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (40, 39, 135, N'TestBalance', N'/finance/balance', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (41, 20, 139, N'CurrencyBook', N'/finance/currency-book', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (43, 39, 144, N'ItemBalance', N'/finance/itembalance', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (44, 39, 148, N'BalanceByAccount', N'/finance/balance-by-account', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (45, 27, 115, N'LogSettings', N'/admin/log-settings', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.1.978
UPDATE [Metadata].[Command]
SET PermissionID = 54
WHERE TitleKey IN(N'VoucherByNo', N'LastVoucher')

-- 1.1.979
ALTER TABLE [Metadata].[View]
ADD [EntityName] NVARCHAR(64) NOT NULL
CONSTRAINT DF_Metadata_View_EntityName DEFAULT N'TBA'
WITH VALUES;
GO

UPDATE [Metadata].[View]
SET [EntityName] = [Name]
WHERE ViewID >= 1 AND ViewID <= 14

UPDATE [Metadata].[View]
SET [EntityName] = N'Journal'
WHERE [Name] LIKE 'Journal%'

UPDATE [Metadata].[View]
SET [EntityName] = N'AccountBook'
WHERE [Name] LIKE 'AccountBook%'

UPDATE [Metadata].[View]
SET [EntityName] = N'CurrencyBook'
WHERE [Name] LIKE 'CurrencyBook%'

UPDATE [Metadata].[View]
SET [EntityName] = N'TestBalance'
WHERE [Name] LIKE 'TestBalance%'

UPDATE [Metadata].[View]
SET [EntityName] = N'DetailAccountBalance'
WHERE [Name] LIKE 'DetailAccountBalance%'

UPDATE [Metadata].[View]
SET [EntityName] = N'CostCenterBalance'
WHERE [Name] LIKE 'CostCenterBalance%'

UPDATE [Metadata].[View]
SET [EntityName] = N'ProjectBalance'
WHERE [Name] LIKE 'ProjectBalance%'

UPDATE [Metadata].[View]
SET [EntityName] = N'ProfitLoss'
WHERE [Name] LIKE 'ProfitLoss%'

UPDATE [Metadata].[View]
SET [EntityName] = [Name]
WHERE ViewID IN(30,31,41,42,58,59,60,61,63)

-- 1.1.980
-- Refreshing security metadata...

ALTER TABLE [Metadata].[Command]
DROP CONSTRAINT [FK_Metadata_Command_Metadata_Parent]
GO

DELETE FROM [Metadata].[Command]

DELETE FROM [Reporting].[SystemIssue]

DELETE FROM [Auth].[RolePermission]

DELETE FROM [Auth].[Permission]

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (1, 1, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (2, 1, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (3, 1, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (4, 1, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (5, 1, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (6, 1, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (7, 1, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (8, 2, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (9, 2, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (10, 2, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (11, 2, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (12, 2, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (13, 2, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (14, 2, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (15, 3, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (16, 3, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (17, 3, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (18, 3, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (19, 3, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (20, 3, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (21, 3, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (22, 4, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (23, 4, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (24, 4, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (25, 4, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (26, 4, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (27, 4, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (28, 4, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (29, 5, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (30, 5, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (31, 5, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (32, 5, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (33, 5, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (34, 5, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (35, 5, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (36, 5, N'AssignRolesToEntity,FiscalPeriod', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (37, 6, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (38, 6, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (39, 6, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (40, 6, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (41, 6, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (42, 6, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (43, 6, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (44, 6, N'ChangeStatus', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (45, 7, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (46, 7, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (47, 7, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (48, 7, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (49, 7, N'Print', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (50, 7, N'CreateLine', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (51, 7, N'EditLine', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (52, 7, N'DeleteLine', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (53, 7, N'Check', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (54, 7, N'UndoCheck', 512)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (55, 7, N'Confirm', 1024)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (56, 7, N'UndoConfirm', 2048)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (57, 7, N'Approve', 4096)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (58, 7, N'UndoApprove', 8192)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (59, 7, N'Finalize', 16384)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (60, 7, N'NavigateEntities,Vouchers', 32768)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (61, 8, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (62, 8, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (63, 8, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (64, 8, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (65, 8, N'GroupCheck', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (66, 8, N'GroupUndoCheck', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (67, 8, N'GroupConfirm', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (68, 8, N'GroupUndoConfirm', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (69, 8, N'GroupFinalize', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (70, 9, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (71, 9, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (72, 9, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (73, 9, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (74, 9, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (75, 9, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (76, 9, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (77, 9, N'AssignRolesToEntity,Branch', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (78, 10, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (79, 10, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (80, 10, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (81, 10, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (82, 11, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (83, 11, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (84, 11, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (85, 11, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (86, 11, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (87, 11, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (88, 11, N'AssignRolesToEntity,User', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (89, 12, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (90, 12, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (91, 12, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (92, 12, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (93, 12, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (94, 12, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (95, 12, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (96, 12, N'AssignEntityToRole,User', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (97, 12, N'AssignEntityToRole,Branch', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (98, 12, N'AssignEntityToRole,FiscalPeriod', 512)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (99, 13, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (100, 13, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (101, 13, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (102, 13, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (103, 13, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (104, 13, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (105, 13, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (106, 14, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (107, 14, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (108, 15, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (109, 15, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (110, 15, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (111, 15, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (112, 15, N'Archive', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (113, 15, N'ViewArchive', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (114, 16, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (115, 16, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (116, 16, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (117, 16, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (118, 16, N'Archive', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (119, 16, N'ViewArchive', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (120, 17, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (121, 17, N'Design', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (122, 17, N'QuickReportDesign', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (123, 18, N'Save', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (124, 18, N'Delete', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (125, 18, N'SetDefault', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (126, 19, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (127, 19, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (128, 20, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (129, 20, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (130, 21, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (131, 21, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (132, 22, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (133, 22, N'Save', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (134, 23, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (135, 23, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (136, 23, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (137, 23, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (138, 23, N'Create', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (139, 23, N'Edit', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (140, 23, N'Delete', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (141, 24, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (142, 24, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (143, 24, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (144, 24, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (145, 24, N'Mark', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (146, 24, N'ViewByBranch', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (147, 25, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (148, 25, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (149, 25, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (150, 25, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (151, 25, N'Mark', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (152, 25, N'ViewByBranch', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (153, 26, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (154, 26, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (155, 26, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (156, 26, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (157, 26, N'ViewByBranch', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (158, 27, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (159, 27, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (160, 27, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (161, 27, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (162, 27, N'Mark', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (163, 27, N'ViewByBranch', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (164, 28, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (165, 28, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (166, 28, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (167, 28, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (168, 28, N'ViewByBranch', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (169, 29, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (170, 29, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (171, 29, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (172, 29, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (173, 29, N'ViewByBranch', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (174, 30, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (175, 31, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (176, 31, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (177, 31, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (178, 31, N'Export', 8)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Auth].[RolePermission] ON
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (1, 2, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (2, 2, 8)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (3, 2, 15)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (4, 2, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (5, 2, 29)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (6, 2, 37)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (7, 2, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (8, 2, 61)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (9, 2, 70)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (10, 2, 78)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (11, 2, 82)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (12, 2, 89)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (13, 2, 99)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (14, 2, 106)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (15, 2, 108)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (16, 2, 114)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (17, 2, 120)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (18, 2, 126)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (19, 2, 128)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (20, 2, 130)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (21, 2, 132)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (22, 2, 134)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (23, 2, 141)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (24, 2, 147)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (25, 2, 153)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (26, 2, 158)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (27, 2, 164)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (28, 2, 169)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (29, 2, 174)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (30, 2, 175)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (31, 3, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (32, 3, 8)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (33, 3, 15)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (34, 3, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (35, 3, 29)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (36, 3, 37)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (37, 3, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (38, 3, 61)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (39, 3, 70)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (40, 3, 78)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (41, 3, 82)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (42, 3, 89)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (43, 3, 99)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (44, 3, 106)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (45, 3, 108)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (46, 3, 114)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (47, 3, 120)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (48, 3, 126)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (49, 3, 128)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (50, 3, 130)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (51, 3, 132)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (52, 3, 134)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (53, 3, 141)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (54, 3, 147)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (55, 3, 153)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (56, 3, 158)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (57, 3, 164)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (58, 3, 169)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (59, 3, 174)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (60, 3, 175)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (61, 4, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (62, 4, 8)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (63, 4, 15)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (64, 4, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (65, 4, 29)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (66, 4, 37)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (67, 4, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (68, 4, 61)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (69, 4, 70)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (70, 4, 78)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (71, 4, 82)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (72, 4, 89)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (73, 4, 99)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (74, 4, 106)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (75, 4, 108)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (76, 4, 114)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (77, 4, 120)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (78, 4, 126)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (79, 4, 128)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (80, 4, 130)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (81, 4, 132)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (82, 4, 134)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (83, 4, 141)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (84, 4, 147)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (85, 4, 153)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (86, 4, 158)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (87, 4, 164)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (88, 4, 169)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (89, 4, 174)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (90, 4, 175)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (91, 5, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (92, 5, 8)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (93, 5, 15)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (94, 5, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (95, 5, 29)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (96, 5, 37)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (97, 5, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (98, 5, 61)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (99, 5, 70)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (100, 5, 78)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (101, 5, 82)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (102, 5, 89)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (103, 5, 99)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (104, 5, 106)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (105, 5, 108)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (106, 5, 114)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (107, 5, 120)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (108, 5, 126)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (109, 5, 128)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (110, 5, 130)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (111, 5, 132)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (112, 5, 134)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (113, 5, 141)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (114, 5, 147)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (115, 5, 153)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (116, 5, 158)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (117, 5, 164)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (118, 5, 169)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (119, 5, 174)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (120, 5, 175)
SET IDENTITY_INSERT [Auth].[RolePermission] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (1, NULL, NULL, N'Accounting', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (2, 1, NULL, N'BaseData', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (3, 2, 99, N'AccountGroup', N'/finance/account-groups', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (4, 2, 1, N'Account', N'/finance/account', 'tasks', 'Ctrl+Shift+A')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (5, 2, 8, N'DetailAccount', N'/finance/detailAccount', 'list', 'Ctrl+Shift+D')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (6, 2, 15, N'CostCenter', N'/finance/costCenter', 'list', 'Ctrl+Shift+C')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (7, 2, 22, N'Project', N'/finance/projects', 'list', 'Ctrl+Shift+P')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (8, 2, 126, N'AccountRelations', N'/finance/accountrelations', 'list', 'Ctrl+Shift+R')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (9, 2, 106, N'AccountCollections', N'/finance/account-collection', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (10, 2, 37, N'Currency', N'/finance/currency', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (11, 1, NULL, N'VoucherOps', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (12, 11, 46, N'NewVoucher', N'/finance/vouchers/new', N'list', N'Ctrl+N')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (13, 11, 60, N'VoucherByNo', N'/finance/vouchers/by-no', N'list', N'Ctrl+S')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (14, 11, 60, N'LastVoucher', N'/finance/vouchers/last', N'list', N'Ctrl+L')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (15, 11, 61, N'Vouchers', N'/finance/voucher', 'list', 'Ctrl+Shift+V')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (16, 1, NULL, N'SpecialOps', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (17, 16, NULL, N'IssueOpeningVoucher', N'/finance/opening-voucher', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (18, 16, NULL, N'IssueClosingVoucher', N'/finance/closing-voucher', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (19, 16, NULL, N'ClosingTempAccounts', N'/finance/close-temp-accounts', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (20, 1, NULL, N'AccountingLedgers', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (21, 20, 141, N'JournalLedger', N'/finance/journal', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (22, 20, 147, N'AccountBook', N'/finance/account-book', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (23, NULL, NULL, N'Organization', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (24, 23, 78, N'Companies', N'/organization/companies', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (25, 23, 70, N'Branches', N'/organization/branches', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (26, 23, 29, N'FiscalPeriods', N'/organization/fiscalperiod', 'list', 'Ctrl+Shift+F')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (27, NULL, NULL, N'Administration', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (28, 27, 82, N'Users', N'/admin/users', 'user', 'Ctrl+Shift+U')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (29, 27, 89, N'Roles', N'/admin/roles', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (30, 27, 132, N'RowAccessSettings', N'/admin/viewRowPermission', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (31, 27, 128, N'Settings', N'/config/settings', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (32, 27, 108, N'OperationLogs', N'/admin/operation-log', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (33, NULL, NULL, N'Profile', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (34, 33, NULL, N'ChangePassword', N'/changePassword', 'eye-open', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (35, 33, NULL, N'LogOut', N'/logout', 'log-out', 'Ctrl+Shift+X')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (36, 33, NULL, N'ChangeCompany', N'/login', 'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (37, NULL, NULL, N'Tools', NULL, N'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (38, 37, 120, N'ReportManagement', N'reports', N'list', N'Ctrl+R')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (39, 1, NULL, N'FinancialReports', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (40, 39, 153, N'TestBalance', N'/finance/balance', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (41, 20, 158, N'CurrencyBook', N'/finance/currency-book', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (42, 27, 174, N'SystemIssue', N'/finance/system-issue', N'tasks', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (43, 39, 164, N'ItemBalance', N'/finance/itembalance', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (44, 39, 169, N'BalanceByAccount', N'/finance/balance-by-account', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (45, 27, 130, N'LogSettings', N'/admin/log-settings', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (46, 39, 175, N'ProfitLoss', N'/finance/profit-loss', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

ALTER TABLE [Metadata].[Command]
ADD CONSTRAINT [FK_Metadata_Command_Metadata_Parent] FOREIGN KEY ([ParentID])
    REFERENCES [Metadata].[Command]([CommandID]);
GO

SET IDENTITY_INSERT [Reporting].[SystemIssue] ON 
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (1, NULL, NULL, NULL, N'Accounting', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (2, 1, NULL, NULL, N'VoucherIssues', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (3, 1, NULL, NULL, N'AccountIssues', NULL, NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (4, 2, 45, 2, N'UnbalancedVouchers', N'/vouchers/unbalanced', N'/vouchers', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (5, 2, 45, 2, N'VouchersWithNoArticle', N'/vouchers/no-article', N'/vouchers', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (6, 2, 45, 42, N'ArticlesHavingZeroAmount', N'/vouchers/articles/sys-issue/zero-amount', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (7, 2, 45, 42, N'ArticlesWithMissingAccount', N'/vouchers/articles/sys-issue/miss-acc', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (8, 2, 45, 42, N'ArticlesWithInvalidAccountItems', N'/vouchers/articles/sys-issue/invalid-acc', N'/vouchers/articles', 1)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (9, 2, NULL, 41, N'MissingVoucherNumbers', N'/vouchers/miss-number', NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (10, 3, 45, 42, N'AccountsWithInvalidBalance', N'/vouchers/articles/sys-issue/invalid-acc-balance', NULL, 0)
INSERT [Reporting].[SystemIssue] ([SystemIssueID], [ParentID], [PermissionID], [ViewID], [TitleKey], [ApiUrl], [DeleteApiUrl], [BranchScope]) 
    VALUES (11, 3, 45, 42, N'AccountsWithInvalidPeriodTurnover', N'/vouchers/articles/sys-issue/invalid-acc-turnover', NULL, 0)
SET IDENTITY_INSERT [Reporting].[SystemIssue] OFF

-- 1.1.985
-- add Triggers for MetaData.Columns...

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [Metadata].[TR_MetaDataView_Delete] ON [Metadata].[Column]
AFTER Delete
AS
   update viw set ModifiedDate = GETDATE() from deleted del join [Metadata].[View] viw on del.ViewID = viw.ViewID
   delete setting from deleted del join Config.UserSetting setting on del.ViewID = setting.ViewID and setting.ModelType = 'ListFormViewConfig'   


GO

ALTER TABLE [Metadata].[Column] ENABLE TRIGGER [TR_MetaDataView_Delete]
GO

-----------------------------

CREATE TRIGGER [Metadata].[TR_MetaDataView_Insert] ON [Metadata].[Column]
AFTER Insert 
AS
   update viw set ModifiedDate = GETDATE() from inserted ins join [Metadata].[View] viw on ins.ViewID = viw.ViewID
   delete setting from inserted ins join Config.UserSetting setting on ins.ViewID = setting.ViewID and ModelType = 'ListFormViewConfig'   


GO

ALTER TABLE [Metadata].[Column] ENABLE TRIGGER [TR_MetaDataView_Insert]
GO

-----------------------------

CREATE TRIGGER [Metadata].[TR_MetaDataView_Update] ON [Metadata].[Column]
AFTER UPDATE 
AS
 
IF UPDATE([Name]) or UPDATE([Type]) or UPDATE([DotNetType]) or UPDATE(StorageType) or UPDATE([Length]) or UPDATE(MinLength) or  UPDATE(IsFixedLength)
 or UPDATE(IsNullable) or UPDATE(AllowSorting) or UPDATE(AllowFiltering) or UPDATE(Visibility) or UPDATE(DisplayIndex) or UPDATE(Expression)
BEGIN
   update viw set ModifiedDate = GETDATE() from inserted ins join [Metadata].[View] viw on ins.ViewID = viw.ViewID
   delete setting from inserted ins join Config.UserSetting setting on ins.ViewID = setting.ViewID and ModelType = 'ListFormViewConfig'   
END

GO

ALTER TABLE [Metadata].[Column] ENABLE TRIGGER [TR_MetaDataView_Update]
GO

-- 1.1.987
UPDATE [Metadata].[Command]
SET RouteUrl = N'/finance/vouchers/opening-voucher'
WHERE TitleKey = N'IssueOpeningVoucher'

UPDATE [Metadata].[Command]
SET RouteUrl = N'/finance/vouchers/closing-voucher'
WHERE TitleKey = N'IssueClosingVoucher'

UPDATE [Metadata].[Command]
SET RouteUrl = N'/finance/vouchers/close-temp-accounts'
WHERE TitleKey = N'ClosingTempAccounts'

-- 1.1.992
DELETE FROM [Metadata].[Column]
WHERE ViewID = 30 AND Name = N'Country'

-- 1.1.995
-- NOTE: Admin-only permissions cannot be assigned to non-admin roles
DELETE FROM [Auth].[RolePermission]
WHERE PermissionID IN(130,131)

-- 1.1.998
UPDATE [Metadata].[Column]
SET [Type] = N'Money'
WHERE ViewID = 62 AND ([Name] LIKE '%Balance%' OR [Name] LIKE '%Turnover%')

UPDATE [Metadata].[Column]
SET [Name] = N'Group'
WHERE ViewID = 64 AND [Name] = N'Category'

-- 1.1.1001
UPDATE [Auth].[PermissionGroup]
SET [EntityName] = 'Vouchers'
WHERE PermissionGroupID = 8


-- 1.1.1003
ALTER TABLE [Metadata].[Column]
ADD [IsDynamic] BIT NOT NULL
CONSTRAINT DF_Metadata_Column_IsDynamic DEFAULT (0)
WITH VALUES;
GO

SET IDENTITY_INSERT [Metadata].[View] ON 
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (65, 'ProfitLossByCostCenters', 'ProfitLoss', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (66, 'ProfitLossByProjects', 'ProfitLoss', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (67, 'ProfitLossByBranches', 'ProfitLoss', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (68, 'ProfitLossByFiscalPeriods', 'ProfitLoss', 0, 0, NULL, '', NULL)
SET IDENTITY_INSERT [Metadata].[View] OFF 

SET IDENTITY_INSERT [Metadata].[Column] ON 
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (670, 65, 'Group', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (671, 65, 'Account', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (672, 65, 'StartBalanceCostCenter', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (673, 65, 'PeriodTurnoverCostCenter', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (674, 65, 'EndBalanceCostCenter', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (675, 65, 'BalanceCostCenter', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, 'Hidden', 5, NULL)

INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (676, 66, 'Group', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (677, 66, 'Account', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (678, 66, 'StartBalanceProject', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (679, 66, 'PeriodTurnoverProject', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (680, 66, 'EndBalanceProject', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (681, 66, 'BalanceProject', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, 'Hidden', 5, NULL)

INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (682, 67, 'Group', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (683, 67, 'Account', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (684, 67, 'StartBalanceBranch', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (685, 67, 'PeriodTurnoverBranch', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (686, 67, 'EndBalanceBranch', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (687, 67, 'BalanceBranch', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, 'Hidden', 5, NULL)

INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (688, 68, 'Group', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (689, 68, 'Account', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (690, 68, 'StartBalanceFiscalPeriod', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (691, 68, 'PeriodTurnoverFiscalPeriod', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (692, 68, 'EndBalanceFiscalPeriod', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (693, 68, 'BalanceFiscalPeriod', 1, NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 0, 0, 0, 'Hidden', 5, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF 


-- 1.1.1012
DELETE FROM [Reporting].[LocalReport]
WHERE ReportID IN(50,51,52,53)

DELETE FROM [Reporting].[Parameter]
WHERE ReportID IN(50,51,52,53)

DELETE FROM [Reporting].[Report]
WHERE ReportID IN(50,51,52,53)

UPDATE [Reporting].[Report]
SET SubsystemID = 2
WHERE Code LIKE '%TestBalance%'

DELETE FROM [Reporting].[LocalReport]
WHERE LocaleID IN(3,4)

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (50, 19, 1, 43, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (51, 19, 1, 44, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (52, 19, 1, 45, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (53, 19, 1, 46, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (54, 19, 1, 48, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (55, 19, 1, 49, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (56, 19, 1, 50, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (57, 19, 1, 51, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (58, 19, 1, 53, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (59, 19, 1, 54, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (60, 19, 1, 55, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (61, 19, 1, 56, 2, '', NULL, 0, 1, 1, 1)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (189, 1, 50, 'Detail account turnover/balance - 2 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (190, 2, 50, N'گردش و مانده تفصیلی شناور 2 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (191, 1, 51, 'Detail account turnover/balance - 4 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (192, 2, 51, N'گردش و مانده تفصیلی شناور 4 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (193, 1, 52, 'Detail account turnover/balance - 6 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (194, 2, 52, N'گردش و مانده تفصیلی شناور 6 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (195, 1, 53, 'Detail account turnover/balance - 8 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (196, 2, 53, N'گردش و مانده تفصیلی شناور 8 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (197, 1, 54, 'Cost center turnover/balance - 2 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (198, 2, 54, N'گردش و مانده مرکز هزینه 2 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (199, 1, 55, 'Cost center turnover/balance - 4 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (200, 2, 55, N'گردش و مانده مرکز هزینه 4 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (201, 1, 56, 'Cost center turnover/balance - 6 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (202, 2, 56, N'گردش و مانده مرکز هزینه 6 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (203, 1, 57, 'Cost center turnover/balance - 8 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (204, 2, 57, N'گردش و مانده مرکز هزینه 8 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (205, 1, 58, 'Project turnover/balance - 2 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (206, 2, 58, N'گردش و مانده پروژه 2 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (207, 1, 59, 'Project turnover/balance - 4 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (208, 2, 59, N'گردش و مانده پروژه 4 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (209, 1, 60, 'Project turnover/balance - 6 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (210, 2, 60, N'گردش و مانده پروژه 6 ستونی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (211, 1, 61, 'Project turnover/balance - 8 column', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (212, 2, 61, N'گردش و مانده پروژه 8 ستونی', NULL)
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (63, 50, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (64, 50, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (65, 50, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (66, 50, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (67, 50, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (68, 51, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (69, 51, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (70, 51, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (71, 51, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (72, 51, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (73, 52, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (74, 52, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (75, 52, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (76, 52, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (77, 52, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (78, 53, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (79, 53, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (80, 53, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (81, 53, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (82, 53, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (83, 54, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (84, 54, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (85, 54, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (86, 54, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (87, 54, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (88, 55, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (89, 55, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (90, 55, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (91, 55, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (92, 55, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (93, 56, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (94, 56, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (95, 56, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (96, 56, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (97, 56, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (98, 57, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (99, 57, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (100, 57, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (101, 57, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (102, 57, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (103, 58, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (104, 58, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (105, 58, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (106, 58, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (107, 58, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (108, 59, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (109, 59, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (110, 59, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (111, 59, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (112, 59, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (113, 60, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (114, 60, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (115, 60, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (116, 60, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (117, 60, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (118, 61, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (119, 61, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (120, 61, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (121, 61, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (122, 61, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

-- 1.1.1017
SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (694, 2, 'TypeName', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, NULL, 16, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.1018
DELETE FROM [Metadata].[Column]
WHERE ViewID IN(65,66,67,68)

DELETE FROM [Core].[SysOperationLog]
WHERE SourceListID IN(65,66,67,68)

DELETE FROM [Core].[SysOperationLogArchive]
WHERE SourceListID IN(65,66,67,68)

DELETE FROM [Config].[UserSetting]
WHERE ViewID IN(65,66,67,68)

DELETE FROM [Metadata].[View]
WHERE ViewID IN(65,66,67,68)

SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (65, 'ComparativeProfitLoss', 'ProfitLoss', 0, 0, NULL, '', NULL)
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (66, 'ComparativeProfitLossSimple', 'ProfitLoss', 0, 0, NULL, '', NULL)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
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
SET IDENTITY_INSERT [Metadata].[Column] OFF

-- 1.1.1020
DELETE FROM [Reporting].[LocalReport]
WHERE ReportID = 12

DELETE FROM [Reporting].[Report]
WHERE ReportID = 12

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (62, 19, 1, 27, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (63, 19, 1, 28, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (64, 19, 1, 29, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (65, 19, 1, 37, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (66, 19, 1, 38, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (67, 19, 1, 39, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (68, 19, 1, 40, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (69, 19, 1, 58, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (70, 6, 1, 61, 1, '', N'oplog/archive', 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (71, 6, 1, 59, 1, '', N'sys-oplog', 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (72, 6, 1, 60, 1, '', N'sys-oplog/archive', 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (73, 6, 1, 13, 1, '', N'oplog', 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (74, 17, 1, 30, 2, '', N'currencies', 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (75, 18, 1, 31, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (76, 19, 1, 62, 2, '', NULL, 0, 1, 1, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (77, 19, 1, 64, 2, '', NULL, 0, 1, 1, 1)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (213, 1, 62, 'Account Book', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (214, 2, 62, N'دفتر حساب', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (215, 1, 63, 'Account Book', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (216, 2, 63, N'دفتر حساب', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (217, 1, 64, 'Account Book', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (218, 2, 64, N'ذفتر حساب', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (219, 1, 65, 'Currency Book', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (220, 2, 65, N'دفتر عملیات ارزی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (221, 1, 66, 'Currency Book', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (222, 2, 66, N'دفتر عملیات ارزی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (223, 1, 67, 'Currency Book', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (224, 2, 67, N'دفتر عملیات ارزی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (225, 1, 68, 'Currency Book', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (226, 2, 68, N'دفتر عملیات ارزی', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (227, 1, 69, 'Balance by account', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (228, 2, 69, N'مانده به تفکیک حساب', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (229, 1, 70, 'Archived operation logs', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (230, 2, 70, N'رویدادهای عملیاتی بایگانی شده', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (231, 1, 71, 'Active system logs', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (232, 2, 71, N'رویدادهای سیستمی فعال', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (233, 1, 72, 'Archived system logs', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (234, 2, 72, N'رویدادهای سیستمی بایگانی شده', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (235, 1, 73, 'Active operation logs', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (236, 2, 73, N'رویدادهای عملیاتی فعال', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (237, 1, 74, 'Currencies', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (238, 2, 74, N'ارزها', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (239, 1, 75, 'Currency rates', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (240, 2, 75, N'نرخ های ارز', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (241, 1, 76, 'Profit-Loss', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (242, 2, 76, N'سود و زیان', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (243, 1, 77, 'Profit-Loss', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (244, 2, 77, N'سود و زیان', NULL)
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (123, 62, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (124, 62, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (125, 62, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (126, 63, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (127, 63, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (128, 63, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (129, 64, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (130, 64, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (131, 64, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (132, 65, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (133, 65, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (134, 65, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (135, 66, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (136, 66, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (137, 66, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (138, 67, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (139, 67, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (140, 67, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (141, 68, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (142, 68, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (143, 68, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (144, 69, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (145, 69, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (146, 69, 'fromNo', 'from', 'EQ', 'System.Int32', 'QueryString', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (147, 69, 'toNo', 'to', 'EQ', 'System.Int32', 'QueryString', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (148, 69, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (149, 70, 'fromDate', 'Date', 'GTE', 'System.DateTime', 'TextBox', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (150, 70, 'toDate', 'Date', 'LTE', 'System.DateTime', 'TextBox', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (151, 71, 'fromDate', 'Date', 'GTE', 'System.DateTime', 'TextBox', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (152, 71, 'toDate', 'Date', 'LTE', 'System.DateTime', 'TextBox', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (153, 72, 'fromDate', 'Date', 'GTE', 'System.DateTime', 'TextBox', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (154, 72, 'toDate', 'Date', 'LTE', 'System.DateTime', 'TextBox', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (155, 73, 'fromDate', 'Date', 'GTE', 'System.DateTime', 'TextBox', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (156, 73, 'toDate', 'Date', 'LTE', 'System.DateTime', 'TextBox', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (157, 75, 'currencyId', 'CurrencyId', 'EQ', 'System.Int32', 'TextBox', 'CurrencyId', 'CurrencyId')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (158, 76, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (159, 76, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (160, 76, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (161, 77, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (162, 77, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (163, 77, 'VoucherStatus', 'VoucherStatusId', 'EQ', 'System.Int32', 'TextBox', 'VoucherStatus', 'VoucherStatus')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

-- 1.1.1021
UPDATE [Metadata].[Column]
SET [DotNetType] = 'System.Int32', [StorageType] = 'int', [ScriptType] = 'number', [Length] = 0
WHERE Name = 'VoucherNo' AND ViewID IN(15,16,17,18,22,23,24,25)

-- 1.1.1024
UPDATE [Metadata].[View]
SET SearchUrl = '/fperiods/company/{companyid}'
WHERE Name = 'FiscalPeriod'

UPDATE [Metadata].[View]
SET SearchUrl = '/branches/company/{companyid}'
WHERE Name = 'Branch'

-- 1.1.1029
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (32, N'DraftVouchers', N'DraftVoucher')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (33, N'ManageEntities,DraftVouchers', N'DraftVouchers')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (179, 32, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (180, 32, N'Create', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (181, 32, N'Edit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (182, 32, N'Delete', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (183, 32, N'Print', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (184, 32, N'CreateLine', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (185, 32, N'EditLine', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (186, 32, N'DeleteLine', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (187, 32, N'Check', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (188, 32, N'UndoCheck', 512)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (189, 32, N'NavigateEntities,DraftVouchers', 1024)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (190, 33, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (191, 33, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (192, 33, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (193, 33, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (194, 33, N'GroupCheck', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (195, 33, N'GroupUndoCheck', 32)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (47, 11, 180, N'NewDraftVoucher', N'/finance/vouchers/new/draft', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (48, 11, 189, N'DraftVoucherByNo', N'/finance/vouchers/by-no/draft', N'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (49, 11, 189, N'LastDraftVoucher', N'/finance/vouchers/last/draft', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.1.1040
SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (196, 32, N'Normalize', 2048)
SET IDENTITY_INSERT [Auth].[Permission] OFF

-- 1.1.1046
SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (197, 26, N'FilterByRef', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (198, 28, N'FilterByRef', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (199, 29, N'FilterByRef', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (200, 31, N'FilterByRef', 16)
SET IDENTITY_INSERT [Auth].[Permission] OFF

-- 1.1.1052
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (34, N'BalanceSheetReport', N'BalanceSheet')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (201, 34, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (202, 34, N'Filter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (203, 34, N'Print', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (204, 34, N'Export', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (205, 34, N'FilterByRef', 16)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (50, 39, 201, N'BalanceSheet', N'/finance/bal-sheet', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.1.1056
SET IDENTITY_INSERT [Metadata].[View] ON 
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (67, 'BalanceSheet', 'BalanceSheet', 0, 0, '', '', NULL)
SET IDENTITY_INSERT [Metadata].[View] OFF 

SET IDENTITY_INSERT [Metadata].[Column] ON 
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (695, 67, 'Assets', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, 'AlwaysVisible', 0, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (696, 67, 'AssetsBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 1, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (697, 67, 'AssetsPreviousBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 2, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (698, 67, 'Liabilities', NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 1, 0, 0, NULL, 3, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (699, 67, 'LiabilitiesBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 4, NULL)
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (700, 67, 'LiabilitiesPreviousBalance', NULL, 'Money', 'System.Decimal', 'money', 'number', 0, 0, 0, 1, 0, 0, NULL, 5, NULL)
SET IDENTITY_INSERT [Metadata].[Column] OFF 

-- 1.1.1065
SET IDENTITY_INSERT [Reporting].[Report] ON

INSERT INTO [Reporting].[Report] (ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
VALUES (80,19,1,67,1,'BalanceSheet','bal-sheet',0,1,1,1)

SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON

INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
VALUES (249,1,80,'BalanceSheet')
INSERT INTO Reporting.LocalReport(LocalReportID,LocaleID,ReportID,Caption)
VALUES (250,2,80,N'ترازنامه')

SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

-- 1.1.1099
SET IDENTITY_INSERT [Reporting].[Report] ON

INSERT INTO [Reporting].[Report] (ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
VALUES (78,19,1,65,2,'',NULL,0,1,1,1)

INSERT INTO [Reporting].[Report] (ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
VALUES (79,19,1,66,2,'',NULL,0,1,1,1)

INSERT INTO [Reporting].[Report] (ReportID,ParentID,CreatedByID,ViewID,SubsystemID,Code,ServiceUrl,IsGroup,IsSystem,IsDefault,IsDynamic)
VALUES (81,18,1,2,2,'Vouchers','vouchers',0,1,1,1)

SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON

INSERT INTO [Reporting].[LocalReport] (LocalReportID,LocaleID,ReportID,Caption)
VALUES (251,1,81,'Vouchers')
INSERT INTO Reporting.LocalReport(LocalReportID,LocaleID,ReportID,Caption)
VALUES (252,2,81,N'اسناد مالی')

SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

Update [Reporting].[Report] Set ParentID = 20 Where ReportID = 39

-- 1.1.1113
update Reporting.LocalReport set Template =  N'{
  "ReportVersion": "2019.2.3.0",
  "ReportGuid": "efb6798f9ec845a18b6eb97181be56db",
  "ReportName": "Report",
  "ReportAlias": "Report",
  "ReportCreated": "/Date(1542053910000+0330)/",
  "ReportChanged": "/Date(1617808125000+0430)/",
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
              "ClientRectangle": "2,0,4.2,0.4",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Accounting Voucher (standard form)"
              },
              "Font": "IRANSansWeb;16;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text6",
              "Guid": "9b3462eb08f14a83b28a0aa8538243cf",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.81,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Voucher No:"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;Bold;",
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
              "Name": "Text7",
              "Guid": "519bad8b0ab241cdb645cf73425d7f19",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.1,0.81,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Voucher date:"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;Bold;",
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
              "Name": "Text8",
              "Guid": "b4bf24897b9c4fdf8f21e3e072147e4d",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.5,0.85,0.5,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{no}"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;;",
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
              "Name": "Text9",
              "Guid": "bb3c8abb1ce64d9988b4339921c3cb4a",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.2,0.86,0.9,0.3",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{date}"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
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
              "ClientRectangle": "0.6,0.53,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Report date:"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;9;Bold;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
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
              "ClientRectangle": "1.4,0.56,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{currentDate}"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
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
              "ClientRectangle": "5.9,0.5,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Page number:"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;;Bold;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
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
              "ClientRectangle": "6.8,0.51,0.7,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{PageNumber.ToString() + \"-\" + TotalPageCount.ToString()}"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;;",
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
              "Font": "IRANSansWeb;9;;",
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
              "Font": "IRANSansWeb;9;;",
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
              "Font": "IRANSansWeb;;;",
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
              "Font": "IRANSansWeb;;;",
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
              "Font": "IRANSansWeb;;;",
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
          "Font": "IRANSansWeb;;;",
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "TextBrush": "solid:Black",
          "TextOptions": {
            "WordWrap": true
          }
        },
        "3": {
          "Ident": "StiColumnHeaderBand",
          "Name": "ColumnHeaderBand2",
          "Guid": "0d32f8644ec5457c94a2123c56da5d61",
          "ClientRectangle": "0,1.7,8.07,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text23",
              "Guid": "07c1ebc9bbd44627a2d658829a3a2bd5",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.2,0,1.4,0.3",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Description"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;;",
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
              "Name": "Text27",
              "Guid": "d37d50c68b2f41629112c8752cc08527",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0,1,0.3",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Credit"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;;",
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
              "Name": "Text29",
              "Guid": "1107891d658a4296918533ef96bc17af",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.2,0,1.2,0.3",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Partial Amount"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;;",
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
              "Name": "Text30",
              "Guid": "0fb1c67c2f4c4fad839fb28becef275f",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.4,0,1,0.3",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Debit"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;;",
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
              "Name": "Text13",
              "Guid": "95d746759f5a4a1ea90fd0c85cef280b",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,1.1,0.3",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Account Code"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;10;;",
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
              "Name": "HorizontalLinePrimitive6",
              "Guid": "5d3c85177da341db941e484fda45244e",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,6.8,0.01",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "6": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive7",
              "Guid": "ded32dc3723c4260acec136d25b806f3",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,6.8,0.01",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "7": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive18",
              "Guid": "7fbf1724f7854f8ea9a78b7b081eebc5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "2b96b7d448e947e38067847c50efbd2c"
            },
            "8": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive18",
              "Guid": "669bfa90c6114c7c8dc6fc2c51d5a506",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0.3,0,0",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "2b96b7d448e947e38067847c50efbd2c"
            },
            "9": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive23",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.2,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "8f19add3617d41c89ce1a2688bb919f2"
            },
            "10": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive25",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "1487ab9012074160a189fcf60a52d8a8"
            },
            "11": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive26",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.5,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "5684e56c2330444280cad552149a5a2d"
            },
            "12": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive27",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "5ffb09a3979042bb86342ca11987680b"
            },
            "13": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive28",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.3,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "d44e4ccd9f2543c4bff2da465aed4aa6"
            },
            "14": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive1",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.2,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ccf86188b6314bef816775f83a96c432"
            },
            "15": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive1",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.2,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ccf86188b6314bef816775f83a96c432"
            },
            "16": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive2",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "8dad7e7cc9cc417da559641180f3e9a9"
            },
            "17": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive2",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "8dad7e7cc9cc417da559641180f3e9a9"
            },
            "18": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive3",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "25305b27b57e4b19a39ceb2cba9ca06c"
            },
            "19": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive3",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "25305b27b57e4b19a39ceb2cba9ca06c"
            },
            "20": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive4",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "e51f4b339cab47dca8b52e22493b01af"
            },
            "21": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive4",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "e51f4b339cab47dca8b52e22493b01af"
            },
            "22": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7763d3cc03d14afcadb9355599e51c02"
            },
            "23": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7763d3cc03d14afcadb9355599e51c02"
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
              "Name": "Text19",
              "Guid": "fe3c47eee3554b4e9df93ac86601ed80",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.25,0,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.partialAmount}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;;;",
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
            "1": {
              "Ident": "StiText",
              "Name": "Text20",
              "Guid": "20293cc6e5bd42c2938f32a4668064ae",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.4,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.debit}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;;;",
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
              "Name": "Text21",
              "Guid": "98bc4d90eeb54b549da14d4b841426c7",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.7,0,0.89,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.accountFullCode}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;;;",
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
              "Name": "DataVouchers_statusName",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.45,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.credit}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;;;",
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
              "Name": "DataVouchers_date",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.85,0,2.29,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.description}"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;;;",
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
            "6": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive6",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "d81ee5ad14f644ecab7bc8528d63b4a0"
            },
            "7": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive6",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "d81ee5ad14f644ecab7bc8528d63b4a0"
            },
            "8": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive7",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "13c23daaffcb4939bc0d73156f4a89f5"
            },
            "9": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive7",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "13c23daaffcb4939bc0d73156f4a89f5"
            },
            "10": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive8",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.2,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7e5ea4f2d20644a08c807ae89ca7b89a"
            },
            "11": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive8",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.2,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7e5ea4f2d20644a08c807ae89ca7b89a"
            },
            "12": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive10",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "0547fc2f7f714229a576252b509167fe"
            },
            "13": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive10",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "0547fc2f7f714229a576252b509167fe"
            },
            "14": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive11",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "6704048d77a44d509af7b3251c99d575"
            },
            "15": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive11",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "6704048d77a44d509af7b3251c99d575"
            },
            "16": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive12",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "0a212e275b4f400eac0243c255d08175"
            },
            "17": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive12",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "0a212e275b4f400eac0243c255d08175"
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
              "ClientRectangle": "4.9,0,0.5,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "Sum"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;;",
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
              "ClientRectangle": "5.5,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{Sum(DataVouchers,VouchersStdForm.debit)}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;;Bold;",
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
              "ClientRectangle": "6.5,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{Sum(DataVouchers,VouchersStdForm.credit)}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;;Bold;",
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
              "Name": "StartPointPrimitive9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "f9a8f43d78a04be7899ef81b11031b90"
            },
            "5": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "f9a8f43d78a04be7899ef81b11031b90"
            },
            "6": {
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
            "7": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive14",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ba8d15b78e1646b29b870c7a76b238b2"
            },
            "8": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive13",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "43e568c3cd0a44608f187b51a10411e0"
            },
            "9": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive13",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "43e568c3cd0a44608f187b51a10411e0"
            },
            "10": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive30",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "6e643229780f498bb36c55887ecd2239"
            },
            "11": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive30",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "6e643229780f498bb36c55887ecd2239"
            }
          }
        },
        "6": {
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
        "7": {
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
        },
        "8": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive7",
          "Guid": "2b96b7d448e947e38067847c50efbd2c",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.4,1.7,0.01,0.3",
          "Linked": true,
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "9": {
          "Ident": "StiStartPointPrimitive",
          "Name": "StartPointPrimitive23",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.2,2.1,0,0",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "ReferenceToGuid": "8f19add3617d41c89ce1a2688bb919f2"
        },
        "10": {
          "Ident": "StiStartPointPrimitive",
          "Name": "StartPointPrimitive24",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "8.1,2.1,0,0",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "ReferenceToGuid": "2b96b7d448e947e38067847c50efbd2c"
        },
        "11": {
          "Ident": "StiEndPointPrimitive",
          "Name": "EndPointPrimitive24",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "8.1,2.4,0,0",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "ReferenceToGuid": "2b96b7d448e947e38067847c50efbd2c"
        },
        "12": {
          "Ident": "StiStartPointPrimitive",
          "Name": "StartPointPrimitive25",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "4.6,2.1,0,0",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "ReferenceToGuid": "1487ab9012074160a189fcf60a52d8a8"
        },
        "13": {
          "Ident": "StiStartPointPrimitive",
          "Name": "StartPointPrimitive26",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "3.5,2.1,0,0",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "ReferenceToGuid": "5684e56c2330444280cad552149a5a2d"
        },
        "14": {
          "Ident": "StiStartPointPrimitive",
          "Name": "StartPointPrimitive27",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "2.4,2.1,0,0",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "ReferenceToGuid": "5ffb09a3979042bb86342ca11987680b"
        },
        "15": {
          "Ident": "StiStartPointPrimitive",
          "Name": "StartPointPrimitive28",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.3,2.1,0,0",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "ReferenceToGuid": "d44e4ccd9f2543c4bff2da465aed4aa6"
        },
        "16": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive14",
          "Guid": "43e568c3cd0a44608f187b51a10411e0",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "6.4,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "17": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive24",
          "Guid": "6e643229780f498bb36c55887ecd2239",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "5.4,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "18": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive12",
          "Guid": "ccf86188b6314bef816775f83a96c432",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "4.2,1.7,0.01,0.3",
          "Linked": true,
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "19": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive9",
          "Guid": "8dad7e7cc9cc417da559641180f3e9a9",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "5.4,1.7,0.01,0.3",
          "Linked": true,
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "20": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive2",
          "Guid": "25305b27b57e4b19a39ceb2cba9ca06c",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "6.4,1.7,0.01,0.3",
          "Linked": true,
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "21": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive15",
          "Guid": "e51f4b339cab47dca8b52e22493b01af",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.7,1.7,0.01,0.3",
          "Linked": true,
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "22": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive18",
          "Guid": "7763d3cc03d14afcadb9355599e51c02",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "0.6,1.7,0.01,0.3",
          "Linked": true,
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "23": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive11",
          "Guid": "d81ee5ad14f644ecab7bc8528d63b4a0",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "0.6,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "24": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive1",
          "Guid": "13c23daaffcb4939bc0d73156f4a89f5",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.7,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "25": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive3",
          "Guid": "7e5ea4f2d20644a08c807ae89ca7b89a",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "4.2,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "26": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive4",
          "Guid": "0547fc2f7f714229a576252b509167fe",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "5.4,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "27": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive5",
          "Guid": "6704048d77a44d509af7b3251c99d575",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "6.4,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "28": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive6",
          "Guid": "0a212e275b4f400eac0243c255d08175",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.4,2.4,0.01,0.3",
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
}'
where ReportID = 40 and LocaleID = 1

update Reporting.LocalReport set Template =  N'{
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
}'
where ReportID = 40 and LocaleID = 2

-- 1.1.1114
UPDATE [Metadata].[Column]
SET IsNullable = 1
WHERE ViewID = 11 AND Name = 'Server'

-- 1.1.1115
DELETE FROM [Metadata].[Column]
WHERE ColumnID > 677
GO

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [IsDynamic], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (678, 2, 'TypeName', 0, NULL, NULL, 'System.String', 'nvarchar', 'string', 0, 0, 0, 0, 0, 0, NULL, 16, NULL)
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
SET IDENTITY_INSERT [Metadata].[Column] OFF


-- 1.1.1121
Update [Reporting].[LocalReport] set Template = N'{
  "ReportVersion": "2019.2.3",
  "ReportGuid": "80079804335c44d4d4e79c5fa8af7531",
  "ReportName": "Report",
  "ReportAlias": "Report",
  "ReportFile": "Report.mrt",
  "ReportCreated": "/Date(1558670029000+0430)/",
  "ReportChanged": "/Date(1558961237000+0430)/",
  "EngineVersion": "EngineV2",
  "CalculationMode": "Interpretation",
  "ReportUnit": "Inches",
  "PreviewSettings": 268435455,
  "Styles": {
    "0": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "عنوان گزارش",
      "Name": "Tadbir_ReportTitle",
      "HorAlignment": "Center",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;14.25;Bold;",
      "Border": ";155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:255,0,0",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseNegativeTextBrush": true,
      "AllowUseTextFormat": true
    },
    "1": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "پاورقی گزارش",
      "Name": "Tadbir_ReportFooter",
      "HorAlignment": "Center",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;12;Bold;",
      "Border": ";155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:55,55,55",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseTextFormat": true
    },
    "2": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "عنوان صفحه",
      "Name": "Tadbir_PageHeader",
      "HorAlignment": "Center",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;14.25;Bold;",
      "Border": ";155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:55,55,55",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseTextFormat": true
    },
    "3": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "پاورقی صفحه",
      "Name": "Tadbir_PageFooter",
      "HorAlignment": "Center",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;11.25;Bold;",
      "Border": ";155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:55,55,55",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseBorderSidesFromLocation": true,
      "AllowUseNegativeTextBrush": true,
      "AllowUseTextFormat": true
    },
    "4": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "عنوان پارامتر ها",
      "Name": "Tadbir_ParameterTitle",
      "HorAlignment": "Right",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;12;Bold;",
      "Border": ";155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:0,0,0",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseNegativeTextBrush": true,
      "AllowUseTextFormat": true
    },
    "5": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "مقدار پارامتر ها",
      "Name": "Tadbir_ParameterValue",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;12;;",
      "Border": ";155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:139,69,19",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseTextFormat": true
    },
    "6": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "عنوان ستون متنی",
      "Name": "Tadbir_ColumnTextHeader",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;12;Bold;",
      "Border": "All;155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:211,211,211",
      "TextBrush": "solid:0,0,0",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseTextFormat": true
    },
    "7": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "عنوان ستون عددی",
      "Name": "Tadbir_ColumnNumberHeader",
      "HorAlignment": "Center",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;12;Bold;",
      "Border": "All;155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:211,211,211",
      "TextBrush": "solid:0,0,0",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseTextFormat": true
    },
    "8": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "داده متنی",
      "Name": "Tadbir_ColumnTextData",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;12;;",
      "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:0,0,0",
      "NegativeTextBrush": "solid:Transparent",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseTextFormat": true
    },
    "9": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "داده عددی",
      "Name": "Tadbir_ColumnNumberData",
      "HorAlignment": "Right",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;12;;",
      "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:0,0,0",
      "NegativeTextBrush": "solid:Transparent",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseTextFormat": true
    },
    "10": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "داده تاریخ",
      "Name": "Tadbir_ColumnDateData",
      "HorAlignment": "Center",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;12;;",
      "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:0,0,0",
      "NegativeTextBrush": "solid:Transparent",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseTextFormat": true
    },
    "11": {
      "Ident": "StiStyle",
      "CollectionName": "Tadbir",
      "Conditions": {
        "0": {
          "Ident": "StiStyleCondition",
          "Type": "ComponentType, Placement, PlacementNestedLevel",
          "Placement": "ReportTitle",
          "ComponentType": "Text, Primitive, Image, CheckBox"
        }
      },
      "Description": "شماره صفحه",
      "Name": "Tadbir_PageNumber",
      "HorAlignment": "Center",
      "VertAlignment": "Center",
      "Font": "IRANSansWeb;9.75;;",
      "Border": ";155,155,155;;;;;;solid:0,0,0",
      "Brush": "solid:Transparent",
      "TextBrush": "solid:55,55,55",
      "NegativeTextBrush": "solid:255,0,0",
      "AllowUseHorAlignment": true,
      "AllowUseVertAlignment": true,
      "AllowUseNegativeTextBrush": true,
      "AllowUseTextFormat": true
    }
  },
  "Dictionary": {
    "Variables": {
      "0": {
        "Name": "vReportTitle",
        "Alias": "عنوان گزارش",
        "Type": "System.String"
      },
      "1": {
        "Name": "vReportFirstTitle",
        "Alias": "عنوان ابتدایی گزارش",
        "Type": "System.String"
      },
      "2": {
        "Name": "vReportSummaryTitle",
        "Alias": "متن پاورقی گزارش",
        "Type": "System.String"
      }
    }
  },
  "Pages": {
    "0": {
      "Ident": "StiPage",
      "Name": "Page1",
      "Guid": "19d54b1fd3494d4f9caefe40a5cfde4b",
      "Interaction": {
        "Ident": "StiInteraction"
      },
      "Border": ";;2;;;;;solid:Black",
      "Brush": "solid:Transparent",
      "Components": {
        "0": {
          "Ident": "StiReportTitleBand",
          "Name": "ReportTitle",
          "ClientRectangle": "0,0.2,7.49,0.4",
          "Alias": "بخش عنوان گزارش - نمایش در صفحه اول گزارش",
          "Enabled": false,
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:Transparent",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "txtReportHeader",
              "Guid": "f67cff7056872b69bf5946e8a49b65d4",
              "ClientRectangle": "2.8,0,2.1,0.3",
              "ComponentStyle": "Tadbir_PageNumber",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{vReportFirstTitle}"
              },
              "AutoWidth": true,
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;9.75;;",
              "Border": ";155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:55,55,55",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression"
            }
          }
        },
        "1": {
          "Ident": "StiPageFooterBand",
          "Name": "PageFooter",
          "ClientRectangle": "0,11.29,7.49,0.4",
          "Alias": "بخش فوتر صفحه - نمایش در همه صفحات گزارش",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:Transparent",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "txtPageNumber",
              "Guid": "591b0a4d9e59c4fd83ad1dbd4588ee4e",
              "ClientRectangle": "3.2,-0.02,1.3,0.3",
              "Alias": "شماره صفحه",
              "ComponentStyle": "Tadbir_PageNumber",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{PageNumber}"
              },
              "AutoWidth": true,
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;9.75;;",
              "Border": ";155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:55,55,55",
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text5",
              "Guid": "687905e92a7a81a3d1aaa4508871ae62",
              "ClientRectangle": "0.3,0.11,2,0.2",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{DateToStr(DateTime.Now(),true)}"
              },
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:Black",
              "Type": "Expression"
            }
          }
        },
        "2": {
          "Ident": "StiPageHeaderBand",
          "Name": "PageHeader",
          "ClientRectangle": "0,1,7.49,0.8",
          "Alias": "بخش عنوان صفحه - نمایش در همه صفحات گزارش",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:Transparent",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "txtPageHeader",
              "Guid": "557c3f85101d4160a57980988cfc1cc1",
              "ClientRectangle": "2.9,0.1,2.1,0.5",
              "ComponentStyle": "Tadbir_ReportTitle",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{vReportTitle}"
              },
              "AutoWidth": true,
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;14.25;Bold;",
              "Border": ";155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:255,0,0",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression"
            }
          }
        },
        "3": {
          "Ident": "StiTable",
          "Name": "Table1",
          "ClientRectangle": "0,2.2,7.49,0.3",
          "Interaction": {
            "Ident": "StiBandInteraction"
          },
          "Border": ";;;None;;;;solid:Black",
          "Brush": "solid:Transparent",
          "Components": {
            "0": {
              "Ident": "StiTableCell",
              "Name": "Table1_Cell1",
              "Guid": "bc5fa7bb16d255d99db5551b5fec13e6",
              "ClientRectangle": "0,0,1.9,0.3",
              "Restrictions": "AllowMove, AllowSelect, AllowChange",
              "ComponentStyle": "Tadbir_ParameterTitle",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "از تاریخ"
              },
              "HorAlignment": "Right",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;Bold;",
              "Border": ";155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:0,0,0",
              "TextOptions": {
                "RightToLeft": true
              },
              "Margins": {
                "Left": 5,
                "Right": 0,
                "Top": 0,
                "Bottom": 0
              },
              "Type": "Expression",
              "CellDockStyle": "Right",
              "ID": 0
            },
            "1": {
              "Ident": "StiTableCell",
              "Name": "Table1_Cell2",
              "Guid": "253ef13ce543bbad6a0da14fa476ad19",
              "ClientRectangle": "1.9,0,1.9,0.3",
              "Restrictions": "AllowMove, AllowSelect, AllowChange",
              "ComponentStyle": "Tadbir_ParameterValue",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "1397/01/01"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;;",
              "Border": ";155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:139,69,19",
              "TextOptions": {
                "RightToLeft": true
              },
              "Margins": {
                "Left": 9,
                "Right": 0,
                "Top": 0,
                "Bottom": 0
              },
              "Type": "Expression",
              "CellDockStyle": "Right",
              "ID": 1
            },
            "2": {
              "Ident": "StiTableCell",
              "Name": "Table1_Cell3",
              "Guid": "38081d6522a5daa7f6fd1b1fbd5ece5f",
              "ClientRectangle": "3.8,0,1.8,0.3",
              "Restrictions": "AllowMove, AllowSelect, AllowChange",
              "ComponentStyle": "Tadbir_ParameterTitle",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "تا تاریخ"
              },
              "HorAlignment": "Right",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;Bold;",
              "Border": ";155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:0,0,0",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression",
              "CellDockStyle": "Right",
              "ID": 2
            },
            "3": {
              "Ident": "StiTableCell",
              "Name": "Table1_Cell4",
              "Guid": "3a930c25fea4cb09525477618d29cd96",
              "ClientRectangle": "5.6,0,1.8,0.3",
              "Restrictions": "AllowMove, AllowSelect, AllowChange",
              "ComponentStyle": "Tadbir_ParameterValue",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "1397/05/05"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;;",
              "Border": ";155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:139,69,19",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression",
              "CellDockStyle": "Right",
              "ID": 3
            }
          },
          "MinHeight": 0.1,
          "AutoWidth": "Page",
          "RowCount": 1,
          "ColumnCount": 4,
          "NumberID": 25
        },
        "4": {
          "Ident": "StiColumnHeaderBand",
          "Name": "ColumnHeaderBand",
          "CanShrink": true,
          "ClientRectangle": "0,2.9,7.49,0.4",
          "Alias": "بخش عناوین ستون ها",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:Transparent",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "txtColumnHeader",
              "Guid": "10214f235f3f47c399dfd5a98a68f584",
              "CanShrink": true,
              "CanGrow": true,
              "ClientRectangle": "3.1,0,1.5,0.4",
              "ComponentStyle": "Tadbir_ColumnNumberHeader",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "عنوان ستون عددی"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;Bold;",
              "Border": "All;155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:211,211,211",
              "TextBrush": "solid:0,0,0",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text2",
              "Guid": "09ba599ceb81bfbc8b5d225b319a7be7",
              "CanShrink": true,
              "CanGrow": true,
              "ClientRectangle": "4.6,0,1.5,0.4",
              "ComponentStyle": "Tadbir_ColumnTextHeader",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "عنوان ستون متنی"
              },
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;Bold;",
              "Border": "All;155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:211,211,211",
              "TextBrush": "solid:0,0,0",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression"
            }
          }
        },
        "5": {
          "Ident": "StiDataBand",
          "Name": "DataBand1",
          "CanShrink": true,
          "ClientRectangle": "0,3.7,7.49,0.3",
          "Alias": "بخش دیتا یا رکورد های اطلاعاتی",
          "Interaction": {
            "Ident": "StiBandInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:Transparent",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text1",
              "Guid": "cb4f22991c5651ad674e5621cdd7d417",
              "CanShrink": true,
              "CanGrow": true,
              "GrowToHeight": true,
              "ClientRectangle": "3.1,0,1.5,0.3",
              "ComponentStyle": "Tadbir_ColumnNumberData",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "داده عددی"
              },
              "CanBreak": true,
              "HorAlignment": "Right",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;;",
              "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:0,0,0",
              "TextOptions": {
                "WordWrap": true
              },
              "Margins": {
                "Left": 6,
                "Right": 0,
                "Top": 0,
                "Bottom": 0
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text3",
              "Guid": "d23cd7875de59e1c86e54bf185ec1a46",
              "CanShrink": true,
              "CanGrow": true,
              "GrowToHeight": true,
              "ClientRectangle": "4.6,0,1.5,0.3",
              "ComponentStyle": "Tadbir_ColumnTextData",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "داده متنی"
              },
              "CanBreak": true,
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;;",
              "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:0,0,0",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Margins": {
                "Left": 0,
                "Right": 6,
                "Top": 0,
                "Bottom": 0
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text4",
              "Guid": "6fa74da912e564b3065fe4e3433742b4",
              "CanShrink": true,
              "CanGrow": true,
              "GrowToHeight": true,
              "ClientRectangle": "1.6,0,1.5,0.3",
              "ComponentStyle": "Tadbir_ColumnDateData",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "داده تاریخ"
              },
              "CanBreak": true,
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;;",
              "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:0,0,0",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            }
          }
        },
        "6": {
          "Ident": "StiReportSummaryBand",
          "Name": "ReportSummary",
          "ClientRectangle": "0,4.4,7.49,0.3",
          "Alias": "بخش فوتر گزارش - نمایش در صفحه آخر گزارش",
          "Enabled": false,
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:Transparent",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "txtReportFooter",
              "Guid": "f9efae84676440e7bae58c451dec3b9c",
              "ClientRectangle": "2.5,0,2.6,0.3",
              "ComponentStyle": "Tadbir_ReportFooter",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{vReportSummaryTitle}"
              },
              "AutoWidth": true,
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "IRANSansWeb;12;Bold;",
              "Border": ";155,155,155;;;;;;solid:0,0,0",
              "Brush": "solid:Transparent",
              "TextBrush": "solid:55,55,55",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression"
            }
          }
        }
      },
      "PaperSize": "A4",
      "TitleBeforeHeader": true,
      "PageWidth": 8.27,
      "PageHeight": 11.69,
      "Watermark": {
        "TextBrush": "solid:50,0,0,0"
      },
      "Margins": {
        "Left": 0.39,
        "Right": 0.39,
        "Top": 0,
        "Bottom": 0
      }
    }
  }
}' where localeid = 2 and ReportID = 43

-- 1.1.1125
CREATE TABLE [Core].[SystemError](
	[SystemErrorID]  INT          IDENTITY (1, 1) NOT NULL,
	[CompanyID]      INT          NULL,
	[FiscalPeriodID] INT          NULL,
	[BranchID]       INT          NULL,
	[TimestampUtc]   VARCHAR(32)  NOT NULL,
	[Code]           INT          NOT NULL,
	[Message]        VARCHAR(256) NOT NULL,
	[FaultingMethod] VARCHAR(64)  NOT NULL,
	[FaultType]      VARCHAR(64)  NOT NULL,
	[StackTrace]     TEXT         NULL,
	[Version]        VARCHAR(16)  CONSTRAINT [DF_Core_SystemError_Version] DEFAULT ('1.0') NOT NULL
    , CONSTRAINT [PK_Core_SystemError] PRIMARY KEY CLUSTERED  ([SystemErrorID] ASC)
    , CONSTRAINT [FK_Core_SystemError_Config_CompanyDb] FOREIGN KEY ([CompanyID]) REFERENCES [Config].[CompanyDb] ([CompanyID])
)
GO

-- 1.1.1137
DELETE FROM [Reporting].[SystemIssue]
WHERE SystemIssueID > 3

SET IDENTITY_INSERT [Reporting].[SystemIssue] ON 
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

-- 1.1.1138
CREATE TABLE [Metadata].[ShortcutCommand] (
    [ShortcutCommandID]   INT              IDENTITY (1, 1) NOT NULL,
    [PermissionID]        INT              NULL,
    [Name]                VARCHAR(128)     NOT NULL,
    [Scope]               VARCHAR(64)      NULL,
    [HotKey]              VARCHAR(32)      NOT NULL,
    [Method]              VARCHAR(128)     NOT NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_ShortcutCommand_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Metadata_ShortcutCommand_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_ShortcutCommand] PRIMARY KEY CLUSTERED ([ShortcutCommandID] ASC)
    , CONSTRAINT [FK_Metadata_ShortcutCommand_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission]([PermissionID])
)
GO


-- 1.1.1148
SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (35, N'SpecialVoucherOps', N'SpecialVoucher')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (206, 35, N'IssueOpeningVoucher', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (207, 35, N'IssueClosingTempAccountsVoucher', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (208, 35, N'IssueClosingVoucher', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (209, 35, N'UncheckClosingVoucher', 8)
SET IDENTITY_INSERT [Auth].[Permission] OFF

UPDATE [Metadata].[Command]
SET PermissionID = 45
WHERE TitleKey = 'IssueOpeningVoucher'

UPDATE [Metadata].[Command]
SET PermissionID = 45
WHERE TitleKey = 'ClosingTempAccounts'

UPDATE [Metadata].[Command]
SET PermissionID = 45
WHERE TitleKey = 'IssueClosingVoucher'

-- 1.1.1152
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (35, N'RoleAccess')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (57, N'CompanyAccess')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

-- 1.1.1153
SET IDENTITY_INSERT [Config].[SysLogSetting] ON
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (29, NULL, 1, 35, 1)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (30, NULL, 2, 57, 1)
SET IDENTITY_INSERT [Config].[SysLogSetting] OFF

-- 1.1.1159
UPDATE [Metadata].[Command]
SET PermissionID = 45
WHERE CommandID = 13

UPDATE [Metadata].[Command]
SET PermissionID = 60
WHERE CommandID = 14

UPDATE [Metadata].[Command]
SET PermissionID = 179
WHERE CommandID = 48

UPDATE [Metadata].[Command]
SET PermissionID = 189
WHERE CommandID = 49

-- 1.1.1166
UPDATE [Metadata].[View]
SET EntityName = 'Company'
WHERE Name = 'CompanyDb'

UPDATE [Metadata].[View]
SET EntityName = 'ItemBalance'
WHERE ViewID >= 43 AND ViewID <= 57

UPDATE [Metadata].[View]
SET EntityName = 'AccountCollection'
WHERE Name = 'AccountCollectionAccount'

UPDATE [Metadata].[View]
SET SearchUrl = '/accounts/lookup'
WHERE Name = 'Account'

-- 1.1.1175
Update Metadata.Command Set RouteUrl = '/admin/changePassword' Where TitleKey = 'ChangePassword'
Update Metadata.Command Set IconName = 'folder-close' Where RouteUrl is Null
Update Metadata.Command Set IconName = 'th-large' Where TitleKey = 'AccountGroup'
Update Metadata.Command Set IconName = 'th-list' Where TitleKey = 'Account'
Update Metadata.Command Set IconName = 'th' Where TitleKey = 'DetailAccount'
Update Metadata.Command Set IconName = 'tower' Where TitleKey = 'CostCenter'
Update Metadata.Command Set IconName = 'file' Where TitleKey = 'Project'
Update Metadata.Command Set IconName = 'transfer' Where TitleKey = 'AccountRelations'
Update Metadata.Command Set IconName = 'usd' Where TitleKey = 'Currency'
Update Metadata.Command Set IconName = 'plus' Where TitleKey = 'NewVoucher'
Update Metadata.Command Set IconName = 'search' Where TitleKey = 'VoucherByNo'
Update Metadata.Command Set IconName = 'lock' Where TitleKey = 'RowAccessSettings'
Update Metadata.Command Set IconName = 'wrench' Where TitleKey = 'Settings'

-- 1.1.1176
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (10, N'Design')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (58, N'PrintPreview')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name]) VALUES (9, N'UserReport')
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Config].[SysLogSetting] ON
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (31, NULL, 5, 58, 1)
INSERT INTO [Config].[SysLogSetting] (SysLogSettingID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (32, NULL, 9, 10, 1)
SET IDENTITY_INSERT [Config].[SysLogSetting] OFF

-- 1.1.1182
UPDATE [Reporting].[Report]
SET ServiceUrl = 'reports/voucher/{0}/std-form'
WHERE Code = 'Voucher-Std-Form'

UPDATE [Reporting].[Report]
SET ServiceUrl = 'reports/voucher/{0}/std-form-detail'
WHERE Code = 'Voucher-Std-Form-Detail'

-- 1.1.1202
UPDATE [Metadata].[Column]
SET [Type] = '2'
WHERE DotNetType LIKE 'System.Date%'

-- 1.1.1203
UPDATE [Metadata].[Column]
SET [Type] = 'Default'
WHERE DotNetType LIKE 'System.Date%'
