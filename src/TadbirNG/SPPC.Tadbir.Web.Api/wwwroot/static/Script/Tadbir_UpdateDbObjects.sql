
-- 1.2.1487
CREATE TABLE [CashFlow].[CashRegister] (
    [CashRegisterID]   INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]         INT              NOT NULL,
    [FiscalPeriodID]   INT              NOT NULL,
    [BranchScope]      SMALLINT         NOT NULL,
    [Name]             NVARCHAR(256)    NOT NULL,
    [Description]      NVARCHAR(256)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_CashRegister_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_CashFlow_CashRegister_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_CashRegister] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC)
    , CONSTRAINT [FK_CashFlow_CashRegister_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_CashFlow_CashRegister_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
)
GO

CREATE TABLE [CashFlow].[UserCashRegister] (
    [UserCashRegisterID]   INT              IDENTITY (1, 1) NOT NULL,
    [CashRegisterID]       INT              NOT NULL,
    [UserID]               INT              NOT NULL,
    [rowguid]              UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_UserCashRegister_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]         DATETIME         CONSTRAINT [DF_CashFlow_UserCashRegister_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_UserCashRegister] PRIMARY KEY CLUSTERED ([UserCashRegisterID] ASC)
    , CONSTRAINT [FK_CashFlow_UserCashRegister_CashFlow_CashRegister] FOREIGN KEY ([CashRegisterID]) REFERENCES [CashFlow].[CashRegister]([CashRegisterID])
)
GO

DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 22

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (22, N'CashRegister', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (66, N'AssignCashRegisterUser')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (214, 3, 1, NULL, 22, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (215, 3, 1, NULL, 22, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (216, 3, 1, NULL, 22, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (217, 3, 1, NULL, 22, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (218, 3, 1, NULL, 22, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (219, 3, 1, NULL, 22, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (220, 3, 1, NULL, 22, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (221, 3, 1, NULL, 22, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (222, 3, 1, NULL, 22, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (223, 3, 1, NULL, 22, 66, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1495
CREATE TABLE [Check].[CheckBook] (
    [CheckBookID]     INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]        INT              NOT NULL,
    [AccountID]       INT              NULL,
    [DetailAccountID] INT              NULL,
    [CostCenterID]    INT              NULL,
    [ProjectID]       INT              NULL,
    [FiscalPeriodID]  INT              CONSTRAINT [DF_Check_CheckBook_FiscalPeriodID] DEFAULT (0) NULL,
    [CheckBookNo]     NVARCHAR(32)     NOT NULL,
    [Name]            NVARCHAR(64)     NOT NULL,
    [IssueDate]       DATETIME         NOT NULL,
    [StartNo]         NVARCHAR(32)     NOT NULL,
    [EndNo]           NVARCHAR(32)     NOT NULL,
    [BankName]        NVARCHAR(32)     NULL,
    [IsArchived]      BIT              NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Check_CheckBook_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Check_CheckBook_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Check_CheckBook] PRIMARY KEY CLUSTERED ([CheckBookID] ASC)
    , CONSTRAINT [FK_Check_CheckBook_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Check_CheckBook_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Check_CheckBook_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
    , CONSTRAINT [FK_Check_CheckBook_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Check_CheckBook_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
)
GO

CREATE TABLE [Check].[CheckBookPage] (
    [CheckBookPageID]   INT              IDENTITY (1, 1) NOT NULL,
    [CheckBookID]       INT              NOT NULL,
    [SerialNo]          NVARCHAR(64)     NOT NULL,
    [Status]            SMALLINT         NULL,
    [CheckId]           INT              NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Check_CheckBookPage_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Check_CheckBookPage_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Check_CheckBookPage] PRIMARY KEY CLUSTERED ([CheckBookPageID] ASC)
    , CONSTRAINT [FK_Check_CheckBookPage_Check_CheckBook] FOREIGN KEY ([CheckBookID]) REFERENCES [Check].[CheckBook]([CheckBookID])
)
GO

DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 21

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (21, N'CheckBook', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (60, N'CreatePages')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (61, N'DeletePages')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (62, N'CancelPage')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (63, N'UndoCancelPage')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (64, N'ConnectToCheck')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (65, N'DisconnectFromCheck')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (201, 3, 2, NULL, 21, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (202, 3, 2, NULL, 21, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (203, 3, 2, NULL, 21, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (204, 3, 2, NULL, 21, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (205, 3, 2, NULL, 21, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (206, 3, 2, NULL, 21, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (207, 3, 2, NULL, 21, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (208, 3, 2, NULL, 21, 60, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (209, 3, 2, NULL, 21, 61, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (210, 3, 2, NULL, 21, 62, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (211, 3, 2, NULL, 21, 63, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (212, 3, 2, NULL, 21, 64, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (213, 3, 2, NULL, 21, 65, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1496
ALTER TABLE [Check].[CheckBook]
ALTER COLUMN [AccountID] INT NOT NULL

-- 1.2.1497
SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (15, N'CheckBook', NULL)
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (67, N'UndoArchive')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (224, 3, 3, 15, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (225, 3, 3, 15, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (226, 3, 3, 15, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (227, 3, 3, 15, NULL, 8, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (228, 3, 3, 15, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (229, 3, 3, 15, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (230, 3, 3, 15, NULL, 67, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1509
ALTER TABLE [Check].[CheckBook]
DROP COLUMN [CheckBookNo]
GO

ALTER TABLE [Check].[CheckBook]
ADD [CheckBookNo] INT NULL,
    [CreatedByID] INT CONSTRAINT [DF_Check_CheckBook_CreatedByID] DEFAULT (1) NOT NULL,
	[ModifiedByID] INT CONSTRAINT [DF_Check_CheckBook_ModifiedByID] DEFAULT (1) NOT NULL,
	[SeriesNo] NVARCHAR(32) CONSTRAINT [DF_Check_CheckBook_SeriesNo] DEFAULT 'A' NOT NULL,
	[SayyadStartNo] NVARCHAR(16) CONSTRAINT [DF_Check_CheckBook_SayyadStartNo] DEFAULT '1' NOT NULL,
	[CreatedDate] DATETIME CONSTRAINT [DF_Check_CheckBook_CreatedDate] DEFAULT (getdate()) NOT NULL
GO

ALTER TABLE [Check].[CheckBookPage]
ADD [SayyadNo] NVARCHAR(16) CONSTRAINT [DF_Check_CheckBookPage_SeriesNo] DEFAULT ('1') NOT NULL

-- 1.2.1510
DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 23

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (23, N'SourceApp', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (231, 3, 1, NULL, 23, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (232, 3, 1, NULL, 23, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (233, 3, 1, NULL, 23, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (234, 3, 1, NULL, 23, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (235, 3, 1, NULL, 23, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (236, 3, 1, NULL, 23, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (237, 3, 1, NULL, 23, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (238, 3, 1, NULL, 23, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (239, 3, 1, NULL, 23, 58, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1512
CREATE TABLE [Config].[UserValueCategory] (
    [CategoryID]   INT              IDENTITY (1, 1) NOT NULL,
    [NameKey]      NVARCHAR(64)     NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Config_UserValueCategory_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Config_UserValueCategory_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_UserValueCategory] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
)
GO

CREATE TABLE [Config].[UserValue] (
    [ValueID]      INT              IDENTITY (1, 1) NOT NULL,
    [CategoryID]   INT              NOT NULL,
    [Value]        NVARCHAR(512)    NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Config_UserValue_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Config_UserValue_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_UserValue] PRIMARY KEY CLUSTERED ([ValueID] ASC)
    , CONSTRAINT [FK_Config_UserValue_Config_Category] FOREIGN KEY ([CategoryID]) REFERENCES [Config].[UserValueCategory]([CategoryID])
)
GO

SET IDENTITY_INSERT [Config].[UserValueCategory] ON
INSERT INTO [Config].[UserValueCategory] ([CategoryID], [NameKey])
    VALUES (1, 'BankName')
SET IDENTITY_INSERT [Config].[UserValueCategory] OFF

-- 1.2.1513
CREATE TABLE [CashFlow].[SourceApp] (
    [SourceAppID]    INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]       INT              NOT NULL,
    [FiscalPeriodID] INT              NOT NULL,
    [BranchScope]    SMALLINT         NOT NULL,
    [Code]           NVARCHAR(64)     NOT NULL,
    [Name]           NVARCHAR(256)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [Type]           SMALLINT         NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_SourceApp_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_CashFlow_SourceApp_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_SourceApp] PRIMARY KEY CLUSTERED ([SourceAppID] ASC)
    , CONSTRAINT [FK_CashFlow_SourceApp_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_CashFlow_SourceApp_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
)
GO

-- 1.2.1515
ALTER TABLE [Finance].[VoucherLine]
ADD [DetailAccountID] INT NULL
GO

UPDATE [Finance].[VoucherLine]
SET [DetailAccountID] = [DetailID]

ALTER TABLE [Finance].[VoucherLine]
DROP CONSTRAINT [FK_Finance_VoucherLine_Finance_DetailAccount]
GO

ALTER TABLE [Finance].[VoucherLine]
DROP COLUMN [DetailID]
GO

ALTER TABLE [Finance].[VoucherLine]
ADD CONSTRAINT [FK_Finance_VoucherLine_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
GO

ALTER TABLE [Finance].[AccountDetailAccount]
ADD [DetailAccountID] INT NULL
GO

UPDATE [Finance].[AccountDetailAccount]
SET [DetailAccountID] = [DetailID]

ALTER TABLE [Finance].[AccountDetailAccount]
DROP CONSTRAINT [FK_Finance_AccountDetailAccount_Finance_DetailAccount]
GO

ALTER TABLE [Finance].[AccountDetailAccount]
DROP COLUMN [DetailID]
GO

ALTER TABLE [Finance].[AccountDetailAccount]
ALTER COLUMN [DetailAccountID] INT NOT NULL

ALTER TABLE [Finance].[AccountDetailAccount]
ADD CONSTRAINT [FK_Finance_AccountDetailAccount_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
GO

-- 1.2.1521
DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 24

DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 25

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (68, N'Register', NULL)
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (24, N'Payment', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (25, N'Receipt', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (240, 3, 2, NULL, 24, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (241, 3, 2, NULL, 24, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (242, 3, 2, NULL, 24, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (243, 3, 2, NULL, 24, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (244, 3, 2, NULL, 24, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (245, 3, 2, NULL, 24, 13, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (246, 3, 2, NULL, 24, 14, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (247, 3, 2, NULL, 24, 15, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (248, 3, 2, NULL, 24, 16, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (249, 3, 2, NULL, 24, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (250, 3, 2, NULL, 24, 68, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (251, 3, 2, NULL, 25, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (252, 3, 2, NULL, 25, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (253, 3, 2, NULL, 25, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (254, 3, 2, NULL, 25, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (255, 3, 2, NULL, 25, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (256, 3, 2, NULL, 25, 13, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (257, 3, 2, NULL, 25, 14, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (258, 3, 2, NULL, 25, 15, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (259, 3, 2, NULL, 25, 16, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (260, 3, 2, NULL, 25, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (261, 3, 2, NULL, 25, 68, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

CREATE TABLE [CashFlow].[PayReceive] (
    [PayReceiveID]      INT              IDENTITY (1, 1) NOT NULL,
    [FiscalPeriodID]    INT              NOT NULL,
    [BranchID]          INT              NOT NULL,
    [IssuedByID]        INT              NOT NULL,
    [ModifiedByID]      INT              NOT NULL,
    [ConfirmedByID]     INT              NULL,
    [ApprovedByID]      INT              NULL,
    [Type]              SMALLINT         NOT NULL,
    [PayReceiveNo]      NVARCHAR(16)     NOT NULL,
    [Reference]         NVARCHAR(64)     NULL,
    [Date]              DATETIME         NOT NULL,
    [CurrencyID]        INT              NULL,
    [CurrencyRate]      Money            NULL,
    [Description]       NVARCHAR(1024)   NULL,
    [CreatedDate]       DATETIME         NOT NULL,
    [IssuedByName]      NVARCHAR(64)     NOT NULL,
    [ModifiedByName]    NVARCHAR(64)     NOT NULL,
    [ConfirmedByName]   NVARCHAR(64)     NULL,
    [ApprovedByName]    NVARCHAR(64)     NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_PayReceive_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_CashFlow_PayReceive_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_PayReceive] PRIMARY KEY CLUSTERED ([PayReceiveID] ASC)
    , CONSTRAINT [FK_CashFlow_PayReceive_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_CashFlow_PayReceive_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
	, CONSTRAINT [FK_CashFlow_PayReceive_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
)
GO

-- 1.2.1523
UPDATE [Metadata].[EntityType]
SET [Name] = N'Receipt'
WHERE EntityTypeID = 25

-- 1.2.1525
UPDATE [Metadata].[OperationSource]
SET [Name] = N'CheckBookReport'
WHERE OperationSourceID = 15

-- 1.2.1528
ALTER TABLE [CashFlow].[SourceApp]
ALTER COLUMN [Code] NVARCHAR(16) NOT NULL

-- 1.2.1531
ALTER TABLE [Corporate].[Branch]
DROP COLUMN [CompanyID]
GO

ALTER TABLE [Finance].[FiscalPeriod]
DROP COLUMN [CompanyID]
GO

-- 1.2.1532
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (69, N'RemoveInvalidRows')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (70, N'RowsAggregation')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (71, N'CreateAccountLine')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (72, N'EditAccountLine')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (73, N'DeleteAccountLine')
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (74, N'GroupDeleteAccountLines')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (262, 3, 2, NULL, 25, 69, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (263, 3, 2, NULL, 25, 70, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (264, 3, 2, NULL, 25, 71, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (265, 3, 2, NULL, 25, 72, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (266, 3, 2, NULL, 25, 73, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (267, 3, 2, NULL, 25, 74, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (268, 3, 2, NULL, 24, 69, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (269, 3, 2, NULL, 24, 70, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (270, 3, 2, NULL, 24, 71, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (271, 3, 2, NULL, 24, 72, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (272, 3, 2, NULL, 24, 73, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (273, 3, 2, NULL, 24, 74, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

CREATE TABLE [CashFlow].[PayReceiveAccount] (
    [PayReceiveAccountID]   INT              IDENTITY (1, 1) NOT NULL,
    [AccountID]             INT              NULL,
    [CostCenterID]          INT              NULL,
    [ProjectID]             INT              NULL,
    [PayReceiveID]          INT              NOT NULL,
    [DetailAccountID]       INT              NULL,
    [Amount]                MONEY            NOT NULL,
    [Description]           NVARCHAR(512)    NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_PayReceiveAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_CashFlow_PayReceiveAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_PayReceiveAccount] PRIMARY KEY CLUSTERED ([PayReceiveAccountID] ASC)
    , CONSTRAINT [FK_CashFlow_PayReceiveAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_CashFlow_PayReceiveAccount_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_CashFlow_PayReceiveAccount_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
    , CONSTRAINT [FK_CashFlow_PayReceiveAccount_CashFlow_PayReceive] FOREIGN KEY ([PayReceiveID]) REFERENCES [CashFlow].[PayReceive]([PayReceiveID])
    , CONSTRAINT [FK_CashFlow_PayReceiveAccount_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
)
GO

-- 1.2.1535
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (75, N'PrintAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (76, N'PrintPreviewAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (77, N'FilterAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (78, N'ExportAccountLines', NULL)
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (274, 3, 2, NULL, 24, 75, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (275, 3, 2, NULL, 24, 76, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (276, 3, 2, NULL, 24, 77, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (277, 3, 2, NULL, 24, 78, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (278, 3, 2, NULL, 25, 75, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (279, 3, 2, NULL, 25, 76, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (280, 3, 2, NULL, 25, 77, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (281, 3, 2, NULL, 25, 78, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1545
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (79, N'CreateCashAccountLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (80, N'EditCashAccountLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (81, N'DeleteCashAccountLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])  
	VALUES (82, N'GroupDeleteCashAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (83, N'PrintCashAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (84, N'PrintPreviewCashAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (85, N'FilterCashAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (86, N'ExportCashAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (87, N'RemoveInvalidCashAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (88, N'AggregateCashAccountLines', NULL)
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (282, 3, 2, NULL, 24, 79, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (283, 3, 2, NULL, 24, 80, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (284, 3, 2, NULL, 24, 81, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (285, 3, 2, NULL, 24, 82, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (286, 3, 2, NULL, 24, 83, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (287, 3, 2, NULL, 24, 84, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (288, 3, 2, NULL, 24, 85, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (289, 3, 2, NULL, 24, 86, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (290, 3, 2, NULL, 25, 79, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (291, 3, 2, NULL, 25, 80, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (292, 3, 2, NULL, 25, 81, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (293, 3, 2, NULL, 25, 82, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (294, 3, 2, NULL, 25, 83, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (295, 3, 2, NULL, 25, 84, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (296, 3, 2, NULL, 25, 85, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (297, 3, 2, NULL, 25, 86, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (298, 3, 2, NULL, 24, 87, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (299, 3, 2, NULL, 24, 88, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (300, 3, 2, NULL, 25, 87, 0)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (301, 3, 2, NULL, 25, 88, 0)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

UPDATE [Metadata].[Operation]
SET [Metadata].[Operation].[Name] = N'RemoveInvalidAccountLines' 
WHERE OperationID = 69 and  [Name] = N'RemoveInvalidRows'

UPDATE [Metadata].[Operation]
SET [Metadata].[Operation].[Name] = N'AggregateAccountLines' 
WHERE OperationID = 70 and  [Name] = N'RowsAggregation'

CREATE TABLE [CashFlow].[PayReceiveCashAccount] (
    [PayReceiveCashAccountID]   INT              IDENTITY (1, 1) NOT NULL,
    [PayReceiveID]              INT              NOT NULL,
    [AccountID]                 INT              NULL,
    [DetailAccountID]           INT              NULL,
    [CostCenterID]              INT              NULL,
    [ProjectID]                 INT              NULL,
    [SourceAppID]               INT              NULL,
    [IsBank]                    BIT              NOT NULL,
    [Amount]                    MONEY            NOT NULL,
    [BankOrderNo]               NVARCHAR(64)     NULL,
    [Remarks]                   NVARCHAR(512)    NULL,
    [rowguid]                   UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_PayReceiveCashAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]              DATETIME         CONSTRAINT [DF_CashFlow_PayReceiveCashAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_PayReceiveCashAccount] PRIMARY KEY CLUSTERED ([PayReceiveCashAccountID] ASC)
    , CONSTRAINT [FK_CashFlow_PayReceiveCashAccount_CashFlow_PayReceive] FOREIGN KEY ([PayReceiveID]) REFERENCES [CashFlow].[PayReceive]([PayReceiveID])
    , CONSTRAINT [FK_CashFlow_PayReceiveCashAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_CashFlow_PayReceiveCashAccount_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
    , CONSTRAINT [FK_CashFlow_PayReceiveCashAccount_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_CashFlow_PayReceiveCashAccount_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
    , CONSTRAINT [FK_CashFlow_PayReceiveCashAccount_CashFlow_SourceApp] FOREIGN KEY ([SourceAppID]) REFERENCES [CashFlow].[SourceApp]([SourceAppID])
)
GO

EXEC sp_rename '[CashFlow].[PayReceiveAccount].[Description]', 'Remarks', 'COLUMN'

-- 1.2.1548
CREATE TABLE [CashFlow].[PayReceiveVoucherLine] (
    [PayReceiveVoucherLineID]   INT              IDENTITY (1, 1) NOT NULL,
    [PayReceiveID]              INT              NOT NULL,
    [VoucherLineID]             INT              NOT NULL,
    [rowguid]                   UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_PayReceiveVoucherLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]              DATETIME         CONSTRAINT [DF_CashFlow_PayReceiveVoucherLine_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_PayReceiveVoucherLine] PRIMARY KEY CLUSTERED ([PayReceiveVoucherLineID] ASC)
    , CONSTRAINT [FK_CashFlow_PayReceiveVoucherLine_CashFlow_PayReceive] FOREIGN KEY ([PayReceiveID]) REFERENCES [CashFlow].[PayReceive]([PayReceiveID])
    , CONSTRAINT [FK_CashFlow_PayReceiveVoucherLine_Finance_VoucherLine] FOREIGN KEY ([VoucherLineID]) REFERENCES [Finance].[VoucherLine]([VoucherLineID]))
GO

-- 1.2.1557
-- ترازنامه 
UPDATE [Finance].[AccountCollectionCategory]
SET [Name] = N'BalanceSheet'
WHERE [CategoryID] = 1

-- سود و زیان
UPDATE [Finance].[AccountCollectionCategory]
SET [Name] = N'ProfitLoss'
WHERE [CategoryID] = 2

-- خزانه داری  
UPDATE [Finance].[AccountCollectionCategory]
SET [Name] = N'Treasury'
WHERE [CategoryID] = 3

-- خرید و فروش
UPDATE [Finance].[AccountCollectionCategory]
SET [Name] = N'SalesPurchase'
WHERE [CategoryID] = 4

-- بستن حساب ها 
UPDATE [Finance].[AccountCollectionCategory]
SET [Name] = N'ClosingAccounts'
WHERE [CategoryID] = 5

-- انبار
UPDATE [Finance].[AccountCollectionCategory]
SET [Name] = N'Warehouse'
WHERE [CategoryID] = 6

-- اموال
UPDATE [Finance].[AccountCollectionCategory]
SET [Name] = N'Property'
WHERE [CategoryID] = 7

-- داراییهای جاری
UPDATE [Finance].[AccountCollection]
SET [Name] = N'LiquidAssets'
WHERE [CollectionID] = 1

-- داراییهای غیرجاری
UPDATE [Finance].[AccountCollection]
SET [Name] = N'NonLiquidAssets'
WHERE [CollectionID] = 2

-- بدهیهای جاری
UPDATE [Finance].[AccountCollection]
SET [Name] = N'LiquidLiabilities'
WHERE [CollectionID] = 3

-- بدهیهای غیرجاری
UPDATE [Finance].[AccountCollection]
SET [Name] = N'NonLiquidLiabilities'
WHERE [CollectionID] = 4

-- حقوق صاحبان سرمایه
UPDATE [Finance].[AccountCollection]
SET [Name] = N'StakeholderEquity'
WHERE [CollectionID] = 5

-- حسابهای انتظامی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'ContraAccounts'
WHERE [CollectionID] = 6

-- فروش
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Sales'
WHERE [CollectionID] = 9

-- برگشت  از فروش و تخفیفات
UPDATE [Finance].[AccountCollection]
SET [Name] = N'SalesRefundDiscounts'
WHERE [CollectionID] = 10

-- قیمت تمام شده کالای فروش رفته
UPDATE [Finance].[AccountCollection]
SET [Name] = N'CostOfGoodsSold'
WHERE [CollectionID] = 11

-- خرید
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Purchase'
WHERE [CollectionID] = 12

-- برگشت از خرید و تخفیفات
UPDATE [Finance].[AccountCollection]
SET [Name] = N'PurchaseRefundDiscounts'
WHERE [CollectionID] = 13

-- هزینه های عملیاتی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'OperationalCosts'
WHERE [CollectionID] = 14

-- سایر هزینه ها و درآمد ها
UPDATE [Finance].[AccountCollection]
SET [Name] = N'OtherRevenuesCosts'
WHERE [CollectionID] = 15

-- صندوق 
UPDATE [Finance].[AccountCollection]
SET [Name] = N'CashFund'
WHERE [CollectionID] = 16

-- بانک
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Bank'
WHERE [CollectionID] = 17

-- اسناد دریافتنی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'NotesReceivable'
WHERE [CollectionID] = 18

-- اسناد پرداختنی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'NotesPayable'
WHERE [CollectionID] = 19

-- اسناد دریافتنی تضمینی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'GuaranteedNotesReceivable'
WHERE [CollectionID] = 20

-- اسناد پرداختنی تضمینی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'GuaranteedNotesPayable'
WHERE [CollectionID] = 21

-- اسناد درجریان وصول
UPDATE [Finance].[AccountCollection]
SET [Name] = N'FloatNotes'
WHERE [CollectionID] = 22

-- اسناد برگشتی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'BouncedNotes'
WHERE [CollectionID] = 23

-- تنخواه گردان ها
UPDATE [Finance].[AccountCollection]
SET [Name] = N'PettyCash'
WHERE [CollectionID] = 24

-- فروش
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Sales'
WHERE [CollectionID] = 25

-- برگشت از فروش
UPDATE [Finance].[AccountCollection]
SET [Name] = N'SalesRefund'
WHERE [CollectionID] = 26

-- خرید
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Purchase'
WHERE [CollectionID] = 27

-- برگشت از خرید
UPDATE [Finance].[AccountCollection]
SET [Name] = N'PurchaseRefund'
WHERE [CollectionID] = 28

-- اضافات فاکتور فروش
UPDATE [Finance].[AccountCollection]
SET [Name] = N'SalesInvoiceCharges'
WHERE [CollectionID] = 29

-- اضافات فاکتور خرید
UPDATE [Finance].[AccountCollection]
SET [Name] = N'PurchaseInvoiceCharges'
WHERE [CollectionID] = 30

-- بدهکاران تجاری
UPDATE [Finance].[AccountCollection]
SET [Name] = N'TradeDebtors'
WHERE [CollectionID] = 31

-- بستانکاران تجاری
UPDATE [Finance].[AccountCollection]
SET [Name] = N'TradeCreditors'
WHERE [CollectionID] = 32

-- تخفیفات فروش
UPDATE [Finance].[AccountCollection]
SET [Name] = N'SalesDiscount'
WHERE [CollectionID] = 33

-- تخفیفات خرید
UPDATE [Finance].[AccountCollection]
SET [Name] = N'PurchaseDiscount'
WHERE [CollectionID] = 34

-- قیمت تمام شده
UPDATE [Finance].[AccountCollection]
SET [Name] = N'FinalCost'
WHERE [CollectionID] = 35

-- فروشنده / خریدار  متفرقه
UPDATE [Finance].[AccountCollection]
SET [Name] = N'OtherSellerPurchaser'
WHERE [CollectionID] = 36

-- مالیات پرداختنی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'TaxPayable'
WHERE [CollectionID] = 37

-- عوارض پرداختنی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'TollPayable'
WHERE [CollectionID] = 38

-- مالیات دریافتنی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'TaxReceivable'
WHERE [CollectionID] = 39

-- عوارض دریافتنی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'TollReceivable'
WHERE [CollectionID] = 40

-- افتتاحیه
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Opening'
WHERE [CollectionID] = 41

-- اختتامیه
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Closing'
WHERE [CollectionID] = 42

-- عملکرد
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Performance'
WHERE [CollectionID] = 43

-- سود و زیان سال جاری
UPDATE [Finance].[AccountCollection]
SET [Name] = N'CurrentYearEarnings'
WHERE [CollectionID] = 44

-- سود و زیان انباشته
UPDATE [Finance].[AccountCollection]
SET [Name] = N'RetainedEarnings'
WHERE [CollectionID] = 45

-- موجودی کالا
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Inventory'
WHERE [CollectionID] = 46

-- کنترل دستمزد
UPDATE [Finance].[AccountCollection]
SET [Name] = N'WageControl'
WHERE [CollectionID] = 47

-- کنترل سربار
UPDATE [Finance].[AccountCollection]
SET [Name] = N'OverheadControl'
WHERE [CollectionID] = 48

-- اموال
UPDATE [Finance].[AccountCollection]
SET [Name] = N'Property'
WHERE [CollectionID] = 49

-- سود و زیان عملیات اموال
UPDATE [Finance].[AccountCollection]
SET [Name] = N'PropertyEarnings'
WHERE [CollectionID] = 50

-- اموال انتقالی
UPDATE [Finance].[AccountCollection]
SET [Name] = N'TransitionalProperty'
WHERE [CollectionID] = 51

-- 1.2.1559

-- Add operation codes for new generic operations (Deactivate and Reactivate)...
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (89, N'Deactivate', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (90, N'Reactivate', NULL)
SET IDENTITY_INSERT [Metadata].[Operation] OFF

-- Add log settings for new generic operations (Deactivate and Reactivate) for all base entities ...
SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (302, 2, 1, NULL, 1, 89, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (303, 2, 1, NULL, 1, 90, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (304, 2, 1, NULL, 6, 89, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (305, 2, 1, NULL, 6, 90, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (306, 2, 1, NULL, 7, 89, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (307, 2, 1, NULL, 7, 90, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (308, 2, 1, NULL, 9, 89, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (309, 2, 1, NULL, 9, 90, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (310, 2, 1, NULL, 12, 89, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (311, 2, 1, NULL, 12, 90, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (312, 2, 1, NULL, 22, 89, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (313, 2, 1, NULL, 22, 90, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (314, 2, 1, NULL, 23, 89, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (315, 2, 1, NULL, 23, 90, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

CREATE TABLE [Core].[InactiveEntity] (
    [InactiveEntityID]   INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]           INT              NOT NULL,
    [FiscalPeriodID]     INT              NOT NULL,
    [EntityID]           INT              NOT NULL,
    [EntityName]         VARCHAR(64)      NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Core_InactiveEntity_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Core_InactiveEntity_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_InactiveEntity] PRIMARY KEY CLUSTERED ([InactiveEntityID] ASC)
    , CONSTRAINT [FK_Core_InactiveEntity_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Core_InactiveEntity_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
)
GO

DROP TABLE [Finance].[InactiveAccount]
GO

DROP TABLE [Finance].[InactiveCurrency]
GO

ALTER TABLE [Finance].[Account]
DROP CONSTRAINT DF_Finance_Account_IsActive
GO

ALTER TABLE [Finance].[Account]
DROP COLUMN [IsActive]
GO

ALTER TABLE [Finance].[Currency]
DROP COLUMN [IsActive]
GO

-- 1.2.1561
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (91, N'PrintForm', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (92, N'PrintPreviewForm', NULL)
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (316, 2, 2, NULL, 17, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (317, 2, 2, NULL, 17, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (318, 2, 2, NULL, 17, 91, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (319, 2, 2, NULL, 17, 92, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (320, 2, 2, NULL, 18, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (321, 2, 2, NULL, 18, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (322, 2, 2, NULL, 18, 91, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (323, 2, 2, NULL, 18, 92, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1566
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description]) 
	VALUES (93, N'UndoRegister', NULL)
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (324, 3, 2, NULL, 24, 93, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (325, 3, 2, NULL, 25, 93, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1567
ALTER TABLE [Finance].[VoucherLine]
ADD SourceAppID INT NULL;
GO

UPDATE [Finance].[VoucherLine]
SET SourceAppID = SourceID;
GO

ALTER TABLE [Finance].[VoucherLine]
DROP COLUMN SourceID;
GO

ALTER TABLE [Finance].[VoucherLine]
ADD CONSTRAINT [FK_Finance_VoucherLine_CashFlow_SourceApp] FOREIGN KEY ([SourceAppID])
    REFERENCES [CashFlow].[SourceApp]([SourceAppID])

-- 1.2.1571
ALTER TABLE [CashFlow].[CashRegister]
ADD [CreatedByID]    INT          CONSTRAINT [DF_CashFlow_CashRegister_CreatedById] DEFAULT (0) NOT NULL, 
	[CreatedByName]  NVARCHAR(64) CONSTRAINT [DF_CashFlow_CashRegister_CreatedByName] DEFAULT ('') NOT NULL, 
	[CreatedDate]    DATETIME     CONSTRAINT [DF_CashFlow_CashRegister_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[ModifiedByID]   INT          CONSTRAINT [DF_CashFlow_CashRegister_ModifiedById] DEFAULT (0) NOT NULL, 
	[ModifiedByName] NVARCHAR(64) CONSTRAINT [DF_CashFlow_CashRegister_ModifiedByName] DEFAULT ('') NOT NULL;
GO
UPDATE [CashFlow].[CashRegister]
SET [CreatedDate] = [ModifiedDate]

ALTER TABLE [CashFlow].[SourceApp]
ADD [CreatedByID]    INT          CONSTRAINT [DF_CashFlow_SourceApp_CreatedById] DEFAULT (0) NOT NULL, 
	[CreatedByName]  NVARCHAR(64) CONSTRAINT [DF_CashFlow_SourceApp_CreatedByName] DEFAULT ('') NOT NULL, 
	[CreatedDate]    DATETIME     CONSTRAINT [DF_CashFlow_SourceApp_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[ModifiedByID]   INT          CONSTRAINT [DF_CashFlow_SourceApp_ModifiedById] DEFAULT (0) NOT NULL, 
	[ModifiedByName] NVARCHAR(64) CONSTRAINT [DF_CashFlow_SourceApp_ModifiedByName] DEFAULT ('') NOT NULL;
GO
UPDATE [CashFlow].[SourceApp]
SET [CreatedDate] = [ModifiedDate]

ALTER TABLE [Finance].[Currency]
ADD [CreatedByID]    INT          CONSTRAINT [DF_Finance_Currency_CreatedById] DEFAULT (0) NOT NULL, 
	[CreatedByName]  NVARCHAR(64) CONSTRAINT [DF_Finance_Currency_CreatedByName] DEFAULT ('') NOT NULL, 
	[CreatedDate]    DATETIME     CONSTRAINT [DF_Finance_Currency_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[ModifiedByID]   INT          CONSTRAINT [DF_Finance_Currency_ModifiedById] DEFAULT (0) NOT NULL, 
	[ModifiedByName] NVARCHAR(64) CONSTRAINT [DF_Finance_Currency_ModifiedByName] DEFAULT ('') NOT NULL;
GO
UPDATE [Finance].[Currency]
SET [CreatedDate] = [ModifiedDate]

ALTER TABLE [Finance].[CurrencyRate]
ADD [CreatedByID]    INT          CONSTRAINT [DF_Finance_CurrencyRate_CreatedById] DEFAULT (0) NOT NULL, 
	[CreatedByName]  NVARCHAR(64) CONSTRAINT [DF_Finance_CurrencyRate_CreatedByName] DEFAULT ('') NOT NULL, 
	[CreatedDate]    DATETIME     CONSTRAINT [DF_Finance_CurrencyRate_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[ModifiedByID]   INT          CONSTRAINT [DF_Finance_CurrencyRate_ModifiedById] DEFAULT (0) NOT NULL, 
	[ModifiedByName] NVARCHAR(64) CONSTRAINT [DF_Finance_CurrencyRate_ModifiedByName] DEFAULT ('') NOT NULL;
GO
UPDATE [Finance].[CurrencyRate]
SET [CreatedDate] = [ModifiedDate]

ALTER TABLE [Finance].[Account]
ADD [CreatedByID]    INT          CONSTRAINT [DF_Finance_Account_CreatedById] DEFAULT (0) NOT NULL, 
	[CreatedByName]  NVARCHAR(64) CONSTRAINT [DF_Finance_Account_CreatedByName] DEFAULT ('') NOT NULL, 
	[CreatedDate]    DATETIME     CONSTRAINT [DF_Finance_Account_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[ModifiedByID]   INT          CONSTRAINT [DF_Finance_Account_ModifiedById] DEFAULT (0) NOT NULL, 
	[ModifiedByName] NVARCHAR(64) CONSTRAINT [DF_Finance_Account_ModifiedByName] DEFAULT ('') NOT NULL;
GO
UPDATE [Finance].[Account]
SET [CreatedDate] = [ModifiedDate]

ALTER TABLE [Finance].[CostCenter]
ADD [CreatedByID]    INT          CONSTRAINT [DF_Finance_CostCenter_CreatedById] DEFAULT (0) NOT NULL, 
	[CreatedByName]  NVARCHAR(64) CONSTRAINT [DF_Finance_CostCenter_CreatedByName] DEFAULT ('') NOT NULL, 
	[CreatedDate]    DATETIME     CONSTRAINT [DF_Finance_CostCenter_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[ModifiedByID]   INT          CONSTRAINT [DF_Finance_CostCenter_ModifiedById] DEFAULT (0) NOT NULL, 
	[ModifiedByName] NVARCHAR(64) CONSTRAINT [DF_Finance_CostCenter_ModifiedByName] DEFAULT ('') NOT NULL;
GO
UPDATE [Finance].[CostCenter]
SET [CreatedDate] = [ModifiedDate]

ALTER TABLE [Finance].[DetailAccount]
ADD [CreatedByID]    INT          CONSTRAINT [DF_Finance_DetailAccount_CreatedById] DEFAULT (0) NOT NULL, 
	[CreatedByName]  NVARCHAR(64) CONSTRAINT [DF_Finance_DetailAccount_CreatedByName] DEFAULT ('') NOT NULL, 
	[CreatedDate]    DATETIME     CONSTRAINT [DF_Finance_DetailAccount_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[ModifiedByID]   INT          CONSTRAINT [DF_Finance_DetailAccount_ModifiedById] DEFAULT (0) NOT NULL, 
	[ModifiedByName] NVARCHAR(64) CONSTRAINT [DF_Finance_DetailAccount_ModifiedByName] DEFAULT ('') NOT NULL;
GO
UPDATE [Finance].[DetailAccount]
SET [CreatedDate] = [ModifiedDate]

ALTER TABLE [Finance].[Project]
ADD [CreatedByID]    INT          CONSTRAINT [DF_Finance_Project_CreatedById] DEFAULT (0) NOT NULL, 
	[CreatedByName]  NVARCHAR(64) CONSTRAINT [DF_Finance_Project_CreatedByName] DEFAULT ('') NOT NULL, 
	[CreatedDate]    DATETIME     CONSTRAINT [DF_Finance_Project_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[ModifiedByID]   INT          CONSTRAINT [DF_Finance_Project_ModifiedById] DEFAULT (0) NOT NULL, 
	[ModifiedByName] NVARCHAR(64) CONSTRAINT [DF_Finance_Project_ModifiedByName] DEFAULT ('') NOT NULL;
GO
UPDATE [Finance].[Project]
SET [CreatedDate] = [ModifiedDate]

-- 1.2.1572
EXEC sp_rename '[CashFlow].[PayReceive].[PayReceiveNo]', 'TextNo', 'COLUMN'
EXEC sp_rename '[Check].[CheckBook].[CheckBookNo]', 'TextNo', 'COLUMN'

ALTER TABLE [CashFlow].[PayReceive]
ALTER COLUMN [TextNo] NVARCHAR(16) NOT NULL

ALTER TABLE [Check].[CheckBook]
ALTER COLUMN [TextNo] NVARCHAR(16) NULL

-- 1.2.1587
SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (326, 2, 2, NULL, 11, 21, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.2.1589
UPDATE [Metadata].[EntityType]
SET [Name] = N'Receipt'
WHERE [EntityTypeID] = 24

UPDATE [Metadata].[EntityType]
SET [Name] = N'Payment'
WHERE [EntityTypeID] = 25

-- 1.2.1590
SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (12, 'ReceiptSettings', 2, 1, 'ReceiptConfig', N'{"registerFlowConfig":{"confirmAfterSave":true, "approveAfterConfirm": true, "registerAfterApprove": true},"registerConfig":{"registerWithLastValidVoucher": true, "registerWithNewCreatedVoucher": false, "checkedVoucher": false}}', N'{"registerFlowConfig":{"confirmAfterSave":true, "approveAfterConfirm": true, "registerAfterApprove": true},"registerConfig":{"registerOnLastValidVoucher": true, "registerOnCreatedVoucher": false, "checkedVoucher": false}}', 'ReceiptSettingsDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (13, 'PaymentSettings', 2, 1, 'PaymentConfig', N'{"registerFlowConfig":{"confirmAfterSave":true, "approveAfterConfirm": true, "registerAfterApprove": true},"registerConfig":{"registerWithLastValidVoucher": true, "registerWithNewCreatedVoucher": false, "checkedVoucher": false}}', N'{"registerFlowConfig":{"confirmAfterSave":true, "approveAfterConfirm": true, "registerAfterApprove": true},"registerConfig":{"registerOnLastValidVoucher": true, "registerOnCreatedVoucher": false, "checkedVoucher": false}}', 'PaymentSettingsDescription', 1)
SET IDENTITY_INSERT [Config].[Setting] OFF

-- 1.2.1593
-- حذف تنظیمات لاگ منابع و مصارف و صندوق اسناد که به اشتباه برای زیرسیستم حسابداری ایجاد شده اند
DELETE FROM [Config].[LogSetting]
WHERE [LogSettingID] >= 312 AND [LogSettingID] <= 315
