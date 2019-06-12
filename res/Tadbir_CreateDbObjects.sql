USE [NGTadbir]
GO

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
    [CurrencyID]     INT              IDENTITY (1, 1) NOT NULL,
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
    [GroupID]          INT              IDENTITY (1, 1) NOT NULL,
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
    [BranchID]       INT              IDENTITY (1, 1) NOT NULL,
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
    [FiscalPeriodID]   INT              IDENTITY (1, 1) NOT NULL,
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
    [AccountID]              INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]               INT              NULL,
    [FiscalPeriodID]         INT              NOT NULL,
    [BranchID]               INT              NOT NULL,
    [GroupID]                INT              NULL,
    [CurrencyID]             INT              CONSTRAINT [DF_Finance_Account_CurrencyID] DEFAULT (1) NOT NULL,
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
    [VoucherID]       INT              IDENTITY (1, 1) NOT NULL,
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
    [DetailID]          INT              IDENTITY (1, 1) NOT NULL,
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
    [CostCenterID]   INT              IDENTITY (1, 1) NOT NULL,
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
    [ProjectID]      INT              IDENTITY (1, 1) NOT NULL,
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
    [LineID]          INT              IDENTITY (1, 1) NOT NULL,
    [VoucherID]       INT              NOT NULL,
    [FiscalPeriodID]  INT              NOT NULL,
    [BranchID]        INT              NOT NULL,
    [AccountID]       INT              NOT NULL,
    [DetailID]        INT              NULL,
    [CostCenterID]    INT              NULL,
    [ProjectID]       INT              NULL,
    [CurrencyID]      INT              NULL,
    [CreatedByID]     INT              NOT NULL,
    [RowNo]           INT              NOT NULL,
    [Debit]           MONEY            NOT NULL,
    [Credit]          MONEY            NOT NULL,
    [Description]     NVARCHAR(1024)   NULL,
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
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_DetailAccount] FOREIGN KEY ([DetailID]) REFERENCES [Finance].[DetailAccount]([DetailID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
)
GO

CREATE TABLE [Finance].[AccountDetailAccount] (
    [AccountDetailAccountID] INT              IDENTITY (1, 1) NOT NULL,
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
    [AccountCostCenterID] INT              IDENTITY (1, 1) NOT NULL,
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
    [AccountProjectID] INT              IDENTITY (1, 1) NOT NULL,
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
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (2, N'Checked')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (3, N'Finalized')
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


-- Insert suggested account groups...
SET IDENTITY_INSERT [Finance].[AccountGroup] ON
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (1, N'داراييهاى جارى', 0, N'CategoryAsset', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (2, N'داراييهاى غيرجارى', 0, N'CategoryAsset', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (3, N'بدهيهاى جارى', 0, N'CategoryLiability', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (4, N'بدهيهاى غيرجارى', 0, N'CategoryLiability', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (5, N'حقوق صاحبان سرمايه', 0, N'CategoryCapital', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (6, N'فروش', 0, N'CategorySales', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (7, N'ساير درآمدها', 0, N'CategoryIncome', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (8, N'قيمت تمام شده كالاى فروش رفته', 1, N'CategoryExpense', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (9, N'خرید', -1, N'CategoryPurchase', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (10, N'هزينه هاى عملياتى', 0, N'CategoryExpense', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (11, N'هزينه هاى غيرعملياتى', 0, N'CategoryExpense', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (12, N'رابط', 0, N'CategoryAssociation', NULL)
INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (13, N'حسابهاى انتظامى', 0, N'CategoryCoordination', NULL)
SET IDENTITY_INSERT [Finance].[AccountGroup] OFF

SET IDENTITY_INSERT [Finance].[AccountCollectionCategory] ON 
GO
INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (1, N'ترازنامه ', N'49e54def-d6b9-4cd3-a065-8feef13026d9', CAST(N'2019-01-15T12:26:01.053' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (2, N'سود و زیان', N'21ed1889-c1a5-46dc-b352-60b577f42154', CAST(N'2019-01-15T12:26:13.500' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (3, N'خزانه داری  ', N'53757fa5-5e85-495d-ac53-a8f5a4869f6b', CAST(N'2019-01-15T12:26:26.410' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (4, N'خرید و فروش', N'2e9ec944-c56b-4350-8489-0e2c24a1757d', CAST(N'2019-01-15T12:26:37.690' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (5, N'بستن حساب ها ', N'247284c1-6d8c-4e9c-9fd2-8dc7aeb0d290', CAST(N'2019-01-15T12:26:52.327' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (6, N'انبار', N'8c22dd59-094b-4ca8-b49f-094c8e157b50', CAST(N'2019-01-15T12:27:05.800' AS DateTime))
GO
INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (7, N'اموال', N'be57a7ce-a837-4b84-9cfa-e6e2b6f638f9', CAST(N'2019-01-15T12:27:19.193' AS DateTime))
GO
SET IDENTITY_INSERT [Finance].[AccountCollectionCategory] OFF
GO

SET IDENTITY_INSERT [Finance].[AccountCollection] ON 
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (1, 1, N'داراييهاى جارى  ', 1, 0, 2, N'afc7ae6a-4520-4263-a5a6-b32e0829118b', CAST(N'2019-01-15T12:32:19.127' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (2, 1, N'داراييهاى غيرجارى ', 1, 0, 2, N'7c34827f-3f03-4be0-8d30-ec4ebb1cc6fe', CAST(N'2019-01-15T12:32:37.267' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (3, 1, N'بدهيهاى جارى ', 1, 0, 2, N'3557c04b-fde4-40a7-b8f3-004f4b75cac0', CAST(N'2019-01-15T12:33:01.480' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (4, 1, N'بدهيهاى غيرجارى', 1, 0, 2, N'0f4021af-2e5c-4ce0-8757-26ee3dd64df4', CAST(N'2019-01-15T12:33:33.307' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (5, 1, N'حقوق صاحبان سرمايه ', 1, 0, 2, N'1755af0b-37c9-4758-a451-68f84af5439a', CAST(N'2019-01-15T12:33:49.270' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (6, 1, N'حسابهاى انتظامى', 1, 0, 2, N'1cb5f6d6-3caf-4f67-a091-c63c0b15bbeb', CAST(N'2019-01-15T12:34:08.290' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (9, 2, N'فروش', 1, 0, 2, N'f1210802-59a7-4d86-803b-e90e45d373f9', CAST(N'2019-01-15T12:36:50.283' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (10, 2, N'برگشت  از فروش و تخفیفات ', 1, 0, 2, N'be473c3e-273c-498d-9f27-fbf9c2b70f3c', CAST(N'2019-01-15T12:37:47.480' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (11, 2, N'قيمت تمام شده كالاى فروش رفته', 1, 0, 1, N'8af11bc5-21fb-4af6-bf18-0517fa8c5d40', CAST(N'2019-01-15T12:38:36.130' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (12, 2, N'خرید', 1, 0, 0, N'da24deea-1c98-4ad9-9363-13327e32eaec', CAST(N'2019-01-15T12:39:19.490' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (13, 2, N'برگشت از خرید و تخفیفات ', 1, 0, 0, N'e1b78a0b-e200-415a-818a-fce1048c5735', CAST(N'2019-01-15T12:40:04.470' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (14, 2, N'هزينه هاى عملياتى', 1, 0, 2, N'4bc26140-7c45-4e2a-96d5-5ccf6266f713', CAST(N'2019-01-15T12:40:41.230' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (15, 2, N'سایر هزينه ها و درآمد ها', 1, 0, 2, N'94299081-734d-4f47-a99c-ec7b9ae8580c', CAST(N'2019-01-15T12:41:03.997' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (16, 3, N'صندوق ', 1, 0, 2, N'df0b3009-6075-451f-a235-c26ffae7f06c', CAST(N'2019-01-15T12:45:09.350' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (17, 3, N'بانک ', 1, 0, 2, N'6ed87a7a-848a-4855-8862-4b8e7b7d3f97', CAST(N'2019-01-15T12:45:23.713' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (18, 3, N'اسناد دریافتنی ', 1, 0, 2, N'ad3c0eb5-3cf0-4369-af43-2b3fa514a850', CAST(N'2019-01-15T12:45:40.737' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (19, 3, N'اسناد پرداختنی', 1, 0, 2, N'c7ed884b-e4e5-40ce-86ed-a5c4d8b35323', CAST(N'2019-01-15T12:46:11.560' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (20, 3, N'اسناد دریافتنی تضمینی ', 1, 0, 2, N'2b287b0b-1211-44d7-8176-57d2ad21b3e5', CAST(N'2019-01-15T12:46:37.440' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (21, 3, N'اسناد پرداختنی تضمینی ', 1, 0, 2, N'e859c4d9-a385-415a-91e5-48b0efd9b966', CAST(N'2019-01-15T12:46:54.073' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (22, 3, N'اسناد درجریان وصول ', 1, 0, 2, N'fcfc7d6b-eda1-4aef-b0ce-ae6b38102dac', CAST(N'2019-01-15T12:47:06.490' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (23, 3, N'اسناد برگشتی ', 1, 0, 2, N'ebc53de1-f7a2-47b6-8e12-c597d757c4f0', CAST(N'2019-01-15T12:47:22.033' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (24, 3, N'تنخواه گردان ها ', 1, 0, 2, N'94bc42d6-d86c-432a-9657-4e7e98666eda', CAST(N'2019-01-15T12:47:54.697' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (25, 4, N'فروش ', 1, 0, 2, N'8c4e7347-736f-44f3-b7f8-20dffb73ade2', CAST(N'2019-01-15T12:48:23.877' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (26, 4, N'برگشت از فروش ', 1, 0, 2, N'99906d33-c220-4b91-9f3b-f9dee4da979d', CAST(N'2019-01-15T12:48:36.030' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (27, 4, N'خرید ', 1, 0, 0, N'40805c86-0018-4976-a0d2-0e2dcc820c62', CAST(N'2019-01-15T12:48:50.010' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (28, 4, N'برگشت از خرید ', 1, 0, 0, N'b7d8b020-058b-4487-9a18-2781eb62f75f', CAST(N'2019-01-15T12:49:02.973' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (29, 4, N'اضافات فاکتور فروش ', 1, 0, 2, N'88f8c1ce-d96f-49f2-8702-70402c6fcc29', CAST(N'2019-01-15T12:49:16.573' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (30, 4, N'اضافات فاکتور خرید', 1, 0, 2, N'c9b09d2f-25d9-4192-aa55-5cc6f492348f', CAST(N'2019-01-15T12:49:33.737' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (31, 4, N'بدهکاران تجاری ', 1, 2, 2, N'b85de96f-f48d-4208-bcd0-78e8bd9d2c44', CAST(N'2019-01-15T12:50:00.620' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (32, 4, N'بستانکاران تجاری ', 1, 2, 2, N'56d36c06-fe0c-4a88-84b6-09c4cee7c646', CAST(N'2019-01-15T12:50:32.900' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (33, 4, N'تخفیفات فروش ', 0, 0, 2, N'43eaf605-e1c4-48a6-b532-761dee9e64db', CAST(N'2019-01-15T12:50:51.310' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (34, 4, N'تخفیفات خرید ', 0, 0, 0, N'83b0e472-c21a-4800-b479-65b25d476716', CAST(N'2019-01-15T12:51:41.607' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (35, 4, N'قیمت تمام شده ', 0, 0, 2, N'a41051cf-3919-4eef-a9bb-f5e100afd6d0', CAST(N'2019-01-15T12:52:06.367' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (36, 4, N'فروشنده / خریدار  متفرقه ', 0, 1, 2, N'b20122c9-8a39-445d-bbe0-3f424c0529e2', CAST(N'2019-01-15T12:52:29.343' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (37, 4, N'مالیات پرداختنی ', 0, 0, 2, N'cb889886-cc01-4583-9ad2-267adfff47b2', CAST(N'2019-01-15T12:52:51.373' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (38, 4, N'عوارض پرداختنی', 0, 0, 2, N'60332921-671e-4f04-af3e-3c0b9ebadebd', CAST(N'2019-01-15T12:53:11.953' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (39, 4, N'مالیات دریافتنی', 0, 0, 2, N'c7a96c21-8160-417b-8185-7e25846d5247', CAST(N'2019-01-15T12:53:31.907' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (40, 4, N'عوارض دریافتنی', 0, 0, 2, N'521498a4-28c1-477d-89e6-95365e01e4fb', CAST(N'2019-01-15T12:53:55.050' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (41, 5, N'افتتاحیه ', 0, 1, 2, N'de1f3fb4-383f-4222-af54-62f3211b5d61', CAST(N'2019-01-15T12:54:36.443' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (42, 5, N'اختتامیه ', 0, 1, 2, N'7670e838-690b-4011-90e1-5b4370f932af', CAST(N'2019-01-15T12:54:51.660' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (43, 5, N'عملکرد ', 0, 1, 2, N'44c8cf51-a426-433d-a950-e8d62653f553', CAST(N'2019-01-15T12:55:11.510' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (44, 5, N'سود و زیان سال جاری', 0, 1, 2, N'3b1cab2a-7f71-4088-a35d-82fc44d42f44', CAST(N'2019-01-15T12:55:30.237' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (45, 5, N'سود و زیان انباشته ', 0, 1, 2, N'4119925a-e825-4a3a-b86f-1899c9ddaf33', CAST(N'2019-01-15T12:55:50.610' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (46, 6, N'موجودی کالا ', 0, 2, 2, N'af71d08f-dc59-4de7-a745-04563f083aa9', CAST(N'2019-01-15T12:56:51.717' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (47, 6, N'کنترل دستمزد', 0, 0, 2, N'67f35b34-d512-47cf-9602-f02d4b7d1cd1', CAST(N'2019-01-15T13:26:26.080' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (48, 6, N'کنترل سربار ', 0, 0, 2, N'082edb78-743f-4f6c-b70a-a989421ed780', CAST(N'2019-01-15T13:27:04.580' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (49, 7, N'اموال ', 0, 0, 2, N'70db4c8e-1763-4f92-a0fa-f478c2af259a', CAST(N'2019-01-15T13:27:42.067' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (50, 7, N'سود و زیان عملیات اموال', 0, 0, 2, N'05cd9415-9a94-42a2-9321-189eb3677e0d', CAST(N'2019-01-15T13:28:04.187' AS DateTime))
GO
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (51, 7, N'اموال انتقالی ', 0, 0, 2, N'0058abb5-6531-44f7-b1a1-fa78c0d67a30', CAST(N'2019-01-15T13:28:52.073' AS DateTime))
GO
SET IDENTITY_INSERT [Finance].[AccountCollection] OFF
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO
