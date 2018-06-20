USE [TadbirDemo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA [Core]
GO

CREATE SCHEMA [Metadata]
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

CREATE TABLE [Metadata].[Locale] (
    [LocaleID]       INT              IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR(64)      NOT NULL,
    [LocalName]      NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Locale_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Locale_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Locale] PRIMARY KEY CLUSTERED ([LocaleID] ASC)
)
GO

CREATE TABLE [Metadata].[LocalText] (
    [LocalTextID]    INT              IDENTITY (1, 1) NOT NULL,
    [LocaleID]       INT              NOT NULL,
    [ResourceId]     VARCHAR(128)     NOT NULL,
    [Text]           NVARCHAR(1024)   NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_LocalText_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_LocalText_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_LocalText] PRIMARY KEY CLUSTERED ([LocalTextID] ASC)
    , CONSTRAINT [FK_Metadata_LocalText_Metadata_Locale] FOREIGN KEY ([LocaleID]) REFERENCES [Metadata].[Locale] ([LocaleID])
)
GO

CREATE TABLE [Metadata].[Entity] (
    [EntityID]               INT              IDENTITY (1, 1) NOT NULL,
    [Name]                   VARCHAR(64)      NOT NULL,
    [IsHierarchy]            BIT              CONSTRAINT [DF_Metadata_Entity_IsHierarchy] DEFAULT (0) NOT NULL,
    [IsCartableIntegrated]   BIT              CONSTRAINT [DF_Metadata_Entity_IsCartableIntegrated] DEFAULT (1) NOT NULL,
    [rowguid]                UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Entity_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]           DATETIME         CONSTRAINT [DF_Metadata_Entity_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Entity] PRIMARY KEY CLUSTERED ([EntityID] ASC)
)
GO

CREATE TABLE [Metadata].[Property] (
    [PropertyID]       INT              IDENTITY (1, 1) NOT NULL,
    [EntityID]         INT              NOT NULL,
    [Name]             VARCHAR(64)      NOT NULL,
    [DotNetType]       VARCHAR(64)      NOT NULL,
    [StorageType]      VARCHAR(32)      NOT NULL,
    [ScriptType]       VARCHAR(32)      NOT NULL,
    [Length]           INT              CONSTRAINT [DF_Metadata_Property_Length] DEFAULT (0) NOT NULL,
    [MinLength]        INT              CONSTRAINT [DF_Metadata_Property_MinLength] DEFAULT (0) NOT NULL,
    [IsFixedLength]    BIT              CONSTRAINT [DF_Metadata_Property_IsFixedLength] DEFAULT (0) NOT NULL,
    [IsNullable]       BIT              NOT NULL,
    [NameResourceId]   VARCHAR(128)     NOT NULL,
    [AllowSorting]     BIT              CONSTRAINT [DF_Metadata_Property_AllowSorting] DEFAULT (1) NOT NULL,
    [AllowFiltering]   BIT              CONSTRAINT [DF_Metadata_Property_AllowFiltering] DEFAULT (1) NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Property_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Metadata_Property_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Property] PRIMARY KEY CLUSTERED ([PropertyID] ASC)
    , CONSTRAINT [FK_Metadata_Property_Metadata_Entity] FOREIGN KEY ([EntityID]) REFERENCES [Metadata].[Entity]([EntityID])
)
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
    , CONSTRAINT [UK_RolePermission] UNIQUE NONCLUSTERED ([RoleID] ASC, [PermissionID] ASC)
)
GO

CREATE TABLE [Metadata].[Command] (
    [CommandID]      INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]       INT              NULL,
    [PermissionID]   INT              NULL,
    [TitleKey]       NVARCHAR(64)     NOT NULL,
    [RouteUrl]       NVARCHAR(256)    NULL,
    [IconName]       VARCHAR(64)      NULL,
    [HotKey]         VARCHAR(32)      NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Command_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Command_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Command] PRIMARY KEY CLUSTERED ([CommandID] ASC)
    , CONSTRAINT [FK_Metadata_Command_Metadata_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Metadata].[Command]([CommandID])
    , CONSTRAINT [FK_Metadata_Command_Auth_Permission] FOREIGN KEY ([PermissionID]) REFERENCES [Auth].[Permission]([PermissionID])
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
    [EntityNo]            NVARCHAR(64)     NOT NULL,
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
    [ParentID]       INT              NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Corporate_Company_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Corporate_Company_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Corporate_Company] PRIMARY KEY CLUSTERED ([CompanyID] ASC)
    , CONSTRAINT [FK_Corporate_Company_Corporate_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Corporate].[Company]([CompanyID])
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

CREATE TABLE [Auth].[RoleFiscalPeriod] (
    [RoleFiscalPeriodID] INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]             INT              NOT NULL,
    [FiscalPeriodID]     INT              NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RoleFiscalPeriod_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Auth_RoleFiscalPeriod_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RoleFiscalPeriod] PRIMARY KEY CLUSTERED ([RoleFiscalPeriodID] ASC)
    , CONSTRAINT [FK_Auth_RoleFiscalPeriod_Auth_Role] FOREIGN KEY ([RoleID]) REFERENCES [Auth].[Role] ([RoleID])
    , CONSTRAINT [FK_Auth_RoleFiscalPeriod_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
)
GO

CREATE TABLE [Finance].[Account] (
    [AccountID]      INT              IDENTITY (1, 1) NOT NULL,
	[ParentID]       INT              NULL,
	[FiscalPeriodID] INT              NOT NULL,
	[BranchID]       INT              NOT NULL,
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
	[DocumentID]        INT              NOT NULL,
    [No]                NVARCHAR(64)     NOT NULL,
    [Date]              DATETIME         NOT NULL,
    [Description]       NVARCHAR(512)    NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Voucher_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Finance_Voucher_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Voucher] PRIMARY KEY CLUSTERED ([VoucherID] ASC)
    , CONSTRAINT [FK_Finance_Voucher_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Voucher_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
    , CONSTRAINT [FK_Finance_Voucher_Core_Document] FOREIGN KEY ([DocumentID]) REFERENCES [Core].[Document] ([DocumentID])
)
GO

CREATE TABLE [Finance].[DetailAccount] (
    [DetailID]          INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]          INT              NULL,
	[FiscalPeriodID] INT                 NOT NULL,
	[BranchID]       INT                 NOT NULL,
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
    [UomID]                INT              NOT NULL,
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

-- Create system metadata records
SET IDENTITY_INSERT [Metadata].[Locale] ON
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName) VALUES (1, 'English', N'English')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName) VALUES (2, 'Persian', N'فارسی')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName) VALUES (3, 'Arabic', N'العربیه')
INSERT INTO [Metadata].[Locale] (LocaleID, Name, LocalName) VALUES (4, 'French', N'Français')
SET IDENTITY_INSERT [Metadata].[Locale] OFF

SET IDENTITY_INSERT [Metadata].[Entity] ON
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (1, 'Account', 1, 1)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (2, 'Voucher', 0, 1)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (3, 'VoucherLine', 0, 1)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (4, 'User', 0, 0)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (5, 'Role', 0, 0)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (6, 'DetailAccount', 1, 1)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (7, 'CostCenter', 1, 1)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (8, 'Project', 1, 1)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (9, 'FiscalPeriod', 1, 1)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (10, 'Branch', 1, 1)
INSERT INTO [Metadata].[Entity] (EntityID, Name, IsHierarchy, IsCartableIntegrated) VALUES (11, 'Company', 1, 1)
SET IDENTITY_INSERT [Metadata].[Entity] OFF

SET IDENTITY_INSERT [Metadata].[Property] ON
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (1, 1, 'Id', 'System.Int32', 'int', 'number', 0, 0, 0, 'Id_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (2, 1, 'Code', 'System.String', 'nvarchar', 'string', 16, 0, 0, 'Code_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (3, 1, 'FullCode', 'System.String', 'nvarchar', 'string', 256, 0, 0, 'FullCode_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (4, 1, 'Name', 'System.String', 'nvarchar', 'string', 512, 0, 0, 'Name_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (5, 1, 'Level', 'System.Int16', 'smallint', '', 0, 0, 0, 'Level_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (6, 1, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (7, 2, 'Id', 'System.Int32', 'int', 'number', 0, 0, 0, 'Id_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (8, 2, 'No', 'System.String', 'nvarchar', 'string', 64, 0, 0, 'Number_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (9, 2, 'Date', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 'Date_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (10, 2, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (11, 3, 'Id', 'System.Int32', 'int', 'number', 0, 0, 0, 'Id_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (12, 3, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (13, 3, 'Debit', 'System.Decimal', 'money', 'number', 0, 0, 0, 'Debit_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (14, 3, 'Credit', 'System.Decimal', 'money', 'number', 0, 0, 0, 'Credit_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (15, 3, 'CurrencyId', 'System.Int32', 'int', 'number', 0, 0, 0, 'CurrencyId_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (16, 3, 'FullAccount', 'System.Object', '(n/a)', 'object', 0, 0, 0, 'FullAccount_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (17, 3, 'FullAccount.AccountId', 'System.Int32', 'int', 'number', 0, 0, 0, 'AccountId_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (18, 3, 'FullAccount.DetailId', 'System.Int32', 'int', 'number', 0, 0, 1, 'DetailId_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (19, 3, 'FullAccount.CostCenterId', 'System.Int32', 'int', 'number', 0, 0, 1, 'CostCenterId_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (20, 3, 'FullAccount.ProjectId', 'System.Int32', 'int', 'number', 0, 0, 1, 'ProjectId_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (21, 4, 'Id', 'System.Int32', 'int', 'number', 0, 0, 0, 'Id_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (22, 4, 'UserName', 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 'UserName_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], MinLength, IsFixedLength, IsNullable, NameResourceId)
    VALUES (23, 4, 'Password', 'System.String', 'nvarchar(32)', 'string', 32, 4, 0, 0, 'Password_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (24, 4, 'LastLoginDate', 'System.DateTime', 'datetime', 'Date', 0, 0, 1, 'LastLoginDate_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (25, 4, 'IsEnabled', 'System.Boolean', 'bit', 'boolean', 0, 0, 0, 'Status_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (26, 4, 'PersonFirstName', 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 'FirstName_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (27, 4, 'PersonLastName', 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 'LastName_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (28, 5, 'Name', 'System.String', 'nvarchar(64)', 'string', 64, 0, 0, 'Name_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (29, 5, 'Description', 'System.String', 'nvarchar(512)', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (30, 6, 'Id', 'System.Int32', 'int', 'number', 0, 0, 0, 'Id_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (31, 6, 'Code', 'System.String', 'nvarchar', 'string', 16, 0, 0, 'Code_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (32, 6, 'FullCode', 'System.String', 'nvarchar', 'string', 256, 0, 0, 'FullCode_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (33, 6, 'Name', 'System.String', 'nvarchar', 'string', 512, 0, 0, 'Name_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (34, 6, 'Level', 'System.Int16', 'smallint', '', 0, 0, 0, 'Level_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (35, 6, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (36, 7, 'Id', 'System.Int32', 'int', 'number', 0, 0, 0, 'Id_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (37, 7, 'Code', 'System.String', 'nvarchar', 'string', 16, 0, 0, 'Code_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (38, 7, 'FullCode', 'System.String', 'nvarchar', 'string', 256, 0, 0, 'FullCode_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (39, 7, 'Name', 'System.String', 'nvarchar', 'string', 512, 0, 0, 'Name_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (40, 7, 'Level', 'System.Int16', 'smallint', '', 0, 0, 0, 'Level_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (41, 7, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (42, 8, 'Id', 'System.Int32', 'int', 'number', 0, 0, 0, 'Id_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (43, 8, 'Code', 'System.String', 'nvarchar', 'string', 16, 0, 0, 'Code_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (44, 8, 'FullCode', 'System.String', 'nvarchar', 'string', 256, 0, 0, 'FullCode_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (45, 8, 'Name', 'System.String', 'nvarchar', 'string', 512, 0, 0, 'Name_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (46, 8, 'Level', 'System.Int16', 'smallint', '', 0, 0, 0, 'Level_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (47, 8, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (48, 9, 'Id', 'System.Int32', 'int', 'number', 0, 0, 0, 'Id_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (49, 9, 'Name', 'System.String', 'nvarchar', 'string', 64, 0, 0, 'Name_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (50, 9, 'StartDate', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 'StartDate_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (51, 9, 'EndDate', 'System.DateTime', 'datetime', 'Date', 0, 0, 0, 'EndDate_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (52, 9, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (53, 10, 'Name', 'System.String', 'nvarchar', 'string', 128, 0, 0, 'Name_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (54, 10, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (55, 11, 'Name', 'System.String', 'nvarchar', 'string', 128, 0, 0, 'Name_Field')
INSERT INTO [Metadata].[Property] (PropertyID, EntityID, Name, DotNetType, StorageType, ScriptType, [Length], IsFixedLength, IsNullable, NameResourceId)
    VALUES (56, 11, 'Description', 'System.String', 'nvarchar', 'string', 512, 0, 1, 'Description_Field')
SET IDENTITY_INSERT [Metadata].[Property] OFF

SET IDENTITY_INSERT [Metadata].[LocalText] ON
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (1, 1, 'Id_Field', N'Id')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (2, 1, 'Name_Field', N'Name')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (3, 1, 'Description_Field', N'Description')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (4, 1, 'Code_Field', N'Code')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (5, 1, 'FullCode_Field', N'Full code')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (6, 1, 'Level_Field', N'Level')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (7, 1, 'Number_Field', N'Number')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (8, 1, 'Date_Field', N'Date')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (9, 1, 'Debit_Field', N'Debit')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (10, 1, 'Credit_Field', N'Credit')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (11, 2, 'Id_Field', N'شناسه')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (12, 2, 'Name_Field', N'نام')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (13, 2, 'Description_Field', N'شرح')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (14, 2, 'Code_Field', N'کد')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (15, 2, 'FullCode_Field', N'کد کامل')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (16, 2, 'Level_Field', N'شماره سطح')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (17, 2, 'Number_Field', N'شماره')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (18, 2, 'Date_Field', N'تاریخ')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (19, 2, 'Debit_Field', N'بدهکار')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (20, 2, 'Credit_Field', N'بستانکار')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (21, 1, 'UserName_Field', N'User Name')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (22, 1, 'Password_Field', N'Password')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (23, 1, 'LastLoginDate_Field', N'Last Login Date')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (24, 1, 'Status_Field', N'Status')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (25, 1, 'FirstName_Field', N'First Name')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (26, 1, 'LastName_Field', N'Last Name')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (27, 2, 'UserName_Field', N'نام کاربری')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (28, 2, 'Password_Field', N'رمز ورود')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (29, 2, 'LastLoginDate_Field', N'تاریخ آخرین ورود')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (30, 2, 'Status_Field', N'وضعیت')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (31, 2, 'FirstName_Field', N'نام')
INSERT INTO [Metadata].[LocalText] (LocalTextID, LocaleID, ResourceId, [Text]) VALUES (32, 2, 'LastName_Field', N'نام خانوادگی')
SET IDENTITY_INSERT [Metadata].[LocalText] OFF


-- Create system records for security (NOTE: These records will be migrated to SYS database in a later stage)

-- admin user is added with password 'Admin@Tadbir1395'
SET IDENTITY_INSERT [Auth].[User] ON
INSERT INTO [Auth].[User] (UserID, UserName, PasswordHash, IsEnabled) VALUES (1, N'admin', '5ab4a25e31220c3b103aef3e32596211b90238a0d5933288efbd36c5154b82ff', 1)
SET IDENTITY_INSERT [Auth].[User] OFF

SET IDENTITY_INSERT [Contact].[Person] ON
INSERT INTO [Contact].[Person] (PersonID, UserID, FirstName, LastName) VALUES (1, 1, N'راهبر', N'سیستم')
SET IDENTITY_INSERT [Contact].[Person] OFF

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
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (1, N'ManageEntities,Accounts', N'Account')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (2, N'ManageEntities,DetailAccounts', N'DetailAccount')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (3, N'ManageEntities,CostCenters', N'CostCenter')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (4, N'ManageEntities,Projects', N'Project')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (5, N'ManageEntities,FiscalPeriods', N'FiscalPeriod')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (6, N'ManageEntities,Currencies', N'Currency')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (7, N'ManageEntities,Vouchers', N'Voucher')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (8, N'ManageEntities,BizUnits', N'BusinessUnit')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (9, N'ManageEntities,BizPartners', N'BusinessPartner')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (10, N'ManageEntities,Users', N'User')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (11, N'ManageEntities,Roles', N'Role')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (12, N'ManageEntities,ReqVouchers', N'RequisitionVoucher')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (13, N'ManageEntities,IRVouchers', N'IssueReceiptVoucher')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (14, N'ManageEntities,SalesInvoices', N'SalesInvoice')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (15, N'ManageEntities,Inventories', N'ProductInventory')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (16, N'ManageEntities,Branches', N'Branch')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (17, N'AccountRelations', N'AccountRelations')
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (18, N'ManageEntities,Companies', N'Company')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (1, 1, N'ViewEntities,Accounts', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (2, 1, N'CreateEntity,Account', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (3, 1, N'EditEntity,Account', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (4, 1, N'DeleteEntity,Account', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (5, 2, N'ViewEntities,DetailAccounts', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (6, 2, N'CreateEntity,DetailAccount', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (7, 2, N'EditEntity,DetailAccount', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (8, 2, N'DeleteEntity,DetailAccount', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (9, 3, N'ViewEntities,CostCenters', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (10, 3, N'CreateEntity,CostCenter', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (11, 3, N'EditEntity,CostCenter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (12, 3, N'DeleteEntity,CostCenter', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (13, 4, N'ViewEntities,Projects', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (14, 4, N'CreateEntity,Project', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (15, 4, N'EditEntity,Project', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (16, 4, N'DeleteEntity,Project', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (17, 5, N'ViewEntities,FiscalPeriods', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (18, 5, N'CreateEntity,FiscalPeriod', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (19, 5, N'EditEntity,FiscalPeriod', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (20, 5, N'DeleteEntity,FiscalPeriod', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (21, 6, N'ViewEntities,Currencies', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (22, 6, N'CreateEntity,Currency', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (23, 6, N'EditEntity,Currency', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (24, 6, N'DeleteEntity,Currency', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (25, 7, N'ViewEntities,Vouchers', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (26, 7, N'CreateEntity,Voucher', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (27, 7, N'EditEntity,Voucher', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (28, 7, N'DeleteEntity,Voucher', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (29, 7, N'PrepareEntity,Voucher', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (30, 7, N'ReviewEntity,Voucher', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (31, 7, N'ConfirmEntity,Voucher', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (32, 7, N'ApproveEntity,Voucher', 128)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (33, 8, N'ViewEntities,BizUnits', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (34, 8, N'CreateEntity,BizUnit', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (35, 8, N'EditEntity,BizUnit', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (36, 8, N'DeleteEntity,BizUnit', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (37, 9, N'ViewEntities,BizPartners', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (38, 9, N'CreateEntity,BizPartner', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (39, 9, N'EditEntity,BizPartner', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (40, 9, N'DeleteEntity,BizPartner', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (41, 10, N'ViewEntities,Users', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (42, 10, N'CreateEntity,User', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (43, 10, N'EditEntity,User', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (44, 11, N'ViewEntities,Roles', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (45, 11, N'CreateEntity,Role', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (46, 11, N'EditEntity,Role', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (47, 11, N'DeleteEntity,Role', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (48, 11, N'AssignEntityToRole,User', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (49, 11, N'AssignEntityToRole,Branch', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (50, 12, N'ViewEntities,ReqVouchers', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (51, 12, N'CreateEntity,ReqVoucher', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (52, 12, N'EditEntity,ReqVoucher', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (53, 12, N'DeleteEntity,ReqVoucher', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (54, 12, N'PrepareEntity,ReqVoucher', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (55, 12, N'ConfirmEntity,ReqVoucher', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (56, 12, N'ApproveEntity,ReqVoucher', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (57, 13, N'ExportEntityToIssueVoucher,ReqVoucher', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (58, 13, N'SaveEntity,IssueVoucher', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (59, 13, N'RejectReviewEntity,IssueVoucher', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (60, 13, N'ConfirmEntity,IssueVoucher', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (61, 13, N'ApproveEntity,IssueVoucher', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (62, 13, N'MonetizeEntity,ValuedIssueVoucher', 32)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (63, 14, N'ExportEntityToSalesInvoice,IssueVoucher', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (64, 14, N'SaveEntity,SalesInvoice', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (65, 14, N'RejectReviewEntity,SalesInvoice', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (66, 14, N'ConfirmEntity,SalesInvoice', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (67, 14, N'ApproveEntity,SalesInvoice', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (68, 14, N'RegisterEntity,SalesInvoice', 256)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (69, 14, N'RegisterEntity,MonetizedIssueVoucher', 512)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (70, 15, N'ViewEntities,Inventories', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (71, 15, N'CreateEntity,Inventory', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (72, 15, N'EditEntity,Inventory', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (73, 15, N'DeleteEntity,Inventory', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (74, 16, N'ViewEntities,Branches', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (75, 16, N'CreateEntity,Branch', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (76, 16, N'EditEntity,Branch', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (77, 16, N'DeleteEntity,Branch', 8)

INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (78, 16, N'ViewEntities,AccountRelations', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (79, 16, N'ManageEntities,AccountRelations', 2)

INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (80, 18, N'ViewEntities,Companies', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (81, 18, N'CreateEntity,Company', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (82, 18, N'EditEntity,Company', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (83, 18, N'DeleteEntity,Company', 8)

INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (84, 11, N'AssignEntityToRole,FiscalPeriod', 64)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (85, 10, N'AssignRolesToEntity,User', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (86, 16, N'AssignRolesToEntity,Branch', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (87, 5, N'AssignRolesToEntity,FiscalPeriod', 16)
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
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (23, 2, 25)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (24, 2, 26)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (25, 2, 27)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (26, 2, 28)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (27, 2, 29)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (28, 3, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (29, 3, 25)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (30, 3, 26)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (31, 3, 27)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (32, 3, 28)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (33, 3, 30)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (34, 4, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (35, 4, 25)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (36, 4, 31)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (37, 5, 1)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (38, 5, 25)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (39, 5, 32)
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
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (64, 1, 46)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (65, 1, 47)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (66, 1, 48)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (67, 1, 49)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (68, 1, 50)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (69, 1, 51)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (70, 1, 52)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (71, 1, 53)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (72, 1, 54)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (73, 1, 55)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (74, 1, 56)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (75, 1, 57)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (76, 1, 58)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (77, 1, 59)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (78, 1, 60)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (79, 1, 61)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (80, 1, 62)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (81, 1, 63)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (82, 1, 64)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (83, 1, 65)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (84, 1, 66)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (85, 1, 67)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (86, 1, 68)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (87, 1, 69)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (88, 1, 70)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (89, 1, 71)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (90, 1, 72)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (91, 1, 73)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (92, 1, 74)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (93, 1, 75)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (94, 1, 76)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (95, 1, 77)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (96, 1, 78)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (97, 1, 79)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (98, 1, 80)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (99, 1, 81)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (100, 1, 82)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (101, 1, 83)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (102, 1, 84)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (103, 1, 85)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (104, 1, 86)
INSERT INTO [Auth].[RolePermission] (RolePermissionID, RoleID, PermissionID) VALUES (105, 1, 87)

SET IDENTITY_INSERT [Auth].[RolePermission] OFF

SET IDENTITY_INSERT [Metadata].[Command] ON
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (1, NULL, NULL, N'Accounting', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (2, 1, 17, N'FiscalPeriods', N'/fiscalperiod', 'list', 'Ctrl+Shift+F')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (3, 1, 1, N'Accounts', N'/account', 'tasks', 'Ctrl+Shift+A')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (4, 1, 5, N'DetailAccounts', N'/detailAccount', 'list', 'Ctrl+Shift+D')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (5, 1, 9, N'CostCenters', N'/costCenter', 'list', 'Ctrl+Shift+C')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (6, 1, 13, N'Projects', N'/projects', 'list', 'Ctrl+Shift+P')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (7, 1, 78, N'AccountRelations', N'/accountrelations', 'list', 'Ctrl+Shift+R')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (8, 1, 25, N'Vouchers', N'/voucher', 'list', 'Ctrl+Shift+V')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (9, NULL, NULL, N'Organization', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (10, 9, 80, N'Companies', N'/companies', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (11, 9, 74, N'Branches', N'/branches', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (12, NULL, NULL, N'Administration', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (13, 12, 41, N'Users', N'/users', 'user', 'Ctrl+Shift+U')
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (14, 12, 44, N'Roles', N'/roles', 'list', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (15, NULL, NULL, N'Profile', NULL, NULL, NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (16, 15, NULL, N'ChangePassword', N'/changePassword', 'eye-open', NULL)
INSERT INTO [Metadata].[Command] (CommandID, ParentID, PermissionID, TitleKey, RouteUrl, IconName, HotKey) VALUES (17, 15, NULL, N'LogOut', N'/logout', 'log-out', 'Ctrl+Shift+X')
SET IDENTITY_INSERT [Metadata].[Command] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

