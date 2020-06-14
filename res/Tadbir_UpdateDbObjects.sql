-- 1.1.619
UPDATE [Core].[DocumentStatus]
SET Name = N'Finalized'
WHERE StatusID = 3

DELETE FROM [Core].[DocumentStatus]
WHERE StatusID > 3

-- 1.1.655
ALTER TABLE [Finance].[Currency]
ADD [Country] NVARCHAR(64) NOT NULL
CONSTRAINT DF_Finance_Currency_Country DEFAULT N'ToBeAdded'
WITH VALUES;
GO

ALTER TABLE [Finance].[Currency]
ADD [Code] NVARCHAR(8) NOT NULL
CONSTRAINT DF_Finance_Currency_Code DEFAULT N'XYZ'
WITH VALUES;
GO

ALTER TABLE [Finance].[Currency]
ADD [MinorUnit] NVARCHAR(16) NOT NULL
CONSTRAINT DF_Finance_Currency_MinorUnit DEFAULT N'Cent'
WITH VALUES;
GO

ALTER TABLE [Finance].[Currency]
ADD [Multiplier] INT NOT NULL
CONSTRAINT DF_Finance_Currency_Multiplier DEFAULT 100
WITH VALUES;
GO

ALTER TABLE [Finance].[Currency]
ADD [DecimalCount] SMALLINT NOT NULL
CONSTRAINT DF_Finance_Currency_DecimalCount DEFAULT 2
WITH VALUES;
GO

ALTER TABLE [Finance].[Currency]
ADD [Description] NVARCHAR(512) NULL
GO


CREATE TABLE [Finance].[CurrencyRate] (
    [CurrencyRateID]   INT              IDENTITY (1, 1) NOT NULL,
    [CurrencyID]       INT              NOT NULL,
    [Date]             DATETIME         NOT NULL,
    [Time]             TIME(7)          NOT NULL,
    [Multiplier]       FLOAT            NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_CurrencyRate_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_CurrencyRate_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_CurrencyRate] PRIMARY KEY CLUSTERED ([CurrencyRateID] ASC)
    , CONSTRAINT [FK_Finance_CurrencyRate_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
)
GO

-- 1.1.664
ALTER TABLE [Finance].[Currency]
ADD [BranchID] INT NOT NULL
CONSTRAINT [DF_Finance_Currency_BranchID] DEFAULT (1)
WITH VALUES;
GO

ALTER TABLE [Finance].[Currency]
ADD [BranchScope] SMALLINT NOT NULL
CONSTRAINT [DF_Finance_Currency_BranchScope] DEFAULT (0)
WITH VALUES;
GO

ALTER TABLE [Finance].[Currency]
ADD CONSTRAINT [FK_Finance_Currency_Corporate_Branch] FOREIGN KEY ([BranchID])
    REFERENCES [Corporate].[Branch]([BranchID]);
GO

-- 1.1.666
ALTER TABLE [Finance].[Currency]
ADD [FiscalPeriodID] INT NOT NULL
CONSTRAINT DF_Finance_Currency_FiscalPeriodID DEFAULT 0
WITH VALUES;
GO

ALTER TABLE [Finance].[Currency]
ADD [IsActive] BIT NOT NULL
CONSTRAINT DF_Finance_Currency_IsActive DEFAULT 1
WITH VALUES;
GO

-- 1.1.676
-- NOTE: This update requires suggested coding and may not be valid for default company database
UPDATE [Finance].[Account]
SET GroupID = 1
WHERE ParentID IS NULL AND AccountID >= 101 AND AccountID <= 135

UPDATE [Finance].[Account]
SET GroupID = 2
WHERE ParentID IS NULL AND AccountID >= 136 AND AccountID <= 145

UPDATE [Finance].[Account]
SET GroupID = 3
WHERE ParentID IS NULL AND AccountID >= 146 AND AccountID <= 163

UPDATE [Finance].[Account]
SET GroupID = 4
WHERE ParentID IS NULL AND AccountID >= 164 AND AccountID <= 167

UPDATE [Finance].[Account]
SET GroupID = 5
WHERE ParentID IS NULL AND AccountID >= 168 AND AccountID <= 172

UPDATE [Finance].[Account]
SET GroupID = 6
WHERE ParentID IS NULL AND AccountID >= 173 AND AccountID <= 177

UPDATE [Finance].[Account]
SET GroupID = 7
WHERE ParentID IS NULL AND AccountID >= 178 AND AccountID <= 181

UPDATE [Finance].[Account]
SET GroupID = 8
WHERE AccountID = 182

UPDATE [Finance].[Account]
SET GroupID = 10
WHERE ParentID IS NULL AND AccountID >= 186 AND AccountID <= 212

UPDATE [Finance].[Account]
SET GroupID = 11
WHERE ParentID IS NULL AND AccountID >= 213 AND AccountID <= 216

UPDATE [Finance].[Account]
SET GroupID = 12
WHERE ParentID IS NULL AND AccountID >= 217 AND AccountID <= 220

UPDATE [Finance].[Account]
SET GroupID = 13
WHERE ParentID IS NULL AND AccountID >= 221 AND AccountID <= 222

SET IDENTITY_INSERT [Finance].[AccountCollectionAccount] ON
-- Add bank accounts
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (1, 17, 106, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (2, 17, 107, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (3, 17, 108, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (4, 17, 109, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (5, 17, 110, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (6, 17, 111, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (7, 17, 112, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (8, 17, 113, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (9, 17, 114, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (10, 17, 115, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (11, 17, 116, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (12, 17, 117, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (13, 17, 118, 1, 1)

-- Add cashier accounts
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (14, 16, 102, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (15, 16, 103, 1, 1)

-- Add liquid asset accounts
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (16, 1, 101, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (17, 1, 106, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (18, 1, 119, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (19, 1, 120, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (20, 1, 124, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (21, 1, 126, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (22, 1, 130, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (23, 1, 131, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (24, 1, 135, 1, 1)

-- Add liquid liability accounts
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (25, 3, 146, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (26, 3, 148, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (27, 3, 150, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (28, 3, 160, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (29, 3, 161, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (30, 3, 162, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([AccountCollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (31, 3, 163, 1, 1)

SET IDENTITY_INSERT [Finance].[AccountCollectionAccount] OFF

-- 1.1.679
ALTER TABLE [Finance].[CurrencyRate]
ADD [BranchID] INT NOT NULL
CONSTRAINT [DF_Finance_CurrencyRate_BranchID] DEFAULT (1)
WITH VALUES;
GO

ALTER TABLE [Finance].[CurrencyRate]
ADD [BranchScope] SMALLINT NOT NULL
CONSTRAINT [DF_Finance_CurrencyRate_BranchScope] DEFAULT (0)
WITH VALUES;
GO

ALTER TABLE [Finance].[CurrencyRate]
ADD CONSTRAINT [FK_Finance_CurrencyRate_Corporate_Branch] FOREIGN KEY ([BranchID])
    REFERENCES [Corporate].[Branch]([BranchID]);
GO

ALTER TABLE [Finance].[DetailAccount]
ADD [CurrencyID] INT NULL
GO

ALTER TABLE [Finance].[DetailAccount]
ADD CONSTRAINT [FK_Finance_DetailAccount_Finance_Currency] FOREIGN KEY ([CurrencyID])
    REFERENCES [Finance].[Currency]([CurrencyID]);
GO

-- 1.1.688
ALTER TABLE [Finance].[CurrencyRate]
ADD [Description] NVARCHAR(512) NULL
GO

-- 1.1.692
ALTER TABLE [Finance].[Currency]
ADD [TaxCode] INT NOT NULL
CONSTRAINT [DF_Finance_Currency_TaxCode] DEFAULT (0)
WITH VALUES;
GO

-- 1.1.699
CREATE TABLE [Finance].[TaxCurrency] (
    [TaxCurrencyID]   INT              IDENTITY (1, 1) NOT NULL,
    [Code]            INT              NOT NULL,
    [Name]            NVARCHAR(64)     NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_TaxCurrency_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_TaxCurrency_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_TaxCurrency] PRIMARY KEY CLUSTERED ([TaxCurrencyID] ASC)
)
GO

-- 1.1.702
ALTER TABLE [Finance].[Account]
	DROP CONSTRAINT [FK_Finance_Account_Finance_Currency]
GO
ALTER TABLE [Finance].[Account]
	DROP CONSTRAINT [DF_Finance_Account_CurrencyID]
GO
ALTER TABLE [Finance].[Account]
	DROP COLUMN [CurrencyID]
GO

CREATE TABLE [Finance].[AccountCurrency](
	[AccountCurrencyID]    INT              IDENTITY(1,1) NOT NULL,
	[AccountID]            INT              NOT NULL,
	[CurrencyID]           INT              NOT NULL,
	[BranchID]             INT              NOT NULL,
	[RowGuid]              UNIQUEIDENTIFIER CONSTRAINT [DF_AccountCurrency_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
	[ModifiedDate]         DATETIME CONSTRAINT [DF_AccountCurrency_ModifiedDate] DEFAULT (getdate()) NOT NULL
, CONSTRAINT [PK_AccountCurrency] PRIMARY KEY CLUSTERED ([AccountCurrencyID] ASC)
, CONSTRAINT [FK_Finance_AccountCurrency_Corporate_Branch] FOREIGN KEY([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
, CONSTRAINT [FK_Finance_AccountCurrency_Finance_Account] FOREIGN KEY([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
, CONSTRAINT [FK_Finance_AccountCurrency_Finance_Currency] FOREIGN KEY([CurrencyID]) REFERENCES [Finance].[Currency] ([CurrencyID])
)
GO

-- 1.1.730
ALTER TABLE Finance.Currency ADD
	IsDefaultCurrency bit NOT NULL  DEFAULT 0
GO

-- 1.1.737
DELETE FROM [Auth].[RoleBranch]
WHERE RoleID = 1

DELETE FROM [Auth].[RoleFiscalPeriod]
WHERE RoleID = 1

-- 1.1.742
CREATE TABLE [Config].[Setting] (
    [SettingID]      INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]       INT              NULL,
    [Subsystem]      VARCHAR(32)      NULL,
    [TitleKey]       VARCHAR(128)     NOT NULL,
    [Type]           SMALLINT         NOT NULL,
    [ScopeType]      SMALLINT         NOT NULL,
    [ModelType]      VARCHAR(128)     NOT NULL,
	[IsStandalone]   BIT              NOT NULL,
    [Values]         NVARCHAR(2048)   NOT NULL,
    [DefaultValues]  NVARCHAR(2048)   NOT NULL,
    [DescriptionKey] VARCHAR(1028)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_Setting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_Setting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_Setting] PRIMARY KEY CLUSTERED ([SettingID] ASC)
    , CONSTRAINT [FK_Config_Setting_Config_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Config].[Setting]([SettingID])
)
GO

CREATE TABLE [Config].[UserSetting] (
    [UserSettingID]  INT              IDENTITY (1, 1) NOT NULL,
    [SettingID]      INT              NOT NULL,
    [ViewID]         INT              NULL,
    [UserID]         INT              NULL,
    [RoleID]         INT              NULL,
    [ModelType]      VARCHAR(128)     NOT NULL,
    [Values]         NTEXT            NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_UserSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_UserSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_UserSetting] PRIMARY KEY CLUSTERED ([UserSettingID] ASC)
    , CONSTRAINT [FK_Config_UserSetting_Config_Setting] FOREIGN KEY ([SettingID]) REFERENCES [Config].[Setting]([SettingID])
)
GO

SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (1, 'AccountRelationsSettings', 2, 1, 'RelationsConfig', N'{"useLeafDetails": true, "useLeafCostCenters": true,"useLeafProjects": true}', N'{"useLeafDetails": true, "useLeafCostCenters": true,"useLeafProjects": true}', 'AccountRelationsSettingsDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (2, 'DateRangeFilterSettings', 2, 0, 'DateRangeConfig', N'{"defaultDateRange": "FiscalStartToFiscalEnd"}', N'{"defaultDateRange": "FiscalStartToFiscalEnd"}', 'DateRangeFilterSettingsDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (3, 'NumberCurrencySettings', 2, 0, 'NumberDisplayConfig', N'{"useSeparator": true, "separatorMode": "UseCustom", "separatorSymbol": ",", "decimalPrecision": 0, "maxPrecision": 8}', N'{"useSeparator": true, "separatorMode": "UseCustom", "separatorSymbol": ",", "decimalPrecision": 0, "maxPrecision": 8}', 'NumberCurrencySettingsDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (5, 'ViewTreeSettings', 2, 2, 'ViewTreeConfig', N'{}', N'{}', 'ViewTreeSettingsDescription', 0)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (6, 'QuickSearchSettings', 3, 2, 'QuickSearchConfig', N'{}', N'{}', 'QuickSearchSettingsDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (9, 'TestBalanceSettings', 2, 1, 'TestBalanceConfig', N'{"addOpeningVoucherToInitBalance": false}', N'{"addOpeningVoucherToInitBalance": false}', 'TestBalanceSettingsDescription', 1)
SET IDENTITY_INSERT [Config].[Setting] OFF

ALTER TABLE [Config].[ViewSetting]
ADD CONSTRAINT [FK_Config_ViewSetting_Config_Setting] FOREIGN KEY ([SettingID])
    REFERENCES [Config].[Setting]([SettingID]);
GO

--1.1.751
UPDATE [Finance].[Voucher]
SET Reference = NULL
WHERE Reference = ''

-- 1.1.762
SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
VALUES (8, 'SystemConfigurationSettings', 2, 1, 'SystemConfig', N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":2,"defaultCalendar":0,"usesDefaultCoding":true}', N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":2,"defaultCalendar":0,"usesDefaultCoding":true}', 'SystemConfigurationDescription', 1)
SET IDENTITY_INSERT [Config].[Setting] OFF

-- 1.1.772
CREATE TABLE [Core].[Filter] (
    [FilterID]       INT              IDENTITY (1, 1) NOT NULL,
    [ViewId]         INT              NOT NULL,
    [UserId]         INT              NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [IsPublic]       BIT              NOT NULL,
    [Values]         NVARCHAR(2048)   NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Core_Filter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Core_Filter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_Filter] PRIMARY KEY CLUSTERED ([FilterID] ASC)
)
GO

-- 1.1.776
CREATE SCHEMA [Metadata]
GO

CREATE TABLE [Metadata].[EntityType] (
    [EntityTypeID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_EntityType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_EntityType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_EntityType] PRIMARY KEY CLUSTERED ([EntityTypeID] ASC)
)
GO

CREATE TABLE [Metadata].[Operation] (
    [OperationID]    INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Operation_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Operation_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Operation] PRIMARY KEY CLUSTERED ([OperationID] ASC)
)
GO

CREATE TABLE [Metadata].[OperationSource] (
    [OperationSourceID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR(128)    NOT NULL,
    [Description]         NVARCHAR(512)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_OperationSource_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Metadata_OperationSource_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_OperationSource] PRIMARY KEY CLUSTERED ([OperationSourceID] ASC)
)
GO

CREATE TABLE [Core].[OperationLog] (
    [OperationLogID]   INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]         INT              NOT NULL,
    [FiscalPeriodID]   INT              NOT NULL,
    [OperationID]      INT              NOT NULL,
    [SourceID]         INT              NOT NULL,
    [EntityTypeID]     INT              NOT NULL,
    [Date]             DATETIME         NOT NULL,
    [Time]             TIME(7)          NOT NULL,
    [UserId]           INT              NOT NULL,
    [CompanyId]        INT              NOT NULL,
    [SourceListId]     INT              NULL,
    [EntityId]         INT              NULL,
    [Description]      NVARCHAR(MAX)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Core_OperationLog_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Core_OperationLog_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_OperationLog] PRIMARY KEY CLUSTERED ([OperationLogID] ASC)
    , CONSTRAINT [FK_Core_OperationLog_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Core_OperationLog_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Core_OperationLog_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_OperationLog_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_OperationLog_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
)
GO

-- 1.1.777
SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (1, N'Account')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (2, N'AccountCollectionAccount')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (3, N'AccountRelations')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (4, N'AccountGroup')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (5, N'Branch')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (6, N'CostCenter')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (7, N'Currency')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (8, N'CurrencyRate')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (9, N'DetailAccount')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (10, N'FiscalPeriod')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (11, N'OperationLog')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (12, N'Project')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (13, N'RoleBranch')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (14, N'RoleFiscalPeriod')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (15, N'Setting')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (16, N'TaxCurrency')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (17, N'Voucher')
INSERT INTO [Metadata].[EntityType] ([EntityTypeID],[Name]) VALUES (18, N'VoucherLine')
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (1, N'View')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (2, N'Create')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (3, N'Edit')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (4, N'Delete')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (5, N'Filter')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (6, N'Print')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (7, N'Save')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (8, N'Archive')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (9, N'SetDefault')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (10, N'Design')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (11, N'Check')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (12, N'UndoCheck')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (13, N'Confirm')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (14, N'UndoConfirm')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (15, N'Approve')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (16, N'UndoApprove')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (17, N'Finalize')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (18, N'UndoFinalize')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (19, N'Mark')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (20, N'QuickReportDesign')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (1, N'Journal')
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (2, N'AccountBook')
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (3, N'CurrencyBook')
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (4, N'TestBalance')
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (5, N'ItemBalance')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

-- 1.1.781
CREATE TABLE [Metadata].[OperationSourceList] (
    [OperationSourceListID] INT              IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR(128)    NOT NULL,
    [Description]           NVARCHAR(512)    NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_OperationSourceList_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Metadata_OperationSourceList_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_OperationSourceList] PRIMARY KEY CLUSTERED ([OperationSourceListID] ASC)
)
GO

SET IDENTITY_INSERT [Metadata].[OperationSourceList] ON
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (1, N'JournalByDateByRow')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (2, N'JournalByDateByRowDetail')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (3, N'JournalByDateByLedger')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (4, N'JournalByDateBySubsidiary')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (5, N'JournalByDateSummary')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (6, N'JournalByDateSummaryByDate')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (7, N'JournalByDateSummaryByMonth')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (8, N'JournalByNoByRow')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (9, N'JournalByNoByRowDetail')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (10, N'JournalByNoByLedger')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (11, N'JournalByNoBySubsidiary')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (12, N'JournalByNoSummary')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (13, N'AccountBookByRow')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (14, N'AccountBookVoucherSum')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (15, N'AccountBookDailySum')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (16, N'AccountBookMonthlySum')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (17, N'CurrencyBookByRow')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (18, N'CurrencyBookVoucherSum')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (19, N'CurrencyBookDailySum')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (20, N'CurrencyBookMonthlySum')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (21, N'CurrencyBookAllCurrencies')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (22, N'TestBalance2Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (23, N'TestBalance4Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (24, N'TestBalance6Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (25, N'TestBalance8Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (26, N'TestBalance10Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (27, N'DetailAccountBalance2Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (28, N'DetailAccountBalance4Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (29, N'DetailAccountBalance6Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (30, N'DetailAccountBalance8Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (31, N'DetailAccountBalance10Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (32, N'CostCenterBalance2Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (33, N'CostCenterBalance4Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (34, N'CostCenterBalance6Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (35, N'CostCenterBalance8Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (36, N'CostCenterBalance10Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (37, N'ProjectBalance2Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (38, N'ProjectBalance4Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (39, N'ProjectBalance6Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (40, N'ProjectBalance8Column')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (41, N'ProjectBalance10Column')
SET IDENTITY_INSERT [Metadata].[OperationSourceList] OFF

-- 1.1.785
CREATE TABLE [Metadata].[Subsystem] (
    [SubsystemID]    INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Subsystem_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Subsystem_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Subsystem] PRIMARY KEY CLUSTERED ([SubsystemID] ASC)
)
GO

CREATE TABLE [Metadata].[OperationSourceType] (
    [OperationSourceTypeID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                    NVARCHAR(64)     NOT NULL,
    [rowguid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_OperationSourceType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]            DATETIME         CONSTRAINT [DF_Metadata_OperationSourceType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_OperationSourceType] PRIMARY KEY CLUSTERED ([OperationSourceTypeID] ASC)
)
GO

CREATE TABLE [Config].[LogSetting] (
    [LogSettingID]   INT              IDENTITY (1, 1) NOT NULL,
    [SubsystemID]    INT              NOT NULL,
    [SourceTypeID]   INT              NOT NULL,
    [SourceID]       INT              NULL,
    [EntityTypeID]   INT              NULL,
    [OperationID]    INT              NOT NULL,
    [IsEnabled]      BIT              NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_LogSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_LogSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_LogSetting] PRIMARY KEY CLUSTERED ([LogSettingID] ASC)
    , CONSTRAINT [FK_Config_LogSetting_Metadata_Subsystem] FOREIGN KEY ([SubsystemID]) REFERENCES [Metadata].[Subsystem]([SubsystemID])
    , CONSTRAINT [FK_Config_LogSetting_Metadata_SourceType] FOREIGN KEY ([SourceTypeID]) REFERENCES [Metadata].[OperationSourceType]([OperationSourceTypeID])
    , CONSTRAINT [FK_Config_LogSetting_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Config_LogSetting_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
    , CONSTRAINT [FK_Config_LogSetting_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
)
GO

SET IDENTITY_INSERT [Metadata].[Subsystem] ON
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (1, N'Accounting')
SET IDENTITY_INSERT [Metadata].[Subsystem] OFF

SET IDENTITY_INSERT [Metadata].[OperationSourceType] ON
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (1, N'BaseData')
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (2, N'OperationalForms')
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (3, N'Reports')
SET IDENTITY_INSERT [Metadata].[OperationSourceType] OFF

-- 1.1.786
SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (1, 1, 1, NULL, 1, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (2, 1, 1, NULL, 1, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (3, 1, 1, NULL, 1, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (4, 1, 1, NULL, 1, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (5, 1, 1, NULL, 1, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (6, 1, 3, 2, NULL, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (7, 1, 3, 2, NULL, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (8, 1, 1, NULL, 2, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (9, 1, 1, NULL, 2, 7, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (10, 1, 1, NULL, 4, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (11, 1, 1, NULL, 4, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (12, 1, 1, NULL, 4, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (13, 1, 1, NULL, 4, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (14, 1, 1, NULL, 4, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (15, 1, 1, NULL, 3, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (16, 1, 1, NULL, 3, 7, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (17, 1, 1, NULL, 5, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (18, 1, 1, NULL, 5, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (19, 1, 1, NULL, 5, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (20, 1, 1, NULL, 5, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (21, 1, 1, NULL, 5, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (22, 1, 1, NULL, 6, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (23, 1, 1, NULL, 6, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (24, 1, 1, NULL, 6, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (25, 1, 1, NULL, 6, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (26, 1, 1, NULL, 6, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (27, 1, 1, NULL, 7, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (28, 1, 1, NULL, 7, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (29, 1, 1, NULL, 7, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (30, 1, 1, NULL, 7, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (31, 1, 1, NULL, 7, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (32, 1, 3, 3, NULL, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (33, 1, 3, 3, NULL, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (34, 1, 2, NULL, 8, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (35, 1, 2, NULL, 8, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (36, 1, 2, NULL, 8, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (37, 1, 2, NULL, 8, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (38, 1, 2, NULL, 8, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (39, 1, 1, NULL, 9, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (40, 1, 1, NULL, 9, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (41, 1, 1, NULL, 9, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (42, 1, 1, NULL, 9, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (43, 1, 1, NULL, 9, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (44, 1, 1, NULL, 10, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (45, 1, 1, NULL, 10, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (46, 1, 1, NULL, 10, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (47, 1, 1, NULL, 10, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (48, 1, 1, NULL, 10, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (49, 1, 3, 5, NULL, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (50, 1, 3, 5, NULL, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (51, 1, 3, 1, NULL, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (52, 1, 3, 1, NULL, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (53, 1, 2, NULL, 11, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (54, 1, 2, NULL, 11, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (55, 1, 2, NULL, 11, 8, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (56, 1, 2, NULL, 11, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (57, 1, 1, NULL, 12, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (58, 1, 1, NULL, 12, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (59, 1, 1, NULL, 12, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (60, 1, 1, NULL, 12, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (61, 1, 1, NULL, 12, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (62, 1, 1, NULL, 15, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (63, 1, 1, NULL, 15, 7, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (64, 1, 1, NULL, 16, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (65, 1, 1, NULL, 16, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (66, 1, 1, NULL, 16, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (67, 1, 3, 4, NULL, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (68, 1, 3, 4, NULL, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (69, 1, 2, NULL, 17, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (70, 1, 2, NULL, 17, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (71, 1, 2, NULL, 17, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (72, 1, 2, NULL, 17, 4, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (73, 1, 2, NULL, 17, 6, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (74, 1, 2, NULL, 17, 11, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (75, 1, 2, NULL, 17, 12, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (76, 1, 2, NULL, 17, 13, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (77, 1, 2, NULL, 17, 14, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (78, 1, 2, NULL, 17, 15, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (79, 1, 2, NULL, 17, 16, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (80, 1, 2, NULL, 17, 17, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (81, 1, 2, NULL, 18, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (82, 1, 2, NULL, 18, 2, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (83, 1, 2, NULL, 18, 3, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (84, 1, 2, NULL, 18, 4, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.792
CREATE TABLE [Core].[OperationLogArchive] (
    [OperationLogArchiveID]   INT              NOT NULL,
    [BranchID]                INT              NOT NULL,
    [FiscalPeriodID]          INT              NOT NULL,
    [OperationID]             INT              NOT NULL,
    [SourceID]                INT              NULL,
    [SourceListID]            INT              NULL,
    [EntityTypeID]            INT              NULL,
    [Date]                    DATETIME         NOT NULL,
    [Time]                    TIME(7)          NOT NULL,
    [UserId]                  INT              NOT NULL,
    [CompanyId]               INT              NOT NULL,
    [EntityId]                INT              NULL,
    [Description]             NVARCHAR(MAX)    NULL,
    [rowguid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_Core_OperationLogArchive_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]            DATETIME         CONSTRAINT [DF_Core_OperationLogArchive_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_OperationLogArchive] PRIMARY KEY CLUSTERED ([OperationLogArchiveID] ASC)
    , CONSTRAINT [FK_Core_OperationLogArchive_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[OperationSourceList]([OperationSourceListID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
)
GO

-- 1.1.795
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (21, N'GroupDelete')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

-- 1.1.832
SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (9, N'Settings')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

-- 1.1.833
SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID],[Name]) VALUES (6, N'BalanceByAccount')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (85, 1, 3, 6, NULL, 1, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (86, 1, 3, 6, NULL, 6, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.835
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (30, N'ViewArchive')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (87, 1, 2, NULL, 11, 30, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.838
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (31, N'CalendarChange')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (32, N'CurrencyChange')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (33, N'DecimalCountChange')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (34, N'DefaultCodingChange')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (88, 1, 1, 9, NULL, 31, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (89, 1, 1, 9, NULL, 32, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (90, 1, 1, 9, NULL, 33, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (91, 1, 1, 9, NULL, 34, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

UPDATE [Metadata].[OperationSource]
SET [Name] = N'EnvironmentParams'
WHERE [Name] = N'Settings'

-- 1.1.839
SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (92, 1, 1, NULL, 1, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (93, 1, 1, NULL, 4, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (94, 1, 1, NULL, 5, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (95, 1, 1, NULL, 6, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (96, 1, 1, NULL, 7, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (97, 1, 2, NULL, 8, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (98, 1, 1, NULL, 9, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (99, 1, 1, NULL, 10, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (100, 1, 2, NULL, 11, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (101, 1, 1, NULL, 12, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (102, 1, 2, NULL, 17, 21, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (103, 1, 2, NULL, 18, 21, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.843
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (35, N'RoleAccess')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (104, 1, 1, NULL, 5, 35, 0)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (105, 1, 1, NULL, 10, 35, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.844
DELETE FROM [Metadata].[EntityType]
WHERE [Name] IN ('RoleBranch', 'RoleFiscalPeriod')

-- 1.1.845
ALTER TABLE [Core].[OperationLog]
ADD [EntityCode] NVARCHAR(256) NULL
GO

ALTER TABLE [Core].[OperationLog]
ADD [EntityName] NVARCHAR(256) NULL
GO

ALTER TABLE [Core].[OperationLog]
ADD [EntityDescription] NVARCHAR(1024) NULL
GO

ALTER TABLE [Core].[OperationLog]
ADD [EntityNo] INT NULL
GO

ALTER TABLE [Core].[OperationLog]
ADD [EntityDate] DATETIME NULL
GO

ALTER TABLE [Core].[OperationLog]
ADD [EntityReference] NVARCHAR(64) NULL
GO

ALTER TABLE [Core].[OperationLog]
ADD [EntityAssociation] NVARCHAR(64) NULL
GO

ALTER TABLE [Core].[OperationLogArchive]
ADD [EntityCode] NVARCHAR(256) NULL
GO

ALTER TABLE [Core].[OperationLogArchive]
ADD [EntityName] NVARCHAR(256) NULL
GO

ALTER TABLE [Core].[OperationLogArchive]
ADD [EntityDescription] NVARCHAR(1024) NULL
GO

ALTER TABLE [Core].[OperationLogArchive]
ADD [EntityNo] INT NULL
GO

ALTER TABLE [Core].[OperationLogArchive]
ADD [EntityDate] DATETIME NULL
GO

ALTER TABLE [Core].[OperationLogArchive]
ADD [EntityReference] NVARCHAR(64) NULL
GO

ALTER TABLE [Core].[OperationLogArchive]
ADD [EntityAssociation] NVARCHAR(64) NULL
GO

-- 1.1.853
UPDATE [Core].[OperationLog]
SET EntityCode = ''
WHERE EntityCode IS NULL

UPDATE [Core].[OperationLog]
SET EntityName = ''
WHERE EntityName IS NULL

UPDATE [Core].[OperationLog]
SET EntityDescription = ''
WHERE EntityDescription IS NULL

UPDATE [Core].[OperationLog]
SET EntityReference = ''
WHERE EntityReference IS NULL

UPDATE [Core].[OperationLog]
SET EntityAssociation = ''
WHERE EntityAssociation IS NULL

UPDATE [Core].[OperationLogArchive]
SET EntityCode = ''
WHERE EntityCode IS NULL

UPDATE [Core].[OperationLogArchive]
SET EntityName = ''
WHERE EntityName IS NULL

UPDATE [Core].[OperationLogArchive]
SET EntityDescription = ''
WHERE EntityDescription IS NULL

UPDATE [Core].[OperationLogArchive]
SET EntityReference = ''
WHERE EntityReference IS NULL

UPDATE [Core].[OperationLogArchive]
SET EntityAssociation = ''
WHERE EntityAssociation IS NULL

-- 1.1.858
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Finance].[CustomerTaxInfo] (
    [CustomerTaxInfoID]   INT              IDENTITY (1, 1) NOT NULL,  
	[AccountID]           INT              NOT NULL,  
    [CustomerFirstName]   NVARCHAR(64)     NULL,
    [CustomerName]        NVARCHAR(128)    NOT NULL,
    [PersonType]          INT              NOT NULL,
    [BuyerType]           INT              NOT NULL,
    [EconomicCode]        NVARCHAR(12)     NULL,
    [Address]             NVARCHAR(256)    NOT NULL,
    [NationalCode]        NVARCHAR(11)     NOT NULL,
    [PerCityCode]         NVARCHAR(10)     NOT NULL,
    [PhoneNo]             NVARCHAR(64)     NOT NULL,
    [MobileNo]            NVARCHAR(64)     NOT NULL,
    [PostalCode]          NVARCHAR(10)     NOT NULL,
    [Description]         NVARCHAR(1024)   NULL,
	[rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_CustomerTaxInfo_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Finance_CustomerTaxInfo_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_CustomerTaxInfo] PRIMARY KEY CLUSTERED ([CustomerTaxInfoID] ASC)
    , CONSTRAINT [FK_Finance_CustomerTaxInfo_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
)
GO

-- 1.1.859
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Finance].[AccountOwner] (
    [AccountOwnerID]   INT              IDENTITY (1, 1) NOT NULL,
    [AccountID]        INT              NOT NULL,    
    [BankName]         NVARCHAR(64)     NOT NULL,
    [AccountType]      INT              NOT NULL,
    [BankBranchName]   NVARCHAR(64)     NOT NULL,
    [BranchIndex]      NVARCHAR(64)     NOT NULL,
    [AccountNumber]    NVARCHAR(32)     NOT NULL,
    [CardNumber]       NVARCHAR(32)     NULL,
    [ShabaNumber]      NVARCHAR(32)     NULL,
    [Description]      NVARCHAR(512)    NULL,
	[rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountOwner_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_AccountOwner_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountOwner] PRIMARY KEY CLUSTERED ([AccountOwnerID] ASC)
    , CONSTRAINT [FK_Finance_AccountOwner_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
)
GO

-- 1.1.862
UPDATE [Config].[LogSetting]
SET IsEnabled = 1

-- 1.1.869
DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 11 AND OperationID = 21

-- 1.1.876
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Finance].[AccountHolder] (
    [AccountHolderID]   INT              IDENTITY (1, 1) NOT NULL,
    [AccountOwnerID]    INT              NOT NULL,    
    [FirstName]         NVARCHAR(64)     NOT NULL,
    [LastName]          NVARCHAR(64)     NOT NULL,
    [HasSignature]      BIT              NOT NULL,
	[rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountHolder_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Finance_AccountHolder_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountHolder] PRIMARY KEY CLUSTERED ([AccountHolderID] ASC)
    , CONSTRAINT [FK_Finance_AccountHolder_Finance_AccountOwner] FOREIGN KEY ([AccountOwnerID]) REFERENCES [Finance].[AccountOwner]([AccountOwnerID])
)
GO

-- 1.1.877
DELETE FROM [Core].[OperationLog]
WHERE EntityTypeID IN(8, 18)

DELETE FROM [Core].[OperationLogArchive]
WHERE EntityTypeID IN(8, 18)

DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID IN(8, 18)

DELETE FROM [Metadata].[EntityType]
WHERE EntityTypeID IN(8, 18)

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (36, N'CreateLine')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (37, N'EditLine')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (38, N'DeleteLine')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (39, N'GroupDeleteLines')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (40, N'CreateRate')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (41, N'EditRate')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (42, N'DeleteRate')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (43, N'PrintRates')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (44, N'GroupDeleteRates')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (106, 1, 2, NULL, 17, 36, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (107, 1, 2, NULL, 17, 37, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (108, 1, 2, NULL, 17, 38, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (109, 1, 2, NULL, 17, 39, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (110, 1, 2, NULL, 7, 40, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (111, 1, 2, NULL, 7, 41, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (112, 1, 2, NULL, 7, 42, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (113, 1, 2, NULL, 7, 43, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (114, 1, 2, NULL, 7, 44, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.878
DELETE FROM [Core].[OperationLog]
WHERE SourceListID IN(42, 43, 44, 45, 46, 47, 48, 49)

DELETE FROM [Core].[OperationLogArchive]
WHERE SourceListID IN(42, 43, 44, 45, 46, 47, 48, 49)

SET IDENTITY_INSERT [Metadata].[OperationSourceList] ON
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (42, N'BalanceByOneAccount')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (43, N'BalanceByAllAccounts')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (44, N'BalanceByOneDetailAccount')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (45, N'BalanceByAllDetailAccounts')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (46, N'BalanceByOneCostCenter')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (47, N'BalanceByAllCostCenters')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (48, N'BalanceByOneProject')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (49, N'BalanceByAllProjects')
SET IDENTITY_INSERT [Metadata].[OperationSourceList] OFF

ALTER TABLE [Core].[OperationLog]
ADD CONSTRAINT [FK_Core_OperationLog_Metadata_OperationSourceList] FOREIGN KEY ([SourceListID])
    REFERENCES [Metadata].[OperationSourceList]([OperationSourceListID])
GO

-- 1.1.880
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE Finance.CustomerTaxInfo
	DROP CONSTRAINT FK_Finance_CustomerTaxInfo_Finance_Account
GO
ALTER TABLE Finance.Account SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE Finance.CustomerTaxInfo
	DROP CONSTRAINT DF_Finance_CustomerTaxInfo_rowguid
GO
ALTER TABLE Finance.CustomerTaxInfo
	DROP CONSTRAINT DF_Finance_CustomerTaxInfo_ModifiedDate
GO
CREATE TABLE Finance.Tmp_CustomerTaxInfo
	(
	CustomerTaxInfoID int NOT NULL IDENTITY (1, 1),
	AccountID int NOT NULL,
	CustomerFirstName nvarchar(64) NULL,
	CustomerName nvarchar(128) NOT NULL,
	PersonType int NOT NULL,
	BuyerType int NOT NULL,
	EconomicCode nvarchar(12) NULL,
	Address nvarchar(256) NOT NULL,
	NationalCode nvarchar(11) NOT NULL,
	PerCityCode nvarchar(10) NOT NULL,
	PhoneNo nvarchar(64) NOT NULL,
	MobileNo nvarchar(64) NOT NULL,
	PostalCode nvarchar(10) NOT NULL,
	ProvinceCode nvarchar(4) NOT NULL,
	CityCode nvarchar(16) NOT NULL,
	Description nvarchar(1024) NULL,
	rowguid uniqueidentifier NOT NULL ROWGUIDCOL,
	ModifiedDate datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE Finance.Tmp_CustomerTaxInfo SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE Finance.Tmp_CustomerTaxInfo ADD CONSTRAINT
	DF_CustomerTaxInfo_ProvinceCode DEFAULT N'' FOR ProvinceCode
GO
ALTER TABLE Finance.Tmp_CustomerTaxInfo ADD CONSTRAINT
	DF_CustomerTaxInfo_CityCode DEFAULT N'' FOR CityCode
GO
ALTER TABLE Finance.Tmp_CustomerTaxInfo ADD CONSTRAINT
	DF_Finance_CustomerTaxInfo_rowguid DEFAULT (newid()) FOR rowguid
GO
ALTER TABLE Finance.Tmp_CustomerTaxInfo ADD CONSTRAINT
	DF_Finance_CustomerTaxInfo_ModifiedDate DEFAULT (getdate()) FOR ModifiedDate
GO
SET IDENTITY_INSERT Finance.Tmp_CustomerTaxInfo ON
GO
IF EXISTS(SELECT * FROM Finance.CustomerTaxInfo)
	 EXEC('INSERT INTO Finance.Tmp_CustomerTaxInfo (CustomerTaxInfoID, AccountID, CustomerFirstName, CustomerName, PersonType, BuyerType, EconomicCode, Address, NationalCode, PerCityCode, PhoneNo, MobileNo, PostalCode, Description, rowguid, ModifiedDate)
		SELECT CustomerTaxInfoID, AccountID, CustomerFirstName, CustomerName, PersonType, BuyerType, EconomicCode, Address, NationalCode, PerCityCode, PhoneNo, MobileNo, PostalCode, Description, rowguid, ModifiedDate FROM Finance.CustomerTaxInfo WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT Finance.Tmp_CustomerTaxInfo OFF
GO
DROP TABLE Finance.CustomerTaxInfo
GO
EXECUTE sp_rename N'Finance.Tmp_CustomerTaxInfo', N'CustomerTaxInfo', 'OBJECT' 
GO
ALTER TABLE Finance.CustomerTaxInfo ADD CONSTRAINT
	PK_Finance_CustomerTaxInfo PRIMARY KEY CLUSTERED 
	(
	CustomerTaxInfoID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE Finance.CustomerTaxInfo ADD CONSTRAINT
	FK_Finance_CustomerTaxInfo_Finance_Account FOREIGN KEY
	(
	AccountID
	) REFERENCES Finance.Account
	(
	AccountID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Metadata].[Province] (
    [ProvinceID]     INT              IDENTITY (1, 1) NOT NULL,
	[Name]           NVARCHAR(64)     NOT NULL,
    [Code]           NVARCHAR(4)      NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Province_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Province_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Province] PRIMARY KEY CLUSTERED ([ProvinceID] ASC)
)
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Metadata].[City] (
    [CityID]         INT              IDENTITY (1, 1) NOT NULL,
    [ProvinceID]     INT              NOT NULL,
	[Name]           NVARCHAR(64)     NOT NULL,
    [Code]           NVARCHAR(16)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_City_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_City_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_City] PRIMARY KEY CLUSTERED ([CityID] ASC)
    , CONSTRAINT [FK_Metadata_City_Metadata_Province] FOREIGN KEY ([ProvinceID]) REFERENCES [Metadata].[Province]([ProvinceID])
)
GO

-- 1.1.894
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (45, N'ViewRates')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

DELETE FROM [Config].[LogSetting]
WHERE LogSettingID >= 110

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (110, 1, 1, NULL, 7, 40, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (111, 1, 1, NULL, 7, 41, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (112, 1, 1, NULL, 7, 42, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (113, 1, 1, NULL, 7, 43, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (114, 1, 1, NULL, 7, 44, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (115, 1, 1, NULL, 7, 45, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.899
ALTER TABLE [Finance].[FiscalPeriod]
ADD [InventoryMode] INT NOT NULL
CONSTRAINT [DF_Finance_FiscalPeriod_InventoryMode] DEFAULT (1)
WITH VALUES;
GO

-- 1.1.900
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (46, N'GroupCheck')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (47, N'UndoGroupCheck')
SET IDENTITY_INSERT [Metadata].[Operation] OFF


-- 1.1.901
SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (116, 1, 1, NULL, 17, 46, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (117, 1, 1, NULL, 17, 47, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF