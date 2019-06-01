--USE [TadbirNG]
--GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA [Core]
GO

CREATE SCHEMA [Config]
GO

CREATE SCHEMA [Auth]
GO

CREATE SCHEMA [Corporate]
GO

CREATE SCHEMA [Finance]
GO

CREATE SCHEMA [Workflow]
GO


CREATE TABLE [Finance].[Currency] (
    [CurrencyID]     INT              NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Currency_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_Currency_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Currency] PRIMARY KEY CLUSTERED ([CurrencyID] ASC)
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
    [DocumentID]     INT              IDENTITY (1, 1) NOT NULL,
    [TypeID]         INT              NOT NULL,
    [EntityID]       INT              NOT NULL,
    [No]             NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Core_Document_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Core_Document_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_Document] PRIMARY KEY CLUSTERED ([DocumentID] ASC)
    , CONSTRAINT [FK_Core_Document_Core_Type] FOREIGN KEY ([TypeID]) REFERENCES [Core].[DocumentType]([TypeID])
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
    , CONSTRAINT [FK_Core_DocumentAction_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document]([DocumentID])
)
GO

CREATE TABLE [Config].[ViewSetting] (
    [ViewSettingID]  INT              IDENTITY (1, 1) NOT NULL,
    [SettingID]      INT              NOT NULL,
    [ViewID]         INT              NULL,
    [ModelType]      VARCHAR(128)     NOT NULL,
    [Values]         NTEXT            NOT NULL,
    [DefaultValues]  NTEXT            NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_ViewSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_ViewSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_ViewSetting] PRIMARY KEY CLUSTERED ([ViewSettingID] ASC)
)
GO

CREATE TABLE [Finance].[AccountGroup] (
    [GroupID]          INT              NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [InventoryMode]    SMALLINT         NOT NULL,
    [Category]         NVARCHAR(64)     NOT NULL,
    [Description]      NVARCHAR(256)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountGroup_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_AccountGroup_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountGroup] PRIMARY KEY CLUSTERED ([GroupID] ASC)
)
GO

CREATE TABLE [Finance].[VoucherOrigin] (
    [OriginID]       INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_VoucherOrigin_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_VoucherOrigin_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_VoucherOrigin] PRIMARY KEY CLUSTERED ([OriginID] ASC)
)
GO

CREATE TABLE [Corporate].[Branch] (
    [BranchID]       INT              NOT NULL,
	[CompanyID]      INT              NOT NULL,
	[ParentID]       INT              NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
	[Level]          INT              CONSTRAINT [DF_Corporate_Branch_Level] DEFAULT (0) NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Corporate_Branch_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Corporate_Branch_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Corporate_Branch] PRIMARY KEY CLUSTERED ([BranchID] ASC)
)
GO

CREATE TABLE [Auth].[RoleBranch] (
    [RoleBranchID]       INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]             INT              NOT NULL,
    [BranchID]           INT              NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RoleBranch_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Auth_RoleBranch_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RoleBranch] PRIMARY KEY CLUSTERED ([RoleBranchID] ASC)
    , CONSTRAINT [FK_Auth_RoleBranch_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
)
GO

CREATE TABLE [Finance].[FiscalPeriod] (
    [FiscalPeriodID]   INT              NOT NULL,
	[CompanyID]        INT              NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [StartDate]        DATETIME         NOT NULL,
    [EndDate]          DATETIME         NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_FiscalPeriod_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_FiscalPeriod_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_FiscalPeriod] PRIMARY KEY CLUSTERED ([FiscalPeriodID] ASC)
)
GO

CREATE TABLE [Auth].[RoleFiscalPeriod] (
    [RoleFiscalPeriodID] INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]             INT              NOT NULL,
    [FiscalPeriodID]     INT              NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RoleFiscalPeriod_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Auth_RoleFiscalPeriod_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RoleFiscalPeriod] PRIMARY KEY CLUSTERED ([RoleFiscalPeriodID] ASC)
    , CONSTRAINT [FK_Auth_RoleFiscalPeriod_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
)
GO

CREATE TABLE [Finance].[Account] (
    [AccountID]              INT              NOT NULL,
    [ParentID]               INT              NULL,
    [FiscalPeriodID]         INT              NOT NULL,
    [BranchID]               INT              NOT NULL,
    [GroupID]                INT              NULL,
    [CurrencyID]             INT              NULL,
    [BranchScope]            SMALLINT         NOT NULL,
    [Code]                   NVARCHAR(16)     NOT NULL,
    [FullCode]               NVARCHAR(256)    NOT NULL,
    [Name]                   NVARCHAR(512)    NOT NULL,
    [Level]                  SMALLINT         NOT NULL,
    [IsActive]               BIT              CONSTRAINT [DF_Finance_Account_IsActive] DEFAULT (1) NOT NULL,
    [IsCurrencyAdjustable]   BIT              CONSTRAINT [DF_Finance_Account_IsCurrencyAdjustable] DEFAULT (1) NOT NULL,
    [TurnoverMode]           SMALLINT         CONSTRAINT [DF_Finance_Account_TurnoverMode] DEFAULT (-1) NOT NULL,
    [Description]            NVARCHAR(512)    NULL,
    [rowguid]                UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Account_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]           DATETIME         CONSTRAINT [DF_Finance_Account_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Account] PRIMARY KEY CLUSTERED ([AccountID] ASC)
    , CONSTRAINT [FK_Finance_Account_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Finance_Account_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Account_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Finance_Account_Finance_Group] FOREIGN KEY ([GroupID]) REFERENCES [Finance].[AccountGroup]([GroupID])
    , CONSTRAINT [FK_Finance_Account_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
)
GO

CREATE TABLE [Finance].[AccountCollectionCategory] (
    [CategoryID]     INT              IDENTITY (1, 1) NOT NULL,    
    [Name]           NVARCHAR(128)    NOT NULL,
	[rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountCollectionCategory_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_AccountCollectionCategory_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountCollectionCategory] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
)
GO

CREATE TABLE [Finance].[AccountCollection] (
    [CollectionID]          INT              IDENTITY (1, 1) NOT NULL,
    [CategoryID]            INT              NOT NULL,    
    [Name]                  NVARCHAR(128)    NOT NULL,
    [MultiSelect]           BIT              NOT NULL,
    [TypeLevel]             SMALLINT         NOT NULL,
    [InventoryMode]         SMALLINT         NOT NULL,
	[rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountCollection_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Finance_AccountCollection_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountCollection] PRIMARY KEY CLUSTERED ([CollectionID] ASC)
    , CONSTRAINT [FK_Finance_AccountCollection_Finance_Category] FOREIGN KEY ([CategoryID]) REFERENCES [Finance].[AccountCollectionCategory]([CategoryID])
)

CREATE TABLE [Finance].[AccountCollectionAccount] (
    [CollectionAccountID]  INT              IDENTITY (1, 1) NOT NULL,
	[CollectionID]         INT              NOT NULL,
    [AccountID]            INT              NOT NULL,
    [BranchID]             INT              NOT NULL,
    [FiscalPeriodID]       INT              NOT NULL,
    [rowguid]              UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountCollectionAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_Finance_AccountCollectionAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountCollectionAccount] PRIMARY KEY CLUSTERED ([CollectionAccountID] ASC)
    , CONSTRAINT [FK_Finance_AccountCollectionAccount_Finance_Collection] FOREIGN KEY ([CollectionID]) REFERENCES [Finance].[AccountCollection]([CollectionID])
    , CONSTRAINT [FK_Finance_AccountCollectionAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Finance_AccountCollectionAccount_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Finance_AccountCollectionAccount_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
)
GO

CREATE TABLE [Finance].[Voucher] (
    [VoucherID]       INT              NOT NULL,
    [FiscalPeriodID]  INT              NOT NULL,
    [BranchID]        INT              NOT NULL,
    [DocumentID]      INT              NULL,
    [StatusID]        INT              NOT NULL,
    [IssuedByID]      INT              NOT NULL,
    [ModifiedByID]    INT              NOT NULL,
    [ConfirmedByID]   INT              NULL,
    [ApprovedByID]    INT              NULL,
    [No]              INT              NOT NULL,
    [DailyNo]         INT              NOT NULL,
    [Date]            DATETIME         NOT NULL,
    [Reference]       NVARCHAR(64)     NULL,
    [Association]     NVARCHAR(64)     NULL,
    [IsBalanced]      BIT              NOT NULL,
    [Type]            SMALLINT         NOT NULL,
    [SubjectType]     SMALLINT         NOT NULL,
    [SaveCount]       INT              NOT NULL,
    [Description]     NVARCHAR(512)    NULL,
	[IssuerName]      NVARCHAR(64)     NOT NULL, 
	[ModifierName]    NVARCHAR(64)     NOT NULL,
	[ConfirmerName]   NVARCHAR(64)     NULL,
	[ApproverName]    NVARCHAR(64)     NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Voucher_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_Voucher_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Voucher] PRIMARY KEY CLUSTERED ([VoucherID] ASC)
    , CONSTRAINT [FK_Finance_Voucher_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Voucher_Finance_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Finance_Voucher_Finance_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document]([DocumentID])
    , CONSTRAINT [FK_Finance_Voucher_Finance_Status] FOREIGN KEY ([StatusID]) REFERENCES [Core].[DocumentStatus]([StatusID])
)
GO

CREATE TABLE [Finance].[DetailAccount] (
    [DetailID]          INT              NOT NULL,
    [ParentID]          INT              NULL,
	[FiscalPeriodID]    INT              NOT NULL,
	[BranchID]          INT              NOT NULL,
	[BranchScope]       SMALLINT         CONSTRAINT [DF_Finance_DetailAccount_BranchScope] DEFAULT (0) NOT NULL,
    [Code]              NVARCHAR(16)     NOT NULL,
    [FullCode]          NVARCHAR(256)    NOT NULL,
    [Name]              NVARCHAR(256)    NOT NULL,
    [Level]             SMALLINT         CONSTRAINT [DF_Finance_DetailAccount_Level] DEFAULT (0) NOT NULL,
    [Description]       NVARCHAR(512)    NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_DetailAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Finance_DetailAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_DetailAccount] PRIMARY KEY CLUSTERED ([DetailID] ASC)
    , CONSTRAINT [FK_Finance_DetailAccount_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_DetailAccount_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_DetailAccount_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
)
GO

CREATE TABLE [Finance].[CostCenter] (
    [CostCenterID]   INT              NOT NULL,
    [ParentID]       INT              NULL,
	[FiscalPeriodID] INT              NOT NULL,
	[BranchID]       INT              NOT NULL,
	[BranchScope]    SMALLINT         CONSTRAINT [DF_Finance_CostCenter_BranchScope] DEFAULT (0) NOT NULL,
    [Code]           NVARCHAR(16)     NOT NULL,
    [FullCode]       NVARCHAR(256)    NOT NULL,
    [Name]           NVARCHAR(256)    NOT NULL,
    [Level]          SMALLINT         CONSTRAINT [DF_Finance_CostCenter_Level] DEFAULT (0) NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_CostCenter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_CostCenter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_CostCenter] PRIMARY KEY CLUSTERED ([CostCenterID] ASC)
    , CONSTRAINT [FK_Finance_CostCenter_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Finance_CostCenter_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_CostCenter_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
)
GO

CREATE TABLE [Finance].[Project] (
    [ProjectID]      INT              NOT NULL,
    [ParentID]       INT              NULL,
	[FiscalPeriodID] INT              NOT NULL,
	[BranchID]       INT              NOT NULL,
	[BranchScope]    SMALLINT         CONSTRAINT [DF_Finance_Project_BranchScope] DEFAULT (0) NOT NULL,
    [Code]           NVARCHAR(16)     NOT NULL,
    [FullCode]       NVARCHAR(256)    NOT NULL,
    [Name]           NVARCHAR(256)    NOT NULL,
    [Level]          SMALLINT         CONSTRAINT [DF_Finance_Project_Level] DEFAULT (0) NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Project_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_Project_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Project] PRIMARY KEY CLUSTERED ([ProjectID] ASC)
    , CONSTRAINT [FK_Finance_Project_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[Project]([ProjectID])
    , CONSTRAINT [FK_Finance_Project_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Project_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
)
GO

CREATE TABLE [Finance].[VoucherLine] (
    [LineID]          INT              NOT NULL,
    [VoucherID]       INT              NOT NULL,
    [FiscalPeriodID]  INT              NOT NULL,
    [BranchID]        INT              NOT NULL,
    [AccountID]       INT              NOT NULL,
    [DetailAccountID] INT              NULL,
    [CostCenterID]    INT              NULL,
    [ProjectID]       INT              NULL,
    [CurrencyID]      INT              NULL,
    [CreatedByID]     INT              NOT NULL,
    [RowNo]           INT              NOT NULL,
    [Debit]           MONEY            NOT NULL,
    [Credit]          MONEY            NOT NULL,
    [Description]     NVARCHAR(512)    NULL,
    [Amount]          FLOAT            NULL,
    [FollowupNo]      NVARCHAR(64)     NULL,
    [CurrencyValue]   MONEY            NULL,
    [Mark]            NVARCHAR(128)    NULL,
    [TypeID]          SMALLINT         NOT NULL,
    [SourceID]        INT              NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_VoucherLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_VoucherLine_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_VoucherLine] PRIMARY KEY CLUSTERED ([LineID] ASC)
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Voucher] FOREIGN KEY ([VoucherID]) REFERENCES [Finance].[Voucher]([VoucherID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
)
GO

CREATE TABLE [Finance].[AccountDetailAccount] (
    [AccountDetailAccountID] INT              NOT NULL,
    [AccountID]              INT              NOT NULL,
    [DetailID]               INT              NOT NULL,
    [rowguid]                UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountDetailAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]           DATETIME         CONSTRAINT [DF_Finance_AccountDetailAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountDetailAccount] PRIMARY KEY CLUSTERED ([AccountDetailAccountID] ASC)
    , CONSTRAINT [FK_Finance_AccountDetailAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
    , CONSTRAINT [FK_Finance_AccountDetailAccount_Finance_DetailAccount] FOREIGN KEY ([DetailID]) REFERENCES [Finance].[DetailAccount] ([DetailID])
)
GO

CREATE TABLE [Finance].[AccountCostCenter] (
    [AccountCostCenterID] INT              NOT NULL,
    [AccountID]           INT              NOT NULL,
    [CostCenterID]        INT              NOT NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountCostCenter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Finance_AccountCostCenter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountCostCenter] PRIMARY KEY CLUSTERED ([AccountCostCenterID] ASC)
    , CONSTRAINT [FK_Finance_AccountCostCenter_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
    , CONSTRAINT [FK_Finance_AccountCostCenter_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter] ([CostCenterID])
)
GO

CREATE TABLE [Finance].[AccountProject] (
    [AccountProjectID] INT              NOT NULL,
    [AccountID]        INT              NOT NULL,
    [ProjectID]        INT              NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountProject_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_AccountProject_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountProject] PRIMARY KEY CLUSTERED ([AccountProjectID] ASC)
    , CONSTRAINT [FK_Finance_AccountProject_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
    , CONSTRAINT [FK_Finance_AccountProject_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project] ([ProjectID])
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
    , CONSTRAINT [FK_Workflow_WorkItemHistory_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document] ([DocumentID])
)
GO

-- Insert system records...
SET IDENTITY_INSERT [Core].[DocumentStatus] ON
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (1, N'Draft')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (2, N'NormalCheck')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (3, N'FinalCheck')
SET IDENTITY_INSERT [Core].[DocumentStatus] OFF

SET IDENTITY_INSERT [Config].[ViewSetting] ON
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (1, 5, 1, 'ViewTreeConfig', N'{"viewId":1,"maxDepth":3,"levels":[{"no":1,"name":"LevelGeneral","codeLength":3,"isEnabled": true},{"no":2,"name":"LevelAuxiliary","codeLength":3,"isEnabled": true},{"no":3,"name":"LevelDetail","codeLength":4,"isEnabled": true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}', N'{"viewId":1,"maxDepth":3,"levels":[{"no":1,"name":"LevelGeneral","codeLength":3},{"no":2,"name":"LevelAuxiliary","codeLength":3},{"no":3,"name":"LevelDetail","codeLength":4},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (2, 5, 6, 'ViewTreeConfig', N'{"viewId":6,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}', N'{"viewId":6,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (3, 5, 7, 'ViewTreeConfig', N'{"viewId":7,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}', N'{"viewId":7,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (4, 5, 8, 'ViewTreeConfig', N'{"viewId":8,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}', N'{"viewId":8,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false}]}')
SET IDENTITY_INSERT [Config].[ViewSetting] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO
