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
