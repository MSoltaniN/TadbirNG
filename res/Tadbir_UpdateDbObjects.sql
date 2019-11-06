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
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (1, 17, 106, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (2, 17, 107, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (3, 17, 108, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (4, 17, 109, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (5, 17, 110, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (6, 17, 111, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (7, 17, 112, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (8, 17, 113, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (9, 17, 114, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (10, 17, 115, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (11, 17, 116, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (12, 17, 117, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (13, 17, 118, 1, 1)

-- Add cashier accounts
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (14, 16, 102, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (15, 16, 103, 1, 1)

-- Add liquid asset accounts
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (16, 1, 101, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (17, 1, 106, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (18, 1, 119, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (19, 1, 120, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (20, 1, 124, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (21, 1, 126, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (22, 1, 130, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (23, 1, 131, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (24, 1, 135, 1, 1)

-- Add liquid liability accounts
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (25, 3, 146, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (26, 3, 148, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (27, 3, 150, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (28, 3, 160, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (29, 3, 161, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
    VALUES (30, 3, 162, 1, 1)
INSERT INTO [Finance].[AccountCollectionAccount] ([CollectionAccountID], [CollectionID], [AccountID], [BranchID], [FiscalPeriodID])
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
