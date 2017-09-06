USE [TadbirDemo]
GO

/****** Object: Table [dbo].[User] Script Date: 2017-02-15 2:44:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA [Core]
GO

CREATE SCHEMA [Auth]
GO

CREATE SCHEMA [Contact]
GO

CREATE SCHEMA [Corporate]
GO

CREATE SCHEMA [Finance]
GO

CREATE SCHEMA [Workflow]
GO

CREATE SCHEMA [WFTracking]
GO

CREATE SCHEMA [Inventory]
GO

CREATE SCHEMA [Procurement]
GO

CREATE SCHEMA [Warehousing]
GO

CREATE SCHEMA [Sales]
GO

CREATE TABLE [Finance].[Currency] (
    [CurrencyID]     INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Currency_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_Currency_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Currency] PRIMARY KEY CLUSTERED ([CurrencyID] ASC)
)
GO

CREATE TABLE [Auth].[User] (
    [UserID]         INT              IDENTITY (1, 1) NOT NULL,
    [UserName]       NVARCHAR(64)     NOT NULL,
    [PasswordHash]   VARCHAR(256)     NOT NULL,
    [LastLoginDate]  DATETIME         NULL,
    [IsEnabled]      BIT              NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_User_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_User_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
)
GO

CREATE TABLE [Auth].[Role] (
    [RoleID]         INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_Role_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_Role_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_Role] PRIMARY KEY CLUSTERED ([RoleID] ASC)
)
GO

CREATE TABLE [Auth].[UserRole] (
    [UserRoleID]     INT              IDENTITY (1, 1) NOT NULL,
    [UserID]         INT              NOT NULL,
    [RoleID]         INT              NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_UserRole_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_UserRole_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_UserRole] PRIMARY KEY CLUSTERED ([UserRoleID] ASC)
    , CONSTRAINT [FK_Auth_UserRole_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User] ([UserID])
    , CONSTRAINT [FK_Auth_UserRole_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role] ([RoleID])
)
GO

CREATE TABLE [Auth].[PermissionGroup] (
    [PermissionGroupID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR(64)     NOT NULL,
    [EntityName]          NVARCHAR(64)     NULL,
    [Description]         NVARCHAR(512)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_PermissionGroup_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Auth_PermissionGroup_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_PermissionGroup] PRIMARY KEY CLUSTERED ([PermissionGroupID] ASC)
)
GO

CREATE TABLE [Auth].[Permission] (
    [PermissionID]   INT              IDENTITY (1, 1) NOT NULL,
	[GroupID]        INT              NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Flag]           INT              CONSTRAINT [DF_Auth_Permission_Flag] DEFAULT (0) NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_Permission_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_Permission_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_Permission] PRIMARY KEY CLUSTERED ([PermissionID] ASC)
    , CONSTRAINT [FK_Auth_Permission_Auth_PermissionGroup] FOREIGN KEY ([GroupID]) REFERENCES [Auth].[PermissionGroup] ([PermissionGroupID])
)
GO

CREATE TABLE [Auth].[RolePermission] (
    [RolePermissionID]   INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]             INT              NOT NULL,
    [PermissionID]       INT              NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RolePermission_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Auth_RolePermission_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RolePermission] PRIMARY KEY CLUSTERED ([RolePermissionID] ASC)
    , CONSTRAINT [FK_Auth_RolePermission_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role] ([RoleID])
    , CONSTRAINT [FK_Auth_RolePermission_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission] ([PermissionID])
)
GO

CREATE TABLE [Core].[DocumentType] (
    [TypeID]                INT              IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR(64)     NOT NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Core_DocumentType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Core_DocumentType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_DocumentType] PRIMARY KEY CLUSTERED ([TypeID] ASC)
)
GO

CREATE TABLE [Core].[DocumentStatus] (
    [StatusID]              INT              IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR(64)     NOT NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Core_DocumentStatus_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Core_DocumentStatus_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_DocumentStatus] PRIMARY KEY CLUSTERED ([StatusID] ASC)
)
GO

CREATE TABLE [Core].[Document] (
    [DocumentID]          INT              IDENTITY (1, 1) NOT NULL,
    [TypeID]              INT              NOT NULL,
    [StatusID]            INT              NOT NULL,
    [No]                  NVARCHAR(64)     NOT NULL,
    [OperationalStatus]   NVARCHAR(64)     NOT NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Core_Document_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Core_Document_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_Document] PRIMARY KEY CLUSTERED ([DocumentID] ASC)
    , CONSTRAINT [FK_Core_Document_Core_DocumentType] FOREIGN KEY ([TypeID]) REFERENCES [Core].[DocumentType]([TypeID])
    , CONSTRAINT [FK_Core_Document_Core_DocumentStatus] FOREIGN KEY ([StatusID]) REFERENCES [Core].[DocumentStatus]([StatusID])
)
GO

CREATE TABLE [Core].[DocumentAction] (
    [ActionID]           INT              IDENTITY (1, 1) NOT NULL,
	[DocumentID]         INT              NOT NULL,
	[LineID]             INT              NULL,
    [CreatedByID]        INT              NOT NULL,
    [ModifiedByID]       INT              NOT NULL,
    [ConfirmedByID]      INT              NULL,
    [ApprovedByID]       INT              NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Core_DocumentAction_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [ConfirmedDate]      DATETIME         NULL,
    [ApprovedDate]       DATETIME         NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Core_DocumentAction_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL
    , CONSTRAINT [PK_Core_DocumentAction] PRIMARY KEY CLUSTERED ([ActionID] ASC)
    , CONSTRAINT [FK_Core_DocumentAction_Auth_CreatedBy] FOREIGN KEY ([CreatedByID]) REFERENCES [Auth].[User]([UserID])
    , CONSTRAINT [FK_Core_DocumentAction_Auth_ModifiedBy] FOREIGN KEY ([ModifiedByID]) REFERENCES [Auth].[User]([UserID])
    , CONSTRAINT [FK_Core_DocumentAction_Auth_ConfirmedBy] FOREIGN KEY ([ConfirmedByID]) REFERENCES [Auth].[User]([UserID])
    , CONSTRAINT [FK_Core_DocumentAction_Auth_ApprovedBy] FOREIGN KEY ([ApprovedByID]) REFERENCES [Auth].[User]([UserID])
    , CONSTRAINT [FK_Core_DocumentAction_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document]([DocumentID])
)
GO

CREATE TABLE [Contact].[Person] (
    [PersonID]       INT              IDENTITY (1, 1) NOT NULL,
	[UserID]         INT              NOT NULL,
    [FirstName]      NVARCHAR(64)     NOT NULL,
    [LastName]       NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Contact_Person_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Contact_Person_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Contact_Person] PRIMARY KEY CLUSTERED ([PersonID] ASC)
    , CONSTRAINT [FK_Contact_Person_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User] ([UserID])
)
GO

CREATE TABLE [Corporate].[Company] (
    [CompanyID]      INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Corporate_Company_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Corporate_Company_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Corporate_Company] PRIMARY KEY CLUSTERED ([CompanyID] ASC)
)
GO

CREATE TABLE [Corporate].[Branch] (
    [BranchID]       INT              IDENTITY (1, 1) NOT NULL,
	[CompanyID]      INT              NOT NULL,
	[ParentID]       INT              NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
	[Level]          INT              CONSTRAINT [DF_Corporate_Branch_Level] DEFAULT (0) NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Corporate_Branch_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Corporate_Branch_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Corporate_Branch] PRIMARY KEY CLUSTERED ([BranchID] ASC)
    , CONSTRAINT [FK_Corporate_Branch_Corporate_Company] FOREIGN KEY ([CompanyID]) REFERENCES [Corporate].[Company] ([CompanyID])
)
GO

CREATE TABLE [Auth].[RoleBranch] (
    [RoleBranchID]       INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]             INT              NOT NULL,
    [BranchID]           INT              NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RoleBranch_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Auth_RoleBranch_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RoleBranch] PRIMARY KEY CLUSTERED ([RoleBranchID] ASC)
    , CONSTRAINT [FK_Auth_RoleBranch_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role] ([RoleID])
    , CONSTRAINT [FK_Auth_RoleBranch_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
)
GO

CREATE TABLE [Finance].[FiscalPeriod] (
    [FiscalPeriodID]   INT              IDENTITY (1, 1) NOT NULL,
	[CompanyID]        INT              NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [StartDate]        DATETIME         NOT NULL,
    [EndDate]          DATETIME         NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_FiscalPeriod_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_FiscalPeriod_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_FiscalPeriod] PRIMARY KEY CLUSTERED ([FiscalPeriodID] ASC)
    , CONSTRAINT [FK_Finance_FiscalPeriod_Corporate_Company] FOREIGN KEY ([CompanyID]) REFERENCES [Corporate].[Company] ([CompanyID])
)
GO

CREATE TABLE [Finance].[Account] (
    [AccountID]      INT              IDENTITY (1, 1) NOT NULL,
	[FiscalPeriodID] INT              NOT NULL,
	[BranchID]       INT              NOT NULL,
    [Code]           NVARCHAR(512)    NOT NULL,
    [Name]           NVARCHAR(512)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Account_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_Account_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Account] PRIMARY KEY CLUSTERED ([AccountID] ASC)
    , CONSTRAINT [FK_Finance_Account_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Account_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
)
GO

CREATE TABLE [Finance].[Transaction] (
    [TransactionID]     INT              IDENTITY (1, 1) NOT NULL,
	[FiscalPeriodID]    INT              NOT NULL,
	[BranchID]          INT              NOT NULL,
	[DocumentID]        INT              NOT NULL,
    [No]                NVARCHAR(64)     NOT NULL,
    [Date]              DATETIME         NOT NULL,
    [Description]       NVARCHAR(512)    NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Transaction_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Finance_Transaction_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Transaction] PRIMARY KEY CLUSTERED ([TransactionID] ASC)
    , CONSTRAINT [FK_Finance_Transaction_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Transaction_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
    , CONSTRAINT [FK_Finance_Transaction_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document] ([DocumentID])
)
GO

CREATE TABLE [Finance].[TransactionLine] (
    [LineID]          INT              IDENTITY (1, 1) NOT NULL,
	[TransactionID]   INT              NOT NULL,
	[FiscalPeriodID]  INT              NOT NULL,
	[BranchID]        INT              NOT NULL,
	[AccountID]       INT              NOT NULL,
	[CurrencyID]      INT              NOT NULL,
    [Description]     NVARCHAR(512)    NULL,
    [Debit]           MONEY            NOT NULL,
    [Credit]          MONEY            NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_TransactionLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_TransactionLine_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_TransactionLine] PRIMARY KEY CLUSTERED ([LineID] ASC)
    , CONSTRAINT [FK_Finance_TransactionLine_Finance_Transaction] FOREIGN KEY ([TransactionID]) REFERENCES [Finance].[Transaction] ([TransactionID])
    , CONSTRAINT [FK_Finance_TransactionLine_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_TransactionLine_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
    , CONSTRAINT [FK_Finance_TransactionLine_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
    , CONSTRAINT [FK_Finance_TransactionLine_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency] ([CurrencyID])
)
GO

CREATE TABLE [Workflow].[WorkItem] (
    [WorkItemID]     INT              IDENTITY (1, 1) NOT NULL,
	[CreatedByID]    INT              NOT NULL,
	[TargetID]       INT              NULL,
    [Number]         NVARCHAR(16)     NOT NULL,
    [Date]           DATETIME         NOT NULL,
    [Time]           TIME(7)          NOT NULL,
    [Title]          NVARCHAR(128)    NOT NULL,
    [DocumentType]   VARCHAR(128)     NOT NULL,
    [Action]         VARCHAR(64)      NOT NULL,
    [Remarks]        NVARCHAR(1024)   NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Workflow_WorkItem_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Workflow_WorkItem_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_WorkItem] PRIMARY KEY CLUSTERED ([WorkItemID] ASC)
    , CONSTRAINT [FK_Workflow_WorkItem_Auth_User] FOREIGN KEY ([CreatedByID]) REFERENCES [Auth].[User] ([UserID])
    , CONSTRAINT [FK_Workflow_WorkItem_Auth_Role] FOREIGN KEY ([TargetID]) REFERENCES [Auth].[Role] ([RoleID])
)
GO

CREATE TABLE [Workflow].[WorkItemDocument] (
    [DocumentItemID]   INT              IDENTITY (1, 1) NOT NULL,
    [WorkItemID]       INT              NULL,
    [EntityID]         INT              NOT NULL,
    [DocumentID]       INT              NOT NULL,
    [DocumentType]     VARCHAR(128)     NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Workflow_WorkItemDocument_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Workflow_WorkItemDocument_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_WorkItemDocument] PRIMARY KEY CLUSTERED ([DocumentItemID] ASC)
    , CONSTRAINT [FK_Workflow_WorkItemDocument_Workflow_WorkItem] FOREIGN KEY ([WorkItemID]) REFERENCES [Workflow].[WorkItem] ([WorkItemID])
    , CONSTRAINT [FK_Workflow_WorkItemDocument_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document] ([DocumentID])
)
GO

CREATE TABLE [Workflow].[WorkItemHistory] (
    [HistoryItemID]       INT              IDENTITY (1, 1) NOT NULL,
	[UserID]              INT              NOT NULL,
	[RoleID]              INT              NOT NULL,
    [DocumentID]          INT              NOT NULL,
    [EntityID]            INT              NOT NULL,
    [Number]              NVARCHAR(16)     NOT NULL,
    [Date]                DATETIME         NOT NULL,
    [Time]                TIME(7)          NOT NULL,
    [Title]               NVARCHAR(128)    NOT NULL,
    [Action]              VARCHAR(64)      NOT NULL,
    [Remarks]             NVARCHAR(1024)   NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Workflow_WorkItemHistory_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Workflow_WorkItemHistory_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_WorkItemHistory] PRIMARY KEY CLUSTERED ([HistoryItemID] ASC)
    , CONSTRAINT [FK_Workflow_WorkItemHistory_Auth_User] FOREIGN KEY ([UserID]) REFERENCES [Auth].[User] ([UserID])
    , CONSTRAINT [FK_Workflow_WorkItemHistory_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role] ([RoleID])
    , CONSTRAINT [FK_Workflow_WorkItemHistory_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document] ([DocumentID])
)
GO

CREATE TABLE [WFTracking].[WorkflowInstanceEvent] (
	[EventID]                    INT IDENTITY(1,1) NOT NULL,
	[WorkflowInstanceId]         UNIQUEIDENTIFIER NOT NULL,
	[ActivityDefinition]         NVARCHAR(256) NULL,
	[RecordNumber]               BIGINT NOT NULL,
	[State]                      NVARCHAR(128) NULL,
	[TraceLevelId]               TINYINT NULL,
	[Reason]                     NVARCHAR(2048) NULL,
	[ExceptionDetails]           NVARCHAR(MAX) NULL,
	[SerializedAnnotations]      NVARCHAR(MAX) NULL,
	[TimeCreated]                DATETIME NOT NULL,
    [rowguid]                    UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_WorkflowInstanceEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]               DATETIME CONSTRAINT [DF_WFTracking_WorkflowInstanceEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_WFTracking_WorkflowInstanceEvent] PRIMARY KEY ([EventID]),
);
GO

CREATE TABLE [WFTracking].[ActivityInstanceEvent] (
	[EventID]               INT IDENTITY(1,1) NOT NULL,
	[WorkflowInstanceId]    UNIQUEIDENTIFIER NOT NULL,
	[RecordNumber]          BIGINT NOT NULL,
	[State]                 NVARCHAR(128) NULL,
	[TraceLevelId]          TINYINT NULL,
	[ActivityRecordType]    NVARCHAR(128) NOT NULL,
	[ActivityName]          NVARCHAR(1024) NULL,
	[ActivityId]            NVARCHAR(256) NULL,
	[ActivityInstanceId]    NVARCHAR(256) NULL,
	[ActivityType]          NVARCHAR(2048) NULL,
	[SerializedArguments]   NVARCHAR(MAX) NULL,
	[SerializedVariables]   NVARCHAR(MAX) NULL,
    [SerializedAnnotations] NVARCHAR(MAX) NULL,
	[TimeCreated]           DATETIME NOT NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_ActivityInstanceEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME CONSTRAINT [DF_WFTracking_ActivityInstanceEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_WFTracking_ActivityInstanceEvent] PRIMARY KEY ([EventID]),
);
GO

CREATE TABLE [WFTracking].[ExtendedActivityEvent] (
	[EventID]                        INT IDENTITY(1,1) NOT NULL,
	[WorkflowInstanceId]             UNIQUEIDENTIFIER NOT NULL,
	[RecordNumber]                   BIGINT NULL,
	[TraceLevelId]                   TINYINT NULL,
	[ActivityRecordType]             NVARCHAR(128) NOT NULL,
	[ActivityName]                   NVARCHAR(1024) NULL,
	[ActivityId]                     NVARCHAR(256) NULL,
	[ActivityInstanceId]             NVARCHAR(256) NULL,
	[ActivityType]                   NVARCHAR(2048) NULL,
	[ChildActivityName]              NVARCHAR(1024) NULL,
	[ChildActivityId]                NVARCHAR(256) NULL,
	[ChildActivityInstanceId]        NVARCHAR(256) NULL,
	[ChildActivityType]              NVARCHAR(2048) NULL,
	[FaultDetails]	                 NVARCHAR(MAX) NULL,
	[FaultHandlerActivityName]       NVARCHAR(1024) NULL,
	[FaultHandlerActivityId]         NVARCHAR(256) NULL,
	[FaultHandlerActivityInstanceId] NVARCHAR(256) NULL,
	[FaultHandlerActivityType]       NVARCHAR(2048) NULL,
	[SerializedAnnotations]          NVARCHAR(MAX) NULL,
	[TimeCreated]                    DATETIME NOT NULL,
    [rowguid]                        UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_ExtendedActivityEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]                   DATETIME CONSTRAINT [DF_WFTracking_ExtendedActivityEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_WFTracking_ExtendedActivityEvent] PRIMARY KEY ([EventID]),
);
GO

CREATE TABLE [WFTracking].[BookmarkResumptionEvent] ( 
    [EventID]                 INT IDENTITY(1,1) NOT NULL,
    [WorkflowInstanceId]      UNIQUEIDENTIFIER NOT NULL,
    [RecordNumber]            BIGINT NULL,
    [TraceLevelId]            TINYINT NULL,
    [BookmarkName]            NVARCHAR(1024) NULL,
    [BookmarkScope]           UNIQUEIDENTIFIER NULL,
    [OwnerActivityName]       NVARCHAR(1024) NULL,
    [OwnerActivityId]         NVARCHAR(256) NULL,
    [OwnerActivityInstanceId] NVARCHAR(256) NULL,
    [OwnerActivityType]       NVARCHAR(2048) NULL,
    [SerializedAnnotations]   NVARCHAR(MAX) NULL,
    [TimeCreated]             DATETIME NOT NULL,
    [rowguid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_BookmarkResumptionEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]            DATETIME CONSTRAINT [DF_WFTracking_BookmarkResumptionEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_WFTracking_BookmarkResumptionEvent] PRIMARY KEY ([EventID])
);
GO

CREATE TABLE [WFTracking].[CustomTrackingEvent] (
	[EventID]               INT IDENTITY(1,1) NOT NULL,
	[WorkflowInstanceId]    UNIQUEIDENTIFIER NOT NULL,
	[RecordNumber]          BIGINT NULL,
	[TraceLevelId]          TINYINT NULL,
	[CustomRecordName]      NVARCHAR(2048) NULL,
	[ActivityName]          NVARCHAR(1024) NULL,
	[ActivityId]            NVARCHAR(256) NULL,
	[ActivityInstanceId]    NVARCHAR(256) NULL,
	[ActivityType]          NVARCHAR(2048) NULL,
	[SerializedData]        NVARCHAR(MAX) NULL,
	[SerializedAnnotations] NVARCHAR(MAX) NULL,
	[TimeCreated]           DATETIME NOT NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_WFTracking_CustomTrackingEvent_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME CONSTRAINT [DF_WFTracking_CustomTrackingEvent_ModifiedDate] DEFAULT (getdate()) NOT NULL,
	CONSTRAINT [PK_WFTracking_CustomTrackingEvent] PRIMARY KEY ([EventID]),
);
GO

CREATE TABLE [Contact].[BusinessPartner] (
    [PartnerID]           INT              IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR(128)    NOT NULL,
    [Phone]               NVARCHAR(64)     NULL,
    [Email]               NVARCHAR(64)     NULL,
    [CommerceCode]        NVARCHAR(64)     NULL,
    [Address]             NVARCHAR(256)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Contact_BusinessPartner_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Contact_BusinessPartner_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Contact_BusinessPartner] PRIMARY KEY CLUSTERED ([PartnerID] ASC)
)
GO

CREATE TABLE [Contact].[Customer] (
    [CustomerID]     INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Phone]          NVARCHAR(64)     NULL,
    [Email]          NVARCHAR(64)     NULL,
    [CommerceCode]   NVARCHAR(64)     NULL,
    [Address]        NVARCHAR(256)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Contact_Customer_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Contact_Customer_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Contact_Customer] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
)
GO

CREATE TABLE [Core].[ServiceJob] (
    [JobID]                INT              IDENTITY (1, 1) NOT NULL,
    [rowguid]              UNIQUEIDENTIFIER CONSTRAINT [DF_Core_ServiceJob_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_Core_ServiceJob_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_ServiceJob] PRIMARY KEY CLUSTERED ([JobID] ASC)
)
GO

CREATE TABLE [Corporate].[BusinessUnit] (
    [UnitID]           INT              IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [Description]      NVARCHAR(256)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Corporate_BusinessUnit_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Corporate_BusinessUnit_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Corporate_BusinessUnit] PRIMARY KEY CLUSTERED ([UnitID] ASC)
)
GO

CREATE TABLE [Finance].[DetailAccount] (
    [DetailID]          INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]          INT              NULL,
    [Code]              NVARCHAR(16)     NOT NULL,
    [FullCode]          NVARCHAR(256)    NOT NULL,
    [Name]              NVARCHAR(256)    NOT NULL,
    [Level]             SMALLINT         CONSTRAINT [DF_Finance_DetailAccount_Level] DEFAULT (0) NOT NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_DetailAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Finance_DetailAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_DetailAccount] PRIMARY KEY CLUSTERED ([DetailID] ASC)
    , CONSTRAINT [FK_Finance_DetailAccount_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[DetailAccount]([DetailID])
)
GO

CREATE TABLE [Finance].[CostCenter] (
    [CostCenterID]   INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]       INT              NULL,
    [Code]           NVARCHAR(16)     NOT NULL,
    [FullCode]       NVARCHAR(256)    NOT NULL,
    [Name]           NVARCHAR(256)    NOT NULL,
    [Level]          SMALLINT         CONSTRAINT [DF_Finance_CostCenter_Level] DEFAULT (0) NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_CostCenter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_CostCenter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_CostCenter] PRIMARY KEY CLUSTERED ([CostCenterID] ASC)
    , CONSTRAINT [FK_Finance_CostCenter_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
)
GO

CREATE TABLE [Finance].[Project] (
    [ProjectID]      INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]       INT              NULL,
    [Code]           NVARCHAR(16)     NOT NULL,
    [FullCode]       NVARCHAR(256)    NOT NULL,
    [Name]           NVARCHAR(256)    NOT NULL,
    [Level]          SMALLINT         CONSTRAINT [DF_Finance_Project_Level] DEFAULT (0) NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Project_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_Project_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Project] PRIMARY KEY CLUSTERED ([ProjectID] ASC)
    , CONSTRAINT [FK_Finance_Project_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[Project]([ProjectID])
)
GO

CREATE TABLE [Finance].[FullAccount] (
    [FullAccountID]   INT              IDENTITY (1, 1) NOT NULL,
    [AccountID]       INT              NOT NULL,
    [DetailID]        INT              NULL,
    [CostCenterID]    INT              NULL,
    [ProjectID]       INT              NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_FullAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_FullAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_FullAccount] PRIMARY KEY CLUSTERED ([FullAccountID] ASC)
    , CONSTRAINT [FK_Finance_FullAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Finance_FullAccount_Finance_Detail] FOREIGN KEY ([DetailID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_FullAccount_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Finance_FullAccount_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
)
GO

CREATE TABLE [Finance].[FullDetailType] (
    [TypeID]         INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [Description]    NVARCHAR(256)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_FullDetailType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_FullDetailType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_FullDetailType] PRIMARY KEY CLUSTERED ([TypeID] ASC)
)
GO

CREATE TABLE [Finance].[FullDetail] (
    [FullDetailID]   INT              IDENTITY (1, 1) NOT NULL,
    [TypeID]         INT              NULL,
    [Detail2ID]      INT              NOT NULL,
    [Detail3ID]      INT              NULL,
    [Detail4ID]      INT              NULL,
    [Detail5ID]      INT              NULL,
    [Detail6ID]      INT              NULL,
    [Detail7ID]      INT              NULL,
    [Detail8ID]      INT              NULL,
    [Detail9ID]      INT              NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_FullDetail_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_FullDetail_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_FullDetail] PRIMARY KEY CLUSTERED ([FullDetailID] ASC)
    , CONSTRAINT [FK_Finance_FullDetail_Finance_FullDetailType] FOREIGN KEY ([TypeID]) REFERENCES [Finance].[FullDetailType]([TypeID])
    , CONSTRAINT [FK_Finance_FullDetail_Finance_Detail2] FOREIGN KEY ([Detail2ID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_FullDetail_Finance_Detail3] FOREIGN KEY ([Detail3ID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_FullDetail_Finance_Detail4] FOREIGN KEY ([Detail4ID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_FullDetail_Finance_Detail5] FOREIGN KEY ([Detail5ID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_FullDetail_Finance_Detail6] FOREIGN KEY ([Detail6ID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_FullDetail_Finance_Detail7] FOREIGN KEY ([Detail7ID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_FullDetail_Finance_Detail8] FOREIGN KEY ([Detail8ID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_FullDetail_Finance_Detail9] FOREIGN KEY ([Detail9ID]) REFERENCES [Finance].[DetailAccount]([DetailID])
)
GO

CREATE TABLE [Inventory].[UOM] (
    [UomID]         INT              IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR(64)     NOT NULL,
    [rowguid]       UNIQUEIDENTIFIER CONSTRAINT [DF_Inventory_UOM_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_Inventory_UOM_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Inventory_UOM] PRIMARY KEY CLUSTERED ([UomID] ASC)
)
GO

CREATE TABLE [Inventory].[ProductCategory] (
    [CategoryID]   INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]     INT              NULL,
    [Code]         NVARCHAR(16)     NOT NULL,
    [FullCode]     NVARCHAR(256)    NOT NULL,
    [Name]         NVARCHAR(64)     NOT NULL,
    [Level]        SMALLINT         CONSTRAINT [DF_Inventory_ProductCategory_Level] DEFAULT (0) NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Inventory_ProductCategory_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Inventory_ProductCategory_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Inventory_ProductCategory] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
    , CONSTRAINT [FK_Inventory_ProductCategory_Inventory_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Inventory].[ProductCategory]([CategoryID])
)
GO

CREATE TABLE [Inventory].[Product] (
    [ProductID]      INT              IDENTITY (1, 1) NOT NULL,
	[CategoryID]     INT              NOT NULL,
    [Code]           NVARCHAR(64)     NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Inventory_Product_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Inventory_Product_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Inventory_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC)
	, CONSTRAINT [FK_Inventory_Product_Inventory_ProductCategory] FOREIGN KEY([CategoryID]) REFERENCES [Inventory].[ProductCategory]([CategoryID])
)
GO

CREATE TABLE [Inventory].[Warehouse] (
    [WarehouseID]    INT              IDENTITY (1, 1) NOT NULL,
    [Code]           NVARCHAR(64)     NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Inventory_Warehouse_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Inventory_Warehouse_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Inventory_Warehouse] PRIMARY KEY CLUSTERED ([WarehouseID] ASC)
)
GO

CREATE TABLE [Inventory].[ProductInventory] (
    [ProductInventoryID]   INT              IDENTITY (1, 1) NOT NULL,
    [ProductID]            INT              NOT NULL,
    [WarehouseID]          INT              NOT NULL,
    [FiscalPeriodID]       INT              NOT NULL,
    [BranchID]             INT              NOT NULL,
    [Quantity]             FLOAT            NOT NULL,
    [rowguid]              UNIQUEIDENTIFIER CONSTRAINT [DF_Inventory_ProductInventory_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_Inventory_ProductInventory_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Inventory_ProductInventory] PRIMARY KEY CLUSTERED ([ProductInventoryID] ASC)
    , CONSTRAINT [FK_Inventory_ProductInventory_Inventory_Product] FOREIGN KEY ([ProductID]) REFERENCES [Inventory].[Product]([ProductID])
    , CONSTRAINT [FK_Inventory_ProductInventory_Inventory_Warehouse] FOREIGN KEY ([WarehouseID]) REFERENCES [Inventory].[Warehouse]([WarehouseID])
    , CONSTRAINT [FK_Inventory_ProductInventory_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Inventory_ProductInventory_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
)
GO

CREATE TABLE [Procurement].[RequisitionVoucherType] (
    [VoucherTypeID] INT              IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR(64)     NOT NULL,
    [Description]   NVARCHAR(256)    NULL,
    [rowguid]       UNIQUEIDENTIFIER CONSTRAINT [DF_Procurement_RequisitionVoucherType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_Procurement_RequisitionVoucherType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Procurement_RequisitionVoucherType] PRIMARY KEY CLUSTERED ([VoucherTypeID] ASC)
)
GO

CREATE TABLE [Warehousing].[IssueReceiptVoucherType] (
    [VoucherTypeID] INT              IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR(64)     NOT NULL,
    [Description]   NVARCHAR(256)    NULL,
    [rowguid]       UNIQUEIDENTIFIER CONSTRAINT [DF_Warehousing_IssueReceiptVoucherType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_Warehousing_IssueReceiptVoucherType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Warehousing_IssueReceiptVoucherType] PRIMARY KEY CLUSTERED ([VoucherTypeID] ASC)
)
GO

CREATE TABLE [Procurement].[RequisitionVoucher] (
    [VoucherID]          INT              IDENTITY (1, 1) NOT NULL,
	[VoucherTypeID]      INT              NOT NULL,
    [FiscalPeriodID]     INT              NOT NULL,
    [BranchID]           INT              NOT NULL,
    [RequesterID]        INT              NOT NULL,
    [ReceiverID]         INT              NOT NULL,
    [RequesterUnitID]    INT              NOT NULL,
    [ReceiverUnitID]     INT              NOT NULL,
    [WarehouseID]        INT              NOT NULL,
    [ServiceJobID]       INT              NULL,
    [FullAccountID]      INT              NOT NULL,
    [FullDetailID]       INT              NULL,
    [DocumentID]         INT              NOT NULL,
    [No]                 NVARCHAR(64)     NOT NULL,
    [Reference]          NVARCHAR(64)     NULL,
    [OrderedDate]        DATETIME         NOT NULL,
    [RequiredDate]       DATETIME         NULL,
    [PromisedDate]       DATETIME         NULL,
    [Reason]             NVARCHAR(256)    NULL,
    [WarehouseComment]   NVARCHAR(256)    NULL,
    [IsActive]           BIT              CONSTRAINT [DF_Procurement_RequisitionVoucher_IsActive] DEFAULT (0) NOT NULL,
    [Description]        NVARCHAR(256)    NULL,
    [Timestamp]          TIMESTAMP        NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Procurement_RequisitionVoucher_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [rowguid]                UNIQUEIDENTIFIER CONSTRAINT [DF_Procurement_RequisitionVoucher_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL
    , CONSTRAINT [PK_Procurement_RequisitionVoucher] PRIMARY KEY CLUSTERED ([VoucherID] ASC)
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Procurement_RequisitionVoucherType] FOREIGN KEY ([VoucherTypeID]) REFERENCES [Procurement].[RequisitionVoucherType]([VoucherTypeID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Contact_Requester] FOREIGN KEY ([RequesterID]) REFERENCES [Contact].[BusinessPartner]([PartnerID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Contact_Receiver] FOREIGN KEY ([ReceiverID]) REFERENCES [Contact].[BusinessPartner]([PartnerID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Corporate_RequesterUnit] FOREIGN KEY ([RequesterUnitID]) REFERENCES [Corporate].[BusinessUnit]([UnitID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Corporate_ReceiverUnit] FOREIGN KEY ([ReceiverUnitID]) REFERENCES [Corporate].[BusinessUnit]([UnitID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Inventory_Warehouse] FOREIGN KEY ([WarehouseID]) REFERENCES [Inventory].[Warehouse]([WarehouseID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Core_ServiceJob] FOREIGN KEY ([ServiceJobID]) REFERENCES [Core].[ServiceJob]([JobID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Finance_FullAccount] FOREIGN KEY ([FullAccountID]) REFERENCES [Finance].[FullAccount]([FullAccountID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Finance_FullDetail] FOREIGN KEY ([FullDetailID]) REFERENCES [Finance].[FullDetail]([FullDetailID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucher_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document]([DocumentID])
)
GO

CREATE TABLE [Procurement].[RequisitionVoucherLine] (
    [LineID]                INT              IDENTITY (1, 1) NOT NULL,
    [VoucherID]             INT              NOT NULL,
    [WarehouseID]           INT              NOT NULL,
    [ProductID]             INT              NOT NULL,
    [UomID]                 INT              NOT NULL,
    [BranchID]              INT              NOT NULL,
    [FiscalPeriodID]        INT              NOT NULL,
    [FullAccountID]         INT              NOT NULL,
    [FullDetailID]          INT              NULL,
	[ActionID]              INT              NOT NULL,
    [No]                    INT              NOT NULL,
    [OrderedQuantity]       FLOAT            NOT NULL,
    [DeliveredQuantity]     FLOAT            NULL,
    [ReservedQuantity]      FLOAT            NULL,
    [LastOrderedQuantity]   FLOAT            NULL,
    [RequiredDate]          DATETIME         NOT NULL,
    [PromisedDate]          DATETIME         NULL,
    [DeliveredDate]         DATETIME         NULL,
    [LastOrderedDate]       DATETIME         NULL,
    [IsActive]              BIT              CONSTRAINT [DF_Procurement_RequisitionVoucherLine_IsActive] DEFAULT (0) NOT NULL,
    [Description]           NVARCHAR(256)    NULL,
    [Timestamp]             TIMESTAMP        NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Procurement_RequisitionVoucherLine_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Procurement_RequisitionVoucherLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL
    , CONSTRAINT [PK_Procurement_RequisitionVoucherLine] PRIMARY KEY CLUSTERED ([LineID] ASC)
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Procurement_RequisitionVoucher] FOREIGN KEY ([VoucherID]) REFERENCES [Procurement].[RequisitionVoucher]([VoucherID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Inventory_Warehouse] FOREIGN KEY ([WarehouseID]) REFERENCES [Inventory].[Warehouse]([WarehouseID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Inventory_Product] FOREIGN KEY ([ProductID]) REFERENCES [Inventory].[Product]([ProductID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Inventory_Uom] FOREIGN KEY ([UomID]) REFERENCES [Inventory].[UOM]([UomID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Finance_FullAccount] FOREIGN KEY ([FullAccountID]) REFERENCES [Finance].[FullAccount]([FullAccountID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Finance_FullDetail] FOREIGN KEY ([FullDetailID]) REFERENCES [Finance].[FullDetail]([FullDetailID])
    , CONSTRAINT [FK_Procurement_RequisitionVoucherLine_Core_DocumentAction] FOREIGN KEY ([ActionID]) REFERENCES [Core].[DocumentAction]([ActionID])
)
GO

CREATE TABLE [Warehousing].[IssueReceiptVoucher] (
    [VoucherID]            INT              IDENTITY (1, 1) NOT NULL,
    [FiscalPeriodID]       INT              NOT NULL,
    [BranchID]             INT              NOT NULL,
    [ActingPartnerID]      INT              NOT NULL,
    [WarehouseID]          INT              NOT NULL,
    [PricedVoucherID]      INT              NOT NULL,
    [PartnerFullAccountID] INT              NOT NULL,
    [PartnerFullDetailID]  INT              NULL,
    [DocumentID]           INT              NOT NULL,
    [No]                   NVARCHAR(64)     NOT NULL,
    [IsActive]             BIT              CONSTRAINT [DF_Warehousing_IssueReceiptVoucher_IsActive] DEFAULT (0) NOT NULL,
    [Reference]            NVARCHAR(64)     NULL,
    [Type]                 SMALLINT         NOT NULL,
    [Description]          NVARCHAR(256)    NULL,
    [Timestamp]            TIMESTAMP        NOT NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_Warehousing_IssueReceiptVoucher_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [rowguid]              UNIQUEIDENTIFIER CONSTRAINT [DF_Warehousing_IssueReceiptVoucher_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL
    , CONSTRAINT [PK_Warehousing_IssueReceiptVoucher] PRIMARY KEY CLUSTERED ([VoucherID] ASC)
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucher_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucher_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucher_Contact_ActingPartner] FOREIGN KEY ([ActingPartnerID]) REFERENCES [Contact].[BusinessPartner]([PartnerID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucher_Inventory_Warehouse] FOREIGN KEY ([WarehouseID]) REFERENCES [Inventory].[Warehouse]([WarehouseID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucher_Warehousing_PricedVoucher] FOREIGN KEY ([PricedVoucherID]) REFERENCES [Warehousing].[IssueReceiptVoucher]([VoucherID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucher_Finance_PartnerFullAccount] FOREIGN KEY ([PartnerFullAccountID]) REFERENCES [Finance].[FullAccount]([FullAccountID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucher_Finance_PartnerFullDetail] FOREIGN KEY ([PartnerFullDetailID]) REFERENCES [Finance].[FullDetail]([FullDetailID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucher_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document]([DocumentID])
)
GO

CREATE TABLE [Warehousing].[IssueReceiptVoucherLine] (
    [LineID]               INT              IDENTITY (1, 1) NOT NULL,
    [VoucherID]            INT              NOT NULL,
    [WarehouseID]          INT              NOT NULL,
    [ProductID]            INT              NOT NULL,
    [UomID]                INT              NOT NULL,
    [CurrencyID]           INT              NOT NULL,
    [RequisitionVoucherID] INT              NOT NULL,
    [BranchID]             INT              NOT NULL,
    [FiscalPeriodID]       INT              NOT NULL,
    [FullAccountID]        INT              NOT NULL,
    [FullDetailID]         INT              NULL,
    [No]                   INT              NOT NULL,
    [Quantity]             FLOAT            NOT NULL,
    [UnitPrice]            FLOAT            NOT NULL,
    [CurrencyUnitPrice]    FLOAT            NULL,
    [Remainder]            FLOAT            NULL,
    [IsActive]             BIT              CONSTRAINT [DF_Warehousing_IssueReceiptVoucherLine_IsActive] DEFAULT (0) NOT NULL,
    [Description]          NVARCHAR(256)    NULL,
    [Timestamp]            TIMESTAMP        NOT NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_Warehousing_IssueReceiptVoucherLine_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [rowguid]              UNIQUEIDENTIFIER CONSTRAINT [DF_Warehousing_IssueReceiptVoucherLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL
    , CONSTRAINT [PK_Warehousing_IssueReceiptVoucherLine] PRIMARY KEY CLUSTERED ([LineID] ASC)
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Warehousing_Voucher] FOREIGN KEY ([VoucherID]) REFERENCES [Warehousing].[IssueReceiptVoucher]([VoucherID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Inventory_Warehouse] FOREIGN KEY ([WarehouseID]) REFERENCES [Inventory].[Warehouse]([WarehouseID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Inventory_Product] FOREIGN KEY ([ProductID]) REFERENCES [Inventory].[Product]([ProductID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Inventory_UOM] FOREIGN KEY ([UomID]) REFERENCES [Inventory].[UOM]([UomID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Procurement_RequisitionVoucher] FOREIGN KEY ([RequisitionVoucherID]) REFERENCES [Procurement].[RequisitionVoucher]([VoucherID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Finance_FullAccount] FOREIGN KEY ([FullAccountID]) REFERENCES [Finance].[FullAccount]([FullAccountID])
    , CONSTRAINT [FK_Warehousing_IssueReceiptVoucherLine_Finance_FullDetail] FOREIGN KEY ([FullDetailID]) REFERENCES [Finance].[FullDetail]([FullDetailID])
)
GO

CREATE TABLE [Sales].[Invoice] (
    [InvoiceID]             INT              IDENTITY (1, 1) NOT NULL,
    [PartnerID]             INT              NOT NULL,
    [CustomerID]            INT              NOT NULL,
    [ReferenceInvoiceID]    INT              NOT NULL,
    [IssueReceiptVoucherID] INT              NOT NULL,
    [FiscalPeriodID]        INT              NOT NULL,
    [BranchID]              INT              NOT NULL,
    [FullAccountID]         INT              NOT NULL,
    [FullDetailID]          INT              NULL,
    [PartnerFullAccountID]  INT              NOT NULL,
    [PartnerFullDetailID]   INT              NULL,
    [DocumentID]            INT              NOT NULL,
    [No]                    NVARCHAR(64)     NOT NULL,
    [IsActive]              BIT              CONSTRAINT [DF_Sales_Invoice_IsActive] DEFAULT (0) NOT NULL,
    [IsCancelled]           BIT              NOT NULL,
    [Type]                  SMALLINT         NOT NULL,
    [Reference]             NVARCHAR(64)     NULL,
    [Date]                  DATETIME         NOT NULL,
    [Discount]              FLOAT            NOT NULL,
    [Expense]               FLOAT            NOT NULL,
    [ContractNo]            NVARCHAR(64)     NULL,
    [ShipmentNo]            NVARCHAR(64)     NULL,
    [Description]           NVARCHAR(256)    NULL,
    [Timestamp]             TIMESTAMP        NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Sales_Invoice_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Sales_Invoice_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL
    , CONSTRAINT [PK_Sales_Invoice] PRIMARY KEY CLUSTERED ([InvoiceID] ASC)
    , CONSTRAINT [FK_Sales_Invoice_Contact_BusinessPartner] FOREIGN KEY ([PartnerID]) REFERENCES [Contact].[BusinessPartner]([PartnerID])
    , CONSTRAINT [FK_Sales_Invoice_Contact_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [Contact].[Customer]([CustomerID])
    , CONSTRAINT [FK_Sales_Invoice_Sales_ReferenceInvoice] FOREIGN KEY ([ReferenceInvoiceID]) REFERENCES [Sales].[Invoice]([InvoiceID])
    , CONSTRAINT [FK_Sales_Invoice_Warehousing_IssueReceiptVoucher] FOREIGN KEY ([IssueReceiptVoucherID]) REFERENCES [Warehousing].[IssueReceiptVoucher]([VoucherID])
    , CONSTRAINT [FK_Sales_Invoice_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Sales_Invoice_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Sales_Invoice_Finance_FullAccount] FOREIGN KEY ([FullAccountID]) REFERENCES [Finance].[FullAccount]([FullAccountID])
    , CONSTRAINT [FK_Sales_Invoice_Finance_FullDetail] FOREIGN KEY ([FullDetailID]) REFERENCES [Finance].[FullDetail]([FullDetailID])
    , CONSTRAINT [FK_Sales_Invoice_Finance_PartnerFullAccount] FOREIGN KEY ([PartnerFullAccountID]) REFERENCES [Finance].[FullAccount]([FullAccountID])
    , CONSTRAINT [FK_Sales_Invoice_Finance_PartnerFullDetail] FOREIGN KEY ([PartnerFullDetailID]) REFERENCES [Finance].[FullDetail]([FullDetailID])
    , CONSTRAINT [FK_Sales_Invoice_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document]([DocumentID])
)
GO

CREATE TABLE [Sales].[InvoiceLine] (
    [LineID]               INT              IDENTITY (1, 1) NOT NULL,
    [InvoiceID]            INT              NOT NULL,
    [WarehouseID]          INT              NOT NULL,
    [ProductID]            INT              NOT NULL,
    [UomID]                INT              NOT NULL,
    [CurrencyID]           INT              NOT NULL,
    [RequisitionVoucherID] INT              NOT NULL,
    [BranchID]             INT              NOT NULL,
    [FiscalPeriodID]       INT              NOT NULL,
    [FullAccountID]        INT              NOT NULL,
    [FullDetailID]         INT              NULL,
    [No]                   INT              NOT NULL,
    [Quantity]             FLOAT            NOT NULL,
    [UnitPrice]            FLOAT            NOT NULL,
    [CurrencyUnitPrice]    FLOAT            NULL,
    [Discount]             FLOAT            NULL,
    [UnitCost]             FLOAT            NULL,
    [IsActive]             BIT              CONSTRAINT [DF_Sales_InvoiceLine_IsActive] DEFAULT (0) NOT NULL,
    [Description]          NVARCHAR(256)    NULL,
    [Timestamp]            TIMESTAMP        NOT NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_Sales_InvoiceLine_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    [rowguid]              UNIQUEIDENTIFIER CONSTRAINT [DF_Sales_InvoiceLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL
    , CONSTRAINT [PK_Sales_InvoiceLine] PRIMARY KEY CLUSTERED ([LineID] ASC)
    , CONSTRAINT [FK_Sales_InvoiceLine_Sales_Invoice] FOREIGN KEY ([InvoiceID]) REFERENCES [Sales].[Invoice]([InvoiceID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Inventory_Warehouse] FOREIGN KEY ([WarehouseID]) REFERENCES [Inventory].[Warehouse]([WarehouseID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Inventory_Product] FOREIGN KEY ([ProductID]) REFERENCES [Inventory].[Product]([ProductID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Inventory_Uom] FOREIGN KEY ([UomID]) REFERENCES [Inventory].[UOM]([UomID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Procurement_RequisitionVoucher] FOREIGN KEY ([RequisitionVoucherID]) REFERENCES [Procurement].[RequisitionVoucher]([VoucherID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Finance_FullAccount] FOREIGN KEY ([FullAccountID]) REFERENCES [Finance].[FullAccount]([FullAccountID])
    , CONSTRAINT [FK_Sales_InvoiceLine_Finance_FullDetail] FOREIGN KEY ([FullDetailID]) REFERENCES [Finance].[FullDetail]([FullDetailID])
)
GO

-- Create system records for security (NOTE: These records will be migrated to SYS database in a later stage)

-- admin user is added with password 'Admin@Tadbir1395'
SET IDENTITY_INSERT [Auth].[User] ON
INSERT INTO [Auth].[User] (UserID, UserName, PasswordHash, IsEnabled) VALUES (1, N'admin', '5ab4a25e31220c3b103aef3e32596211b90238a0d5933288efbd36c5154b82ff', 1)
SET IDENTITY_INSERT [Auth].[User] OFF

SET IDENTITY_INSERT [Auth].[Role] ON
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (1, N'راهبر سیستم', N'این نقش دارای کلیه دسترسی های تعریف شده در برنامه بوده و قابل اصلاح یا حذف نیست.')
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (2, N'کارشناس حسابداری', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (3, N'رییس حسابداری', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (4, N'معاون مالی', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (5, N'مدیر مالی', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (6, N'سرپرست واحد', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (7, N'مدیر واحد', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (8, N'انباردار', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (9, N'سرپرست انبار', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (10, N'مدیر انبار', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (11, N'کارشناس حسابداری انبار', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (12, N'کارشناس فروش', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (13, N'معاون فروش', NULL)
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (14, N'مدیر فروش', NULL)
SET IDENTITY_INSERT [Auth].[Role] OFF

SET IDENTITY_INSERT [Auth].[UserRole] ON
INSERT INTO [Auth].[UserRole] (UserRoleID, UserID, RoleID) VALUES (1, 1, 1)
SET IDENTITY_INSERT [Auth].[UserRole] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (1, N'مدیریت سرفصل های مالی', N'Account')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (2, N'مدیریت اسناد مالی', N'Transaction')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (3, N'مدیریت کاربران', N'User')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (4, N'مدیریت نقش ها', N'Role')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (5, N'مدیریت درخواست های کالا', N'RequisitionVoucher')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (6, N'مدیریت حواله های انبار', N'IssueReceiptVoucher')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (7, N'مدیریت فاکتورهای فروش', N'SalesInvoice')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (8, N'مدیریت موجودی کالا', N'ProductInventory')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (1, 1, N'مشاهده حساب ها', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (2, 1, N'ایجاد حساب', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (3, 1, N'اصلاح حساب', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (4, 1, N'حذف حساب', 8)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (5, 2, N'مشاهده اسناد', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (6, 2, N'ایجاد سند', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (7, 2, N'اصلاح سند', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (8, 2, N'حذف سند', 8)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (9, 2, N'تنظیم سند', 16)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (10, 2, N'بررسی سند', 32)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (11, 2, N'تایید سند', 64)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (12, 2, N'تصویب سند', 128)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (13, 3, N'مشاهده کاربران', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (14, 3, N'ایجاد کاربر', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (15, 3, N'اصلاح کاربر', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (16, 4, N'مشاهده نقش ها', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (17, 4, N'ایجاد نقش', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (18, 4, N'اصلاح نقش', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (19, 4, N'حذف نقش', 8)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (20, 4, N'تخصیص کاربر به نقش', 16)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (21, 4, N'تخصیص شعبه به نقش', 32)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (22, 5, N'مشاهده درخواست های کالا', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (23, 5, N'ایجاد درخواست کالا', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (24, 5, N'اصلاح درخواست کالا', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (25, 5, N'حذف درخواست کالا', 8)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (26, 5, N'تنظیم درخواست کالا', 16)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (27, 5, N'تایید درخواست کالا', 32)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (28, 5, N'تصویب درخواست کالا', 64)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (29, 6, N'تبدیل درخواست کالا به حواله', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (30, 6, N'ثبت حواله انبار', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (31, 6, N'بررسی مجدد حواله انبار', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (32, 6, N'تایید حواله انبار', 8)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (33, 6, N'تصویب حواله انبار', 16)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (34, 6, N'ریالی کردن حواله مقداری', 32)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (35, 7, N'تبدیل حواله به فاکتور فروش', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (36, 7, N'ثبت فاکتور فروش', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (37, 7, N'بررسی مجدد فاکتور فروش', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (38, 7, N'تایید فاکتور فروش', 8)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (39, 7, N'تصویب فاکتور فروش', 16)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (40, 2, N'ثبت مالی فاکتور فروش', 256)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (41, 2, N'ثبت مالی حواله ریالی', 512)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (42, 8, N'مشاهده موجودی کالا', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (43, 8, N'ایجاد موجودی کالا', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (44, 8, N'اصلاح موجودی کالا', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (45, 8, N'حذف موجودی کالا', 8)
SET IDENTITY_INSERT [Auth].[Permission] OFF

SET IDENTITY_INSERT [Auth].[RolePermission] ON
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (1, 1, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (2, 1, 2)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (3, 1, 3)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (4, 1, 4)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (5, 1, 5)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (6, 1, 6)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (7, 1, 7)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (8, 1, 8)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (9, 1, 9)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (10, 1, 10)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (11, 1, 11)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (12, 1, 12)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (13, 1, 13)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (14, 1, 14)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (15, 1, 15)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (16, 1, 16)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (17, 1, 17)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (18, 1, 18)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (19, 1, 19)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (20, 1, 20)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (21, 1, 21)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (22, 2, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (23, 2, 5)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (24, 2, 6)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (25, 2, 7)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (26, 2, 8)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (27, 2, 9)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (28, 3, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (29, 3, 5)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (30, 3, 6)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (31, 3, 7)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (32, 3, 8)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (33, 3, 10)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (34, 4, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (35, 4, 5)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (36, 4, 11)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (37, 5, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (38, 5, 5)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (39, 5, 12)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (40, 1, 22)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (41, 1, 23)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (42, 1, 24)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (43, 1, 25)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (44, 1, 26)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (45, 1, 27)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (46, 1, 28)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (47, 1, 29)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (48, 1, 30)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (49, 1, 31)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (50, 1, 32)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (51, 1, 33)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (52, 1, 34)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (53, 1, 35)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (54, 1, 36)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (55, 1, 37)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (56, 1, 38)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (57, 1, 39)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (58, 1, 40)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (59, 1, 41)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (60, 1, 42)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (61, 1, 43)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (62, 1, 44)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (63, 1, 45)
SET IDENTITY_INSERT [Auth].[RolePermission] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

