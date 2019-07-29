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

