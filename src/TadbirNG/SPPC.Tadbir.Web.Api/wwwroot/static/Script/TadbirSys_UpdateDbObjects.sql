﻿USE [NGTadbirSys]
GO

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

-- 1.2.1437
SET IDENTITY_INSERT [Metadata].[View] ON
INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])
    VALUES (68, 'Widget', 'Widget', 0, 0, 'Core', NULL, NULL)
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (687, 68, 'Id', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (688, 68, 'TypeId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (689, 68, 'FunctionId', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (690, 68, 'CreatedById', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 1, 1, N'AlwaysHidden', -1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (691, 68, 'RowNo', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (692, 68, 'Title', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, N'AlwaysVisible', 1)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (693, 68, 'TypeName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 2)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (694, 68, 'FunctionName', NULL, NULL, 'System.String', 'nvarchar', 'string', 64, 0, 0, 0, 1, 1, NULL, 3)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (695, 68, 'CreatedByFullName', NULL, NULL, 'System.String', 'nvarchar', 'string', 128, 0, 0, 0, 1, 1, NULL, 4)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex])
    VALUES (696, 68, 'Description', NULL, NULL, 'System.String', 'nvarchar', 'string', 512, 0, 0, 0, 1, 1, NULL, 5)
SET IDENTITY_INSERT [Metadata].[Column] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (36, N'Dashboard', N'Dashboard')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (210, 36, N'ManageDashboard', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (211, 36, N'ManageWidgets', 2)
SET IDENTITY_INSERT [Auth].[Permission] OFF

-- 1.2.1451
SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey)
    VALUES (51, 37, 210, N'ManageDashboard', N'/tadbir/dashboard', N'list', NULL)
SET IDENTITY_INSERT [Metadata].[Command] OFF

-- 1.2.1454
UPDATE [Metadata].[Column]
SET [IsNullable] = 1
WHERE [ViewID] = 68 AND [Name] = 'Description'

-- 1.2.1455
UPDATE [Metadata].[View]
SET [EntityName] = 'Dashboard'
WHERE [Name] = 'Widget'

UPDATE [Metadata].[Column]
SET [AllowSorting] = 0, [AllowFiltering] = 0
WHERE [ViewID] = 68 AND [Name] = 'RowNo'

SET IDENTITY_INSERT [Reporting].[Report] ON
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (91, 7, 1, 68, 1, '', N'dashboard/widgets', 0, 1, 0, 1)
INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])
    VALUES (92, 7, 1, 68, 1, '', N'dashboard/widgets/all', 0, 1, 0, 1)
SET IDENTITY_INSERT [Reporting].[Report] OFF

SET IDENTITY_INSERT [Reporting].[LocalReport] ON
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (273, 1, 91, 'My Widgets', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (274, 2, 91, N'ویجت های من', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (275, 1, 92, 'All Widgets', NULL)
INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])
    VALUES (276, 2, 92, N'همه ویجت ها', NULL)
SET IDENTITY_INSERT [Reporting].[LocalReport] OFF

-- 1.2.1460
UPDATE [Metadata].[Column]
SET [IsNullable] = 1
WHERE [ViewID] = 2 AND [Name] IN('OriginName', 'TypeName')


-- 1.2.1480
UPDATE [Reporting].[LocalReport] SET [Caption] = N'Widgets list' WHERE ReportID = 91 AND LocaleID = 1
UPDATE [Reporting].[LocalReport] SET [Caption] = N'لیست ویجت ها' WHERE ReportID = 91 AND LocaleID = 2
UPDATE [Reporting].[LocalReport] SET [Caption] = N'Widgets list' WHERE ReportID = 92 AND LocaleID = 1
UPDATE [Reporting].[LocalReport] SET [Caption] = N'لیست ویجت ها' WHERE ReportID = 92 AND LocaleID = 2

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
    VALUES (703, 69, 'CheckID', NULL, NULL, 'System.Int32', 'int', 'number', 0, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
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
Set [Name] = N'IsArchivedName', [DotNetType] = N'System.String', [StorageType] = N'nvarchar'
	, [ScriptType] = N'string', [IsNullable] = 1, [AllowFiltering] = 1, [Visibility] = NULL
Where ColumnID = 737

-- 1.2.1504
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

