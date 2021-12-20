USE NGTadbirSys
GO

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

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (1, NULL, N'NewVoucherLine', N'VoucherLineComponent', N'Ctrl+O', N'sh_addNew')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (2, NULL, N'ExportToExcel', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+E', N'sh_exportToExcel')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (3, NULL, N'Print', N'AutoGeneratedGridComponent,AutoGridExplorerComponent,DetailComponent', N'Alt+P', N'sh_print')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (4, NULL, N'ReportSetting', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+S', N'sh_openReportSetting')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (5, NULL, N'AdvanceFilter', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Alt+L', N'sh_openAdvanceFilter')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (6, NULL, N'NewRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+N', N'sh_openNewDialog')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (7, NULL, N'EditRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+M', N'sh_openEditDialog')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (8, NULL, N'DeleteRecord', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+D', N'sh_delete')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (9, NULL, N'ExecuteFilter', N'AutoGeneratedGridComponent,AutoGridExplorerComponent', N'Ctrl+Alt+L', N'sh_executeFilter')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (10, NULL, N'NewVoucher', N'VoucherEditorComponent', N'Alt+Insert', N'sh_newVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (11, NULL, N'RemoveVoucher', N'VoucherEditorComponent', N'Alt+Delete', N'sh_removeVoucher')

INSERT [Metadata].[ShortcutCommand] ([ShortcutCommandID], [PermissionID], [Name], [Scope], [HotKey], [Method]) VALUES (12, NULL, N'CheckVoucher', N'VoucherEditorComponent', N'Alt+S', N'sh_checkVoucher')

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


--1.2.1321

Update Metadata.Command set HotKey = 'Ctrl+Alt+R' where CommandID = 9

Update Metadata.Command set HotKey = 'Ctrl+Alt+U' where CommandID = 41

Update Metadata.Command set HotKey = 'Ctrl+Shift+O' where CommandID = 32

Update Metadata.Command set HotKey = 'Ctrl+Alt+W' where CommandID = 30

Update Metadata.Command set HotKey = 'Ctrl+K' where CommandID = 31

Update Metadata.Command set HotKey = 'Ctrl+Alt+V' where CommandID = 15

Update Metadata.Command set HotKey = 'Ctrl+Alt+V' where CommandID = 15

Update Metadata.Command set HotKey = 'Alt+Q' where CommandID = 38








