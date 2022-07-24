USE NGTadbirSys
GO

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

-- 1.1.1215
UPDATE [Metadata].[Column]
SET AllowSorting = 0, AllowFiltering = 0, IsNullable = 1
WHERE [Name] = 'RowNo'

-- 1.1.1230
UPDATE [Metadata].[Column]
SET StorageType = 'time', [Length] = 7
WHERE ViewID IN(13, 59, 60, 61) AND [Name] = 'Time'

-- 1.1.1238
UPDATE [Reporting].[Report]
SET ViewId = 2
WHERE Code = 'Voucher-Std-Form'

UPDATE [Reporting].[Report]
SET ViewId = 2
WHERE Code = 'Voucher-Std-Form-Detail'

-- 1.2.1257
SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (82, 20, 1, 2, 2, '', 'reports/finance/voucher-by-no/{0}/by-detail', 0, 1, 0, 0)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] (LocalReportID, LocaleID, ReportID, Caption)
    VALUES (255, 1, 82, 'Simple - by detail level')
INSERT INTO [Reporting].[LocalReport] (LocalReportID, LocaleID, ReportID, Caption)
    VALUES (256, 2, 82, N'ساده - در سطح تفصیلی')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey])
    VALUES (164, 82, N'no', N'no', N'EQ', N'System.Int32', N'TextBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF


UPDATE [Reporting].[Report]
SET ServiceUrl = 'reports/finance/vouchers/sum-by-date'
WHERE Code = 'Voucher-Sum-By-Date'

UPDATE [Reporting].[Report]
SET ServiceUrl = 'reports/finance/voucher-by-no/{0}/std-form'
WHERE Code = 'Voucher-Std-Form'

UPDATE [Reporting].[Report]
SET ServiceUrl = 'reports/finance/voucher-by-no/{0}/std-form-detail'
WHERE Code = 'Voucher-Std-Form-Detail'

-- 1.2.1261
SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (83, 20, 1, 2, 2, '', 'reports/finance/voucher-by-no/{0}/by-ledger', 0, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (84, 20, 1, 2, 2, '', 'reports/finance/voucher-by-no/{0}/by-subsid', 0, 1, 0, 0)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] (LocalReportID, LocaleID, ReportID, Caption)
    VALUES (257, 1, 83, 'Aggregate - by ledger level')
INSERT INTO [Reporting].[LocalReport] (LocalReportID, LocaleID, ReportID, Caption)
    VALUES (258, 2, 83, N'مرکب - در سطح کل')
INSERT INTO [Reporting].[LocalReport] (LocalReportID, LocaleID, ReportID, Caption)
    VALUES (259, 1, 84, 'Aggregate - by subsidiary level')
INSERT INTO [Reporting].[LocalReport] (LocalReportID, LocaleID, ReportID, Caption)
    VALUES (260, 2, 84, N'مرکب - در سطح معین')
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey])
    VALUES (165, 83, N'no', N'no', N'EQ', N'System.Int32', N'TextBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo')
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey])
    VALUES (166, 84, N'no', N'no', N'EQ', N'System.Int32', N'TextBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

-- 1.2.1267
SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey])
    VALUES (167, 41, N'no', N'no', N'EQ', N'System.Int32', N'TextBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

DELETE [Metadata].[ShortcutCommand]

SET IDENTITY_INSERT [Metadata].[ShortcutCommand] ON
	INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method])
    VALUES (1, NULL, N'NewVoucherLine', N'VoucherLineComponent', N'Ctrl+O', N'addNew')
	
	INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method])
    VALUES (2, NULL, N'ExportToExcel', NULL, N'Ctrl+Alt+X', N'exportToExcel')	
	
	INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method])
    VALUES (3, NULL, N'Print', NULL, N'Ctrl+Alt+P', N'print')
	
	INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method])
    VALUES (4, NULL, N'ReportSetting', NULL, N'Ctrl+Alt+S', N'openReportSetting')
	
	INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method])
    VALUES (5, NULL, N'AdvanceFilter', NULL, N'Ctrl+Alt+A', N'openAdvanceFilter')
	
SET IDENTITY_INSERT [Metadata].[ShortcutCommand] OFF

-- 1.2.1271
UPDATE [Reporting].[Report] SET Code = 'Voucher-By-Detail' WHERE ReportID = 82

UPDATE [Reporting].[Report] SET Code = 'Voucher-By-Ledger' WHERE ReportID = 83

UPDATE [Reporting].[Report] SET Code = 'Voucher-By-Subsid' WHERE ReportID = 84

-- 1.2.1279
SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (85, 16, 1, 17, 2, 'Journal-ByDate-ByLedger', N'reports/journal/by-date/by-ledger', 0, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (86, 16, 1, 18, 2, 'Journal-ByDate-BySubsidiary', N'reports/journal/by-date/by-subsid', 0, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (87, 16, 1, 24, 2, 'Journal-ByNo-ByLedger', N'reports/journal/by-no/by-ledger', 0, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (88, 16, 1, 25, 2, 'Journal-ByNo-BySubsidiary', N'reports/journal/by-no/by-subsid', 0, 1, 0, 0)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (261, 1, 85, 'Journal in Ledger Level - By Date', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (262, 2, 85, N'دفتر روزنامه در سطح کل - بر اساس تاریخ', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (263, 1, 86, 'Journal in Subsidiary Level - By Date', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (264, 2, 86, N'دفتر روزنامه در سطح معین - بر اساس تاریخ', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (265, 1, 87, 'Journal in Ledger Level - By Number', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (266, 2, 87, N'دفتر روزنامه در سطح کل - بر اساس شماره سند', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (267, 1, 88, 'Journal in Subsidiary Level - By Number', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (268, 2, 88, N'دفتر روزنامه در سطح معین - بر اساس شماره سند', NULL)
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (168, 85, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (169, 85, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (170, 86, 'fromDate', 'from', 'EQ', 'System.DateTime', 'QueryString', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (171, 86, 'toDate', 'to', 'EQ', 'System.DateTime', 'QueryString', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (172, 87, 'fromNo', 'from', 'EQ', 'System.Int32', 'NumberBox', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (173, 87, 'toNo', 'to', 'EQ', 'System.Int32', 'NumberBox', 'ToNo', 'ToNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (174, 88, 'fromNo', 'from', 'EQ', 'System.Int32', 'NumberBox', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (175, 88, 'toNo', 'to', 'EQ', 'System.Int32', 'NumberBox', 'ToNo', 'ToNo')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

-- 1.2.1284
ALTER TABLE [Reporting].[Parameter]
ADD [Source] NVARCHAR(64) NULL
GO

UPDATE [Reporting].[Parameter]
SET [Source] = [ControlType]


-- 1.2.1289

DELETE [Reporting].[Parameter]

SET IDENTITY_INSERT [Reporting].[Parameter] ON 
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (1, 27, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'84732780-f776-403e-8ed3-e31e13610dd3', CAST(N'2020-10-25T15:42:02.040' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (2, 27, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'fe82f62e-e911-4235-8e6a-faba397fd027', CAST(N'2020-10-25T15:42:02.040' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (3, 28, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'f6874908-874c-49ae-b4de-16cdd227582d', CAST(N'2020-10-25T15:42:02.040' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (4, 28, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'c321285b-1c15-44f1-94f9-dfc29418062e', CAST(N'2020-10-25T15:42:02.053' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (5, 29, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'72c50d03-59cc-4540-be4b-da6531318ca6', CAST(N'2020-10-25T15:42:02.057' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (6, 29, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'898c7ace-e2dc-45ac-8984-4b2ff9bcda7a', CAST(N'2020-10-25T15:42:02.057' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (7, 30, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ef60f87e-2740-4c0f-b36a-a487cfb5a3a3', CAST(N'2020-10-25T15:42:02.057' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (8, 30, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'eb69507d-b6d2-4327-81e3-7d711cf675be', CAST(N'2020-10-25T15:42:02.057' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (9, 31, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ccc37b76-145e-4057-8cd5-c0e1484b3a10', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (10, 31, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'18b1a8ac-09f2-48b4-9190-7d6d748c4018', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (11, 32, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'888350e5-01d5-4fc0-a22a-758adada5ea3', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (12, 32, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'aa0a98e4-ed63-4c1d-839c-3db749ce7ec4', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (13, 33, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'ec00fcfa-5bf6-4f12-9f3a-68ee5439d73d', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (14, 33, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'952696f4-49cd-4637-bec3-37eae53e0c78', CAST(N'2020-10-25T15:42:02.060' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (15, 34, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'efaa89c3-d6b7-4a5b-870d-10bed60212b1', CAST(N'2020-10-25T15:42:02.063' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (16, 34, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'00216b6f-560a-4fa7-9f4b-8f4990f19ea8', CAST(N'2020-10-25T15:42:02.063' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (17, 35, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'b9ecb187-06e0-4785-b224-6f3103fb3898', CAST(N'2020-10-25T15:42:02.063' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (18, 35, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'5e5eb692-c608-467f-bf0e-4810b29cff7c', CAST(N'2020-10-25T15:42:02.063' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (19, 36, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'93f0375e-2c16-404e-b0e7-e443b01bcb29', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (20, 36, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'55e07ff1-be8e-4f82-930c-938ca0060924', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (21, 37, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'f66e70ac-0adf-4bb9-890c-5d7792b5823d', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (22, 37, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'797e64e5-7a8c-4f35-837f-b4b5fd89f868', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (23, 38, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'50b85b6f-6df4-4367-b0aa-5743a65e8372', CAST(N'2020-10-25T15:42:02.067' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (24, 38, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'5b22b731-a0c2-4f8c-aff7-5b6ea83b3ded', CAST(N'2020-10-25T15:42:02.070' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (25, 39, N'from', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'96d5bf6f-448a-4d54-8e53-8168e8e58786', CAST(N'2020-10-25T15:42:02.070' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (26, 39, N'to', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'2ede8dfa-3141-43cc-bfee-35cbc4f19f75', CAST(N'2020-10-25T15:42:02.070' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (39, 39, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'3f40ce4e-b112-47ea-b732-410dbfa82d93', CAST(N'2020-10-25T15:42:02.087' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (40, 39, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'fc8433f6-543f-4cb6-99ed-cbe45b2bfd3c', CAST(N'2020-10-25T15:42:02.087' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (42, 40, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'bd82ec0d-9c10-4e32-ab9c-46027788ef92', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (43, 46, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'5512defe-4a18-4ce9-938d-70c0d5947627', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (44, 46, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'11fc43dc-35cb-4ca3-883c-b851e7a96369', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (45, 46, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7e2132f2-c1af-48bf-a527-a0cadc06a7a4', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (46, 46, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'a199be92-9258-43c1-88a9-282c11afcf83', CAST(N'2020-10-25T15:42:02.090' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (48, 47, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'3df0a65d-d678-4990-8ecd-ee671202d211', CAST(N'2020-10-25T15:42:02.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (49, 47, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'1eb88cbe-6d53-4e60-9e1f-0eb661a3fcf6', CAST(N'2020-10-25T15:42:02.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (50, 47, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7799a459-7178-496f-b87f-1056ed78cdad', CAST(N'2020-10-25T15:42:02.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (51, 47, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'57ffe553-bdaa-45b0-9a90-cb01f33d2f69', CAST(N'2020-10-25T15:42:02.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (53, 48, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'f2c6a59f-e869-4837-a90c-8864e22bf574', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (54, 48, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'bf2aa88a-112a-4e7b-b6bb-5e208ffa156c', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (55, 48, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'a83cee27-1b2f-4a31-a097-0299ad828e35', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (56, 48, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'21b5b77b-34a4-4bfc-b414-1f2ff1c1d008', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (58, 49, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'36c7abbb-909c-482b-ba9c-04f3a2865386', CAST(N'2020-10-25T15:42:02.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (59, 49, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'22ae306d-eeb8-4b5c-916c-3dc893411cd9', CAST(N'2020-10-25T15:42:02.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (60, 49, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'27517b43-b9ef-4d45-a978-4ce5e98e4474', CAST(N'2020-10-25T15:42:02.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (61, 49, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'17e9538a-f44a-4727-863f-4cf1efda5c5a', CAST(N'2020-10-25T15:42:02.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (63, 50, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'9923e064-7905-40ba-93e9-55d3eacd4912', CAST(N'2020-10-25T15:45:55.800' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (64, 50, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'f7275c15-a72e-4f58-9b80-f8f3fcf02773', CAST(N'2020-10-25T15:45:55.800' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (65, 50, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'cf037b90-4e08-468d-ba45-dbe2ad63cd20', CAST(N'2020-10-25T15:45:55.803' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (66, 50, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'997f82d4-8c43-40d6-bab1-6f09a6b720dd', CAST(N'2020-10-25T15:45:55.807' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (68, 51, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'1111d526-70bb-479c-805b-3414a937c741', CAST(N'2020-10-25T15:45:55.810' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (69, 51, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'6258bdbb-e6ee-4905-a946-3b4ad91ae395', CAST(N'2020-10-25T15:45:55.810' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (70, 51, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7581d672-a482-4be4-b6a8-314ef0432f27', CAST(N'2020-10-25T15:45:55.813' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (71, 51, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'ce613fa4-22dd-4657-9c36-f7e3aa92d419', CAST(N'2020-10-25T15:45:55.813' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (73, 52, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'd4f519e3-ae51-47f8-88df-086f0e01a826', CAST(N'2020-10-25T15:45:55.817' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (74, 52, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'f93969ab-00ac-4a96-8312-9eff293c88b7', CAST(N'2020-10-25T15:45:55.820' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (75, 52, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'c3b01215-3890-496f-b42f-7a65d899e3e9', CAST(N'2020-10-25T15:45:55.820' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (76, 52, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'8724a10f-6237-492c-80a7-baf0e8f04347', CAST(N'2020-10-25T15:45:55.820' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (78, 53, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'2a59b010-3922-417c-a056-735005ffcf46', CAST(N'2020-10-25T15:45:55.823' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (79, 53, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'd20f07be-f1d8-475c-adb4-6f2b4254d654', CAST(N'2020-10-25T15:45:55.827' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (80, 53, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'cf2b9df4-f0bc-4c34-8edc-f53e87e39dbb', CAST(N'2020-10-25T15:45:55.827' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (81, 53, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'9ee7ad78-83d8-4a4a-8fbe-1bb89f3c2596', CAST(N'2020-10-25T15:45:55.830' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (83, 54, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'e206c2e5-cb0f-4450-a51e-176be8506add', CAST(N'2020-10-25T15:45:55.830' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (84, 54, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'12da3b9a-d05b-4901-87fd-66813cadb1fa', CAST(N'2020-10-25T15:45:55.833' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (85, 54, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7982f7e1-f3a6-4f86-aa5a-f0fc9e02e95d', CAST(N'2020-10-25T15:45:55.833' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (86, 54, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'fa52667e-0e87-4f59-9dee-f35568c22cd0', CAST(N'2020-10-25T15:45:55.837' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (88, 55, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'125ed154-b199-492f-916b-c2664d1b5e3d', CAST(N'2020-10-25T15:45:55.840' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (89, 55, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'67ff4255-574d-46fd-9429-4ee14f698805', CAST(N'2020-10-25T15:45:55.840' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (90, 55, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'd99a5611-538e-4ce5-9980-ebdd5de827c2', CAST(N'2020-10-25T15:45:55.840' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (91, 55, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'0c582466-67e0-4100-9b7e-062e4de690ed', CAST(N'2020-10-25T15:45:55.843' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (93, 56, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'97c111d3-4dc5-4896-963c-6c1ea0d10fc4', CAST(N'2020-10-25T15:45:55.847' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (94, 56, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'df384db3-3c42-4891-94b9-d1560a7e1747', CAST(N'2020-10-25T15:45:55.847' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (95, 56, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'0ad1e482-57e7-4f6c-a53f-4b1c15ae80ce', CAST(N'2020-10-25T15:45:55.847' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (96, 56, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'3643b73f-570b-4ae5-997d-2f626471afe1', CAST(N'2020-10-25T15:45:55.850' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (98, 57, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'42856328-8f76-4cf0-be84-b7652e11c443', CAST(N'2020-10-25T15:45:55.850' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (99, 57, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'561001e3-e645-4943-aa44-660549053df6', CAST(N'2020-10-25T15:45:55.853' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (100, 57, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'ff420147-b3ce-4b78-b659-fad13b2558db', CAST(N'2020-10-25T15:45:55.853' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (101, 57, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'29679a94-323f-4002-9430-60b501683fb6', CAST(N'2020-10-25T15:45:55.857' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (103, 58, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'8715b9f5-f3b6-48d1-88a9-a20379012ec5', CAST(N'2020-10-25T15:45:55.857' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (104, 58, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'f90505ac-b7c2-4419-8ff3-0004bad4b758', CAST(N'2020-10-25T15:45:55.860' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (105, 58, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'e6cda994-564c-4440-b677-543964ffe793', CAST(N'2020-10-25T15:45:55.860' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (106, 58, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'2985882d-4613-4dc1-a818-46c4e74a4a5a', CAST(N'2020-10-25T15:45:55.860' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (108, 59, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'9f883d6e-be2d-487c-b7d7-56b4f252bc55', CAST(N'2020-10-25T15:45:55.863' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (109, 59, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'1e5b3e5f-596b-4299-a20a-8bec1e71ff4e', CAST(N'2020-10-25T15:45:55.863' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (110, 59, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'a2eda7d9-9894-49be-845a-85eceed626ba', CAST(N'2020-10-25T15:45:55.867' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (111, 59, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'7e811526-91a5-4473-9087-fa97e654d25a', CAST(N'2020-10-25T15:45:55.867' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (113, 60, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'e31ad3b2-8865-427a-a5e2-b7e895419731', CAST(N'2020-10-25T15:45:55.870' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (114, 60, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'c8606466-1d25-43d1-97a3-be2b153ef51a', CAST(N'2020-10-25T15:45:55.873' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (115, 60, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'd61e11b4-3d2b-4de0-b3ca-46cde7dc4edf', CAST(N'2020-10-25T15:45:55.877' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (116, 60, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'87c087d0-a488-4c61-b994-536e98252a93', CAST(N'2020-10-25T15:45:55.877' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (118, 61, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ed57b093-d280-468d-a696-b487cf0fd157', CAST(N'2020-10-25T15:45:55.880' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (119, 61, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'30ea1b47-3af8-46ff-8129-419954bdc7c6', CAST(N'2020-10-25T15:45:55.880' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (120, 61, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'7f01ae23-c02b-475f-b01c-fd80c03a05da', CAST(N'2020-10-25T15:45:55.883' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (121, 61, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'6783385a-2212-4f5c-8384-b5f85656a383', CAST(N'2020-10-25T15:45:55.887' AS DateTime), NULL)
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (123, 62, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'43808c19-895e-4043-9260-141d65e41470', CAST(N'2020-11-16T16:33:28.113' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (124, 62, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'0ea78a3e-a1e0-45c4-8341-f67e7184a40b', CAST(N'2020-11-16T16:33:28.113' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (126, 63, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'003cb6c3-b74d-423b-ba7b-4276acfbf763', CAST(N'2020-11-16T16:33:28.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (127, 63, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'8a53e7c9-5189-4488-9d04-f4ee330e86b7', CAST(N'2020-11-16T16:33:28.117' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (129, 64, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'0ef97b82-c0a7-4b6c-af31-793c8067506a', CAST(N'2020-11-16T16:33:28.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (130, 64, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'4ebc103f-fda0-40df-8cbb-e857bc53f571', CAST(N'2020-11-16T16:33:28.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (132, 65, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'd816ef3b-0f2f-4013-b58d-3133977296b6', CAST(N'2020-11-16T16:33:28.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (133, 65, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'01489499-56e4-482d-8eb7-95501671096a', CAST(N'2020-11-16T16:33:28.120' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (135, 66, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'530bdd56-2bc7-4fc3-95a1-6e193f761039', CAST(N'2020-11-16T16:33:28.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (136, 66, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'8f870c99-f391-4a08-aefa-aa5210322bb3', CAST(N'2020-11-16T16:33:28.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (138, 67, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'867728b1-54da-4808-bcee-fbeccd4a0fd9', CAST(N'2020-11-16T16:33:28.123' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (139, 67, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'174303f5-9b9d-4a0b-ac86-50e1d6a4bab2', CAST(N'2020-11-16T16:33:28.127' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (141, 68, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'00a575a6-5aed-4588-89c7-e424dfc0a6c9', CAST(N'2020-11-16T16:33:28.127' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (142, 68, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'2a07a9bf-5b5d-4bfe-bf88-bf0df0b8f488', CAST(N'2020-11-16T16:33:28.127' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (144, 69, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'ee4f1225-2534-4284-8656-ca291efdd87d', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (145, 69, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'831d75cc-4468-49d6-b12c-75c712f1376b', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (146, 69, N'fromNo', N'from', N'EQ', N'System.Int32', N'TextBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'0d250786-4a36-4663-9a8c-51430f0c2715', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (147, 69, N'toNo', N'to', N'EQ', N'System.Int32', N'TextBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'b03fd7c8-30c3-4171-bc53-50e4590f8840', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (149, 70, N'fromDate', N'Date', N'GTE', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'8385212d-2096-4b29-9832-df3844384bef', CAST(N'2020-11-16T16:33:28.130' AS DateTime), N'GridOption')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (150, 70, N'toDate', N'Date', N'LTE', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'350f3c09-7198-418a-9540-d57c2bda8559', CAST(N'2020-11-16T16:33:28.133' AS DateTime), N'GridOption')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (151, 71, N'fromDate', N'Date', N'GTE', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'abe24fa4-74b9-4731-a086-fca4599f181c', CAST(N'2020-11-16T16:33:28.133' AS DateTime), N'GridOption')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (152, 71, N'toDate', N'Date', N'LTE', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'ec1dd965-5ea4-4b97-9c36-f0f1daea59b5', CAST(N'2020-11-16T16:33:28.137' AS DateTime), N'GridOption')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (153, 72, N'fromDate', N'Date', N'GTE', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'3d34114f-5a5d-47e3-8942-f3bfb6f7ec9c', CAST(N'2020-11-16T16:33:28.137' AS DateTime), N'GridOption')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (154, 72, N'toDate', N'Date', N'LTE', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'9bdab2ae-1342-4f82-ae52-aa93fe870ef2', CAST(N'2020-11-16T16:33:28.137' AS DateTime), N'GridOption')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (155, 73, N'fromDate', N'Date', N'GTE', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'd5d764db-f763-49d1-8906-6c08a3a3f313', CAST(N'2020-11-16T16:33:28.137' AS DateTime), N'GridOption')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (156, 73, N'toDate', N'Date', N'LTE', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'7c2356ce-ff9f-4e56-af0d-ace93101ebfc', CAST(N'2020-11-16T16:33:28.140' AS DateTime), N'GridOption')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (158, 76, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'725dc31d-ca0e-4684-9be2-51dad9f407dc', CAST(N'2020-11-16T16:33:28.140' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (159, 76, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'e0526a31-a2a7-4c29-a23f-38b05a131b75', CAST(N'2020-11-16T16:33:28.140' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (161, 77, N'fromDate', N'from', N'EQ', N'System.DateTime', N'TextBox', N'FromDate', NULL, NULL, NULL, N'FromDate', N'12acf28a-3fe3-4343-a0db-2e5eee95f3e5', CAST(N'2020-11-16T16:33:28.143' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (162, 77, N'toDate', N'to', N'EQ', N'System.DateTime', N'TextBox', N'ToDate', NULL, NULL, NULL, N'ToDate', N'b620feff-6706-4695-b70e-3b707722635a', CAST(N'2020-11-16T16:33:28.143' AS DateTime), N'QueryString')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (164, 82, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'9547da60-1477-4dd9-ba38-848d5d091290', CAST(N'2021-11-05T17:11:56.697' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (165, 83, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'578093cb-f49d-458b-ab97-ba9495e362f3', CAST(N'2021-11-05T17:11:56.703' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (166, 84, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'9f4b49d1-eb3e-4de7-b199-2f68b37bfb59', CAST(N'2021-11-05T17:11:56.703' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (167, 41, N'no', N'no', N'EQ', N'System.Int32', N'NumberBox', N'VoucherNo', NULL, NULL, NULL, N'VoucherNo', N'131b617b-8e0c-4f6c-9b1b-7e5f73b68f31', CAST(N'2021-11-15T11:20:12.253' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (168, 85, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'6e9fe990-7fd5-4b98-9666-d4d07c39df73', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'DatePicker')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (169, 85, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'2a5d45b0-ab27-4c8c-bbd6-ebcaf1bb3c35', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'DatePicker')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (170, 86, N'fromDate', N'from', N'EQ', N'System.DateTime', N'DatePicker', N'FromDate', NULL, NULL, NULL, N'FromDate', N'728995ab-f331-4c20-a2f8-3d406c2316e5', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'DatePicker')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (171, 86, N'toDate', N'to', N'EQ', N'System.DateTime', N'DatePicker', N'ToDate', NULL, NULL, NULL, N'ToDate', N'20b6f2c5-353b-4e76-a714-f974519cc9a1', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'DatePicker')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (172, 87, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'b34899da-b1ac-4775-9a8c-627c18defe97', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'NumberBox')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (173, 87, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'f45065b4-14c1-4afb-93d7-8d07eb2511b0', CAST(N'2021-11-13T14:52:37.147' AS DateTime), N'NumberBox')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (174, 88, N'fromNo', N'from', N'EQ', N'System.Int32', N'NumberBox', N'FromNo', NULL, NULL, NULL, N'FromNo', N'ae4a6e07-01a6-4103-a3fc-f6835247ca85', CAST(N'2021-11-13T14:52:37.150' AS DateTime), N'NumberBox')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (175, 88, N'toNo', N'to', N'EQ', N'System.Int32', N'NumberBox', N'ToNo', NULL, NULL, NULL, N'ToNo', N'c27542f8-d876-4224-97d9-4363589d345d', CAST(N'2021-11-13T14:52:37.150' AS DateTime), N'NumberBox')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (176, 21, N'companyId', N'companyId', N'EQ', N'System.Int32', N'NumberBox', N'CompanyId', NULL, NULL, NULL, N'CompanyId', N'17754561-aa81-41a2-afe6-dd423886f5c4', CAST(N'2021-11-15T11:49:21.773' AS DateTime), N'Route')
GO
INSERT [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DefaultValue], [MinValue], [MaxValue], [DescriptionKey], [rowguid], [ModifiedDate], [Source]) VALUES (177, 80, N'date', N'date', N'EQ', N'System.DateTime', N'DatePicker', N'Date', NULL, NULL, NULL, N'Date', N'7c984db0-8248-4bf9-908e-557a7677ddf3', CAST(N'2021-11-15T13:51:11.493' AS DateTime), N'QueryString')
GO
SET IDENTITY_INSERT [Reporting].[Parameter] OFF
GO

-- 1.2.1292
SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (89, 16, 1, 2, 2, 'Voucher-Summary-By-Date', N'reports/finance/vouchers/sum-by-date', 0, 1, 0, 0)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (90, 16, 1, 2, 2, 'Voucher-Summary-By-No', N'reports/finance/vouchers/sum-by-no', 0, 1, 0, 0)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (269, 1, 89, 'Accounting Voucher Summary - By Date', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (270, 2, 89, N'خلاصه اسناد حسابداری - بر اساس تاریخ', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (271, 1, 90, 'Accounting Voucher Summary - By Voucher No', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (272, 2, 90, N'خلاصه اسناد حسابداری - بر اساس شماره سند', NULL)
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

SET IDENTITY_INSERT [Reporting].[Parameter] ON
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (178, 89, 'fromDate', 'Date', 'GTE', 'System.DateTime', 'TextBox', 'FromDate', 'FromDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (179, 89, 'toDate', 'Date', 'LTE', 'System.DateTime', 'TextBox', 'ToDate', 'ToDate')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (180, 90, 'fromNo', 'No', 'GTE', 'System.Int32', 'TextBox', 'FromNo', 'FromNo')
INSERT INTO [Reporting].[Parameter] ([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])
    VALUES (181, 90, 'toNo', 'No', 'LTE', 'System.Int32', 'TextBox', 'ToNo', 'ToNo')
SET IDENTITY_INSERT [Reporting].[Parameter] OFF

UPDATE [Reporting].[Parameter]
SET Source = 'GridOptions'
WHERE Source = 'GridOption'

-- 1.2.1304
SET IDENTITY_INSERT [Metadata].[Column] ON

INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES (686, 37, 'BranchName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 5, NULL)

SET IDENTITY_INSERT [Metadata].[Column] OFF

DELETE Config.[UserSetting] WHERE ViewID = 37

-- 1.2.1314
Delete [Metadata].[ShortcutCommand]
Go

ALTER TABLE [Metadata].[ShortcutCommand]
ALTER COLUMN [Scope] varchar(1024) NULL
GO

SET IDENTITY_INSERT [Metadata].[ShortcutCommand] ON 

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (1, NULL, N'NewVoucherLine', N'VoucherLineComponent', N'Ctrl+O', N'sh_addVoucherLine')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (2, NULL, N'ExportToExcel', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+E', N'sh_exportToExcel')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (3, NULL, N'Print', N'AutoGeneratedGridComponent,AutoGridExplorerComponent,DetailComponent', N'Alt+P', N'sh_print')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (4, NULL, N'ReportSetting', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+S', N'sh_openReportSetting')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (5, NULL, N'AdvanceFilter', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+L', N'sh_openAdvanceFilter')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (6, NULL, N'NewRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+N', N'sh_openNewDialog')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (7, NULL, N'EditRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+M', N'sh_openEditDialog')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (8, NULL, N'DeleteRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Shift+D', N'sh_delete')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (9, NULL, N'ExecuteFilter', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+L', N'sh_executeFilter')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (10, NULL, N'NewVoucher', N'VoucherEditorComponent', N'Alt+Insert', N'sh_newVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (11, NULL, N'RemoveVoucher', N'VoucherEditorComponent', N'Alt+Delete', N'sh_removeVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (12, NULL, N'CheckVoucher', N'VoucherEditorComponent', N'Alt+C', N'sh_checkVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (13, NULL, N'UnCheckVoucher', N'VoucherEditorComponent', N'Alt+K', N'sh_unCheckVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (14, NULL, N'ReportManagement', N'NavMenuComponent,AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+Q', N'sh_openReportManager')

SET IDENTITY_INSERT [Metadata].[ShortcutCommand] OFF


-- 1.2.1319
Update Metadata.Command set HotKey = 'Ctrl+Shift+A' where CommandID = 3

Update Metadata.Command set HotKey = 'Ctrl+A' where CommandID = 4

Update Metadata.Command set HotKey = 'Ctrl+Backquote' where CommandID = 5

Update Metadata.Command set HotKey = 'Ctrl+E' where CommandID = 6

Update Metadata.Command set HotKey = 'Ctrl+J' where CommandID = 7

Update Metadata.Command set HotKey = 'Ctrl+D' where CommandID = 12

Update Metadata.Command set HotKey = 'Ctrl+Z' where CommandID = 21

Update Metadata.Command set HotKey = 'Ctrl+Q' where CommandID = 22

Update Metadata.Command set HotKey = 'Ctrl+Alt+C' where CommandID = 24

Update Metadata.Command set HotKey = 'Ctrl+B' where CommandID = 25

Update Metadata.Command set HotKey = 'Ctrl+Alt+F' where CommandID = 26

Update Metadata.Command set HotKey = 'Ctrl+Alt+H' where CommandID = 29

Update Metadata.Command set HotKey = 'Ctrl+Shift+G' where CommandID = 40

Update Metadata.Command set HotKey = 'Ctrl+Shift+S' where CommandID = 42

Update Metadata.Command set HotKey = 'Ctrl+Shift+I' where CommandID = 43

Update Metadata.Command set HotKey = 'Ctrl+Shift+B' where CommandID = 44

Update Metadata.Command set HotKey = 'Ctrl+Shift+P' where CommandID = 46

Update Metadata.Command set HotKey = 'Ctrl+Shift+V' where CommandID = 47

Update Metadata.Command set HotKey = 'Ctrl+Shift+D' where CommandID = 48

Update Metadata.Command set HotKey = 'Ctrl+Shift+L' where CommandID = 49

Update Metadata.Command set HotKey = 'Ctrl+Shift+K' where CommandID = 50


-- 1.2.1321

Update Metadata.Command set HotKey = 'Ctrl+Alt+R' where CommandID = 9

Update Metadata.Command set HotKey = 'Ctrl+Alt+U' where CommandID = 41

Update Metadata.Command set HotKey = 'Ctrl+Shift+O' where CommandID = 32

Update Metadata.Command set HotKey = 'Ctrl+Alt+W' where CommandID = 30

Update Metadata.Command set HotKey = 'Ctrl+K' where CommandID = 31

Update Metadata.Command set HotKey = 'Ctrl+Alt+V' where CommandID = 15

Update Metadata.Command set HotKey = 'Ctrl+Alt+V' where CommandID = 15

Update Metadata.Command set HotKey = 'Alt+Q' where CommandID = 38

-- 1.2.1322

Update [Metadata].[ShortcutCommand] Set Method = 'sh_addVoucherLine' Where ShortcutCommandID = 1

Update [Metadata].[ShortcutCommand] Set HotKey = 'Alt+C' Where ShortcutCommandID = 12

Update [Metadata].[ShortcutCommand] Set HotKey = 'Ctrl+Shift+R' Where ShortcutCommandID = 8

-- 1.2.1326

Update Metadata.Command set HotKey = 'Ctrl+Y' where CommandID = 10

Update Metadata.Command set HotKey = 'Ctrl+Alt+Y' where CommandID = 17

Update Metadata.Command set HotKey = 'Ctrl+Alt+U' where CommandID = 18

Update Metadata.Command set HotKey = 'Ctrl+Alt+I' where CommandID = 19

-- 1.2.1328
CREATE TABLE [Auth].[Session] (
    [SessionID]       INT              IDENTITY (1, 1) NOT NULL,
    [UserID]          INT              NOT NULL,
    [Device]          NVARCHAR(64)     NOT NULL,
    [Browser]         NVARCHAR(64)     NOT NULL,
    [Fingerprint]     NVARCHAR(128)    NOT NULL,
    [IPAddress]       NVARCHAR(16)     NULL,
    [SinceUtc]        DATETIME         NOT NULL,
    [LastActivityUtc] DATETIME         NOT NULL,
    [TimeZone]        NVARCHAR(32)     NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_Session_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Auth_Session_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_Session] PRIMARY KEY CLUSTERED ([SessionID] ASC)
    , CONSTRAINT [FK_Auth_Session_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User]([UserID])
)
GO

-- 1.2.1355
UPDATE [Metadata].[View]
SET FetchUrl = ''
WHERE ViewID IN(9,10)	-- FetchUrl DOES NOT apply to FiscalPeriod and Branch

CREATE TABLE [Metadata].[ValidRowPermission] (
    [RowPermissionID] INT              IDENTITY (1, 1) NOT NULL,
    [ViewID]          INT              NOT NULL,
    [AccessMode]      NVARCHAR(64)     NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_ValidRowPermission_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Metadata_ValidRowPermission_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_ValidRowPermission] PRIMARY KEY CLUSTERED ([RowPermissionID] ASC)
    , CONSTRAINT [FK_Metadata_ValidRowPermission_Metadata_View] FOREIGN KEY ([ViewID]) REFERENCES [Metadata].[View]([ViewID])
)
GO

SET IDENTITY_INSERT [Metadata].[ValidRowPermission] ON

INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (1, 1, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (2, 1, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (3, 1, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (4, 2, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (5, 2, 'Access_AllRecordsCreatedByUser')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (6, 2, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (7, 2, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (8, 2, 'Access_SpecificReferences')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (9, 2, 'Access_AllExceptSpecificReferences')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (10, 3, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (11, 3, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (12, 3, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (13, 3, 'Access_MaxMoneyValue')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (14, 6, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (15, 6, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (16, 6, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (17, 7, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (18, 7, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (19, 7, 'Access_AllExceptSpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (20, 8, 'Access_Default')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (21, 8, 'Access_SpecificRecords')
INSERT INTO [Metadata].[ValidRowPermission] ([RowPermissionID], [ViewID], [AccessMode]) VALUES (22, 8, 'Access_AllExceptSpecificRecords')

SET IDENTITY_INSERT [Metadata].[ValidRowPermission] OFF

-- 1.2.1357
UPDATE [Metadata].[Column]
SET AllowSorting = 0, AllowFiltering = 0
WHERE ViewID IN(27,28,29) AND Name = 'Balance'

-- 1.2.1362
USE [msdb]
GO

BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [Database Maintenance]    Script Date: 2022-04-20 11:46:47 AM ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Maintenance' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Maintenance'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'CleanUp_CloseExpiredSessions', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'This job periodically closes all application sessions that have not been active for a configured time span.', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=N'NgTadbirUser', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [MainTask]    Script Date: 2022-04-20 11:46:47 AM ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'MainTask', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'DELETE FROM [Auth].[Session] WHERE DATEDIFF(hour, [SinceUtc], GETUTCDATE()) >= 336', 
		@database_name=N'NGTadbirSys', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'ModerateFrequency', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=4, 
		@freq_subday_interval=30, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20220420, 
		@active_end_date=99991231, 
		@active_start_time=0, 
		@active_end_time=235959, 
		@schedule_uid=N'5b922ed4-da80-4a1e-a3f1-188876f205bf'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
GO

USE [NGTadbirSys]
GO

-- 1.2.1365
ALTER TABLE [Core].[SystemError]
ALTER COLUMN [Message] varchar(2048) NOT NULL
GO

-- 1.2.1386
ALTER TABLE [Metadata].[View]
ALTER COLUMN [FetchUrl] NVARCHAR(512) NULL
GO

UPDATE [Metadata].[View]
SET [FetchUrl] = NULL
WHERE [FetchUrl] = ''
GO
