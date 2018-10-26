USE [TadbirDemo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA [Core]
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
    [AccountID]      INT              IDENTITY (1, 1) NOT NULL,
	[ParentID]       INT              NULL,
	[FiscalPeriodID] INT              NOT NULL,
	[BranchID]       INT              NOT NULL,
	[BranchScope]    SMALLINT         CONSTRAINT [DF_Finance_Account_BranchScope] DEFAULT (0) NOT NULL,
    [Code]           NVARCHAR(16)     NOT NULL,
    [FullCode]       NVARCHAR(256)    NOT NULL,
    [Name]           NVARCHAR(512)    NOT NULL,
    [Level]          SMALLINT         CONSTRAINT [DF_Finance_Account_Level] DEFAULT (0) NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Account_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_Account_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Account] PRIMARY KEY CLUSTERED ([AccountID] ASC)
    , CONSTRAINT [FK_Finance_Account_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Finance_Account_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Account_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
)
GO

CREATE TABLE [Finance].[Voucher] (
    [VoucherID]         INT              IDENTITY (1, 1) NOT NULL,
	[FiscalPeriodID]    INT              NOT NULL,
	[BranchID]          INT              NOT NULL,
    [DocumentID]        INT              NULL,
	[StatusID]          INT              NOT NULL,
    [No]                NVARCHAR(64)     NOT NULL,
    [Date]              DATETIME         NOT NULL,
    [Reference]         NVARCHAR(64)     NULL,
    [Description]       NVARCHAR(512)    NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Voucher_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Finance_Voucher_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Voucher] PRIMARY KEY CLUSTERED ([VoucherID] ASC)
    , CONSTRAINT [FK_Finance_Voucher_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Voucher_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
    , CONSTRAINT [FK_Finance_Voucher_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document]([DocumentID])
    , CONSTRAINT [FK_Finance_Voucher_Core_DocumentStatus] FOREIGN KEY ([StatusID]) REFERENCES [Core].[DocumentStatus] ([StatusID])
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
	[CurrencyID]      INT              NOT NULL,
    [Description]     NVARCHAR(512)    NULL,
    [Debit]           MONEY            NOT NULL,
    [Credit]          MONEY            NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_VoucherLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_VoucherLine_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_VoucherLine] PRIMARY KEY CLUSTERED ([LineID] ASC)
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Voucher] FOREIGN KEY ([VoucherID]) REFERENCES [Finance].[Voucher] ([VoucherID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_VoucherLine_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_DetailAccount] FOREIGN KEY ([DetailID]) REFERENCES [Finance].[DetailAccount] ([DetailID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter] ([CostCenterID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project] ([ProjectID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency] ([CurrencyID])
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
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (2, N'NormalCheck')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (3, N'FinalCheck')
SET IDENTITY_INSERT [Core].[DocumentStatus] OFF

-- TODO: Insert supporting records for suggested coding (fiscal period, branch, etc.)

-- Insert suggested account coding...
SET IDENTITY_INSERT [Finance].[Account] ON
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (101, NULL, 1, 1, 0, N'111', N'111', N'وجوه نقد', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (102, 101, 1, 1, 0, N'001', N'111001', N'صندوقها', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (103, 102, 1, 1, 0, N'0001', N'1110010001', N'صندوق', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (104, 101, 1, 1, 0, N'002', N'111002', N'تنخواه گردانها', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (105, 101, 1, 1, 0, N'003', N'111003', N'وجوه در راه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (106, NULL, 1, 1, 0, N'112', N'112', N'موجودى نزد بانكها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (107, 106, 1, 1, 0, N'001', N'112001', N'بانك ملى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (108, 106, 1, 1, 0, N'002', N'112002', N'بانك صادرات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (109, 106, 1, 1, 0, N'003', N'112003', N'بانك تجارت', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (110, 106, 1, 1, 0, N'004', N'112004', N'بانك ملت', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (111, 106, 1, 1, 0, N'005', N'112005', N'بانك سپه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (112, 106, 1, 1, 0, N'006', N'112006', N'بانك مسكن', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (113, 106, 1, 1, 0, N'007', N'112007', N'بانك رفاه كارگران', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (114, 106, 1, 1, 0, N'008', N'112008', N'بانك كشاورزى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (115, 106, 1, 1, 0, N'009', N'112009', N'بانک پاسارگاد', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (116, 106, 1, 1, 0, N'010', N'112010', N'بانک پارسیان', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (117, 106, 1, 1, 0, N'011', N'112011', N'بانک شهر', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (118, 106, 1, 1, 0, N'012', N'112012', N'بانک اقتصاد نوین', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (119, NULL, 1, 1, 0, N'113', N'113', N'سرمايه گذاريهاى كوتاه مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (120, NULL, 1, 1, 0, N'114', N'114', N'اسناد دريافتنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (121, 120, 1, 1, 0, N'001', N'114001', N'اسناد دريافتنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (122, 120, 1, 1, 0, N'002', N'114002', N'اسناد در جريان وصول', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (123, 120, 1, 1, 0, N'003', N'114003', N'اسناد برگشتى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (124, NULL, 1, 1, 0, N'115', N'115', N'حسابهاى دريافتنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (125, 124, 1, 1, 0, N'001', N'115001', N'بدهكاران تجارى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (126, NULL, 1, 1, 0, N'116', N'116', N'ساير حسابها و اسناد دريافتنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (127, 126, 1, 1, 0, N'001', N'116001', N'وام كاركنان', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (128, 126, 1, 1, 0, N'002', N'116002', N'مساعده كاركنان', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (129, 126, 1, 1, 0, N'003', N'116003', N'جارى كاركنان', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (130, NULL, 1, 1, 0, N'117', N'117', N'موجودى مواد و كالا', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (131, NULL, 1, 1, 0, N'118', N'118', N'سپرده ها و پيش پرداختها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (132, 131, 1, 1, 0, N'001', N'118001', N'پيش پرداخت ماليات ارزش افزوده', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (133, 132, 1, 1, 0, N'0001', N'1180010001', N'ماليات ارزش افزوده دريافتنى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (134, 132, 1, 1, 0, N'0002', N'1180010002', N'عوارض ارزش افزوده دريافتنى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (135, NULL, 1, 1, 0, N'119', N'119', N'سفارشات', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (136, NULL, 1, 1, 0, N'151', N'151', N'داراييهاى ثابت مشهود', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (137, 136, 1, 1, 0, N'001', N'151001', N'ساختمانها', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (138, 136, 1, 1, 0, N'002', N'151002', N'ماشين آلات و تاسيسات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (139, 136, 1, 1, 0, N'003', N'151003', N'وسايط نقليه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (140, 136, 1, 1, 0, N'004', N'151004', N'اثاثه و منصوبات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (141, 136, 1, 1, 0, N'005', N'151005', N'تجهيزات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (142, 136, 1, 1, 0, N'006', N'151006', N'اموال انتقالى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (143, 136, 1, 1, 0, N'007', N'151007', N'استهلاك انباشته', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (144, NULL, 1, 1, 0, N'152', N'152', N'سرمايه گذاريهاى بلند مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (145, NULL, 1, 1, 0, N'153', N'153', N'ساير داراييها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (146, NULL, 1, 1, 0, N'211', N'211', N'اسناد پرداختنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (147, 146, 1, 1, 0, N'001', N'211001', N'اسناد پرداختنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (148, NULL, 1, 1, 0, N'212', N'212', N'حسابهاى پرداختنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (149, 148, 1, 1, 0, N'001', N'212001', N'بستانكاران تجارى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (150, NULL, 1, 1, 0, N'213', N'213', N'ساير حسابها و اسناد پرداختنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (151, 150, 1, 1, 0, N'001', N'213001', N'بيمه پرداختنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (152, 151, 1, 1, 0, N'0001', N'2130010001', N'بيمه تامين اجتماعى شعبه ....', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (153, 150, 1, 1, 0, N'002', N'213002', N'ماليات پرداختنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (154, 153, 1, 1, 0, N'0001', N'2130020001', N'ماليات حقوق', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (155, 153, 1, 1, 0, N'0002', N'2130020002', N'ماليات عملكرد', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (156, 153, 1, 1, 0, N'0003', N'2130020003', N'ماليات تكليفى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (157, 153, 1, 1, 0, N'0004', N'2130020004', N'ماليات ارزش افزوده پرداختنى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (158, 153, 1, 1, 0, N'0005', N'2130020005', N'عوارض ارزش افزوده پرداختنى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (159, 150, 1, 1, 0, N'003', N'213003', N'حقوق پرداختنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (160, NULL, 1, 1, 0, N'214', N'214', N'پيش دريافتها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (161, NULL, 1, 1, 0, N'215', N'215', N'ذخيره ماليات', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (162, NULL, 1, 1, 0, N'216', N'216', N'سود سهام پيشنهادى و پرداختنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (163, NULL, 1, 1, 0, N'217', N'217', N'تسهيلات و اعتبارات مالى دريافتى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (164, NULL, 1, 1, 0, N'251', N'251', N'اسناد پرداختنى بلند مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (165, NULL, 1, 1, 0, N'252', N'252', N'حسابهاى پرداختنى بلند مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (166, NULL, 1, 1, 0, N'253', N'253', N'تسهيلات مالى دريافتى بلند مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (167, NULL, 1, 1, 0, N'254', N'254', N'ذخيره مزاياى پايان خدمت كاركنان', 0)
SET IDENTITY_INSERT [Finance].[Account] OFF


SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO
