USE [@FirstDbName]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA [Metadata]
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

CREATE SCHEMA [Reporting]
GO

CREATE SCHEMA [CashFlow]
GO

CREATE SCHEMA [Check]
GO














CREATE SCHEMA [ProductScope]
GO

CREATE TABLE [Core].[Version] (
    [VersionID]      INT              NOT NULL,
    [Number]         VARCHAR(16)      NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Core_Version_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Core_Version_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_Version] PRIMARY KEY CLUSTERED ([VersionID] ASC)
)
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

CREATE TABLE [Metadata].[OperationSourceList] (
    [OperationSourceListID] INT              IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR(128)    NOT NULL,
    [Description]           NVARCHAR(512)    NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_OperationSourceList_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Metadata_OperationSourceList_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_OperationSourceList] PRIMARY KEY CLUSTERED ([OperationSourceListID] ASC)
)
GO

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

CREATE TABLE [Metadata].[CustomForm] (
    [CustomFormID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_CustomForm_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_CustomForm_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_CustomForm] PRIMARY KEY CLUSTERED ([CustomFormID] ASC)
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

CREATE TABLE [Corporate].[Branch] (
    [BranchID]       INT              IDENTITY (1, 1) NOT NULL,
	[ParentID]       INT              NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
	[Level]          INT              CONSTRAINT [DF_Corporate_Branch_Level] DEFAULT (0) NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Corporate_Branch_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Corporate_Branch_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Corporate_Branch] PRIMARY KEY CLUSTERED ([BranchID] ASC)
)
GO

CREATE TABLE [Finance].[Currency] (
    [CurrencyID]       INT              IDENTITY (1, 1) NOT NULL,
	[FiscalPeriodID]   INT              CONSTRAINT [DF_Finance_Currency_FiscalPeriodID] DEFAULT (0) NOT NULL,
    [BranchID]         INT              NOT NULL,
    [BranchScope]      SMALLINT         CONSTRAINT [DF_Finance_Currency_BranchScope] DEFAULT (0) NOT NULL,
    [CreatedByID]      INT              NOT NULL,
    [CreatedByName]    NVARCHAR(64)     NOT NULL,
    [CreatedDate]      DATETIME         NOT NULL,
    [ModifiedByID]     INT              NOT NULL,
    [ModifiedByName]   NVARCHAR(64)     NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [Country]          NVARCHAR(64)     NOT NULL,
    [Code]             NVARCHAR(8)      NOT NULL,
    [TaxCode]          INT              NOT NULL,
    [MinorUnit]        NVARCHAR(32)     NOT NULL,
    [Multiplier]       INT              NOT NULL,
    [DecimalCount]     SMALLINT         NOT NULL,
    [IsActive]         BIT              NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Currency_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_Currency_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Currency] PRIMARY KEY CLUSTERED ([CurrencyID] ASC)
    , CONSTRAINT [FK_Finance_Currency_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
)
GO

CREATE TABLE [Finance].[CurrencyRate] (
    [CurrencyRateID]   INT              IDENTITY (1, 1) NOT NULL,
    [CurrencyID]       INT              NOT NULL,
	[FiscalPeriodID]   INT              CONSTRAINT [DF_Finance_CurrencyRate_FiscalPeriodID] DEFAULT (0) NOT NULL,
    [BranchID]         INT              NOT NULL,
    [CreatedByID]      INT              NOT NULL,
    [CreatedByName]    NVARCHAR(64)     NOT NULL,
    [CreatedDate]      DATETIME         NOT NULL,
    [ModifiedByID]     INT              NOT NULL,
    [ModifiedByName]   NVARCHAR(64)     NOT NULL,
    [BranchScope]      SMALLINT         CONSTRAINT [DF_Finance_CurrencyRate_BranchScope] DEFAULT (0) NOT NULL,
    [Date]             DATETIME         NOT NULL,
    [Time]             TIME(7)          NOT NULL,
    [Multiplier]       FLOAT            NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_CurrencyRate_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_CurrencyRate_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_CurrencyRate] PRIMARY KEY CLUSTERED ([CurrencyRateID] ASC)
    , CONSTRAINT [FK_Finance_CurrencyRate_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
    , CONSTRAINT [FK_Finance_CurrencyRate_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
)
GO

CREATE TABLE [Finance].[TaxCurrency] (
    [TaxCurrencyID]   INT              IDENTITY (1, 1) NOT NULL,
    [Code]            INT              NOT NULL,
    [Name]            NVARCHAR(64)     NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_TaxCurrency_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_TaxCurrency_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_TaxCurrency] PRIMARY KEY CLUSTERED ([TaxCurrencyID] ASC)
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
    [Values]         NVARCHAR(MAX)            NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_UserSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_UserSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_UserSetting] PRIMARY KEY CLUSTERED ([UserSettingID] ASC)
    , CONSTRAINT [FK_Config_UserSetting_Config_Setting] FOREIGN KEY ([SettingID]) REFERENCES [Config].[Setting]([SettingID])
)
GO

CREATE TABLE [Config].[ViewSetting] (
    [ViewSettingID]  INT              IDENTITY (1, 1) NOT NULL,
    [SettingID]      INT              NOT NULL,
    [ViewID]         INT              NULL,
    [ModelType]      VARCHAR(128)     NOT NULL,
    [Values]         NVARCHAR(MAX)            NOT NULL,
    [DefaultValues]  NVARCHAR(MAX)            NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_ViewSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_ViewSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_ViewSetting] PRIMARY KEY CLUSTERED ([ViewSettingID] ASC)
	, CONSTRAINT [FK_Config_ViewSetting_Config_Setting] FOREIGN KEY ([SettingID]) REFERENCES [Config].[Setting]([SettingID])
)
GO

CREATE TABLE [Config].[LabelSetting] (
    [LabelSettingID] INT              IDENTITY (1, 1) NOT NULL,
    [SettingID]      INT              NOT NULL,
    [CustomFormID]   INT              NOT NULL,
	[LocaleID]       INT              NOT NULL,
	[ModelType]      VARCHAR(128)     NOT NULL,
	[Values]         NVARCHAR(MAX)            NOT NULL,
	[DefaultValues]  NVARCHAR(MAX)            NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_LabelSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_LabelSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_LabelSetting] PRIMARY KEY CLUSTERED ([LabelSettingID] ASC)
    , CONSTRAINT [FK_Config_LabelSetting_Config_Setting] FOREIGN KEY ([SettingID]) REFERENCES [Config].[Setting]([SettingID])
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
    [Name]             NVARCHAR(64)     NOT NULL,
    [StartDate]        DATETIME         NOT NULL,
    [EndDate]          DATETIME         NOT NULL,
	[InventoryMode]    INT              CONSTRAINT [DF_Finance_FiscalPeriod_InventoryMode] DEFAULT (1) NOT NULL,
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

CREATE TABLE [Core].[OperationLog] (
    [OperationLogID]      INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]            INT              NOT NULL,
    [FiscalPeriodID]      INT              NOT NULL,
    [OperationID]         INT              NOT NULL,
    [SourceID]            INT              NULL,
    [EntityTypeID]        INT              NULL,
    [SourceListID]        INT              NULL,
    [Date]                DATETIME         NOT NULL,
    [Time]                TIME(7)          NOT NULL,
    [UserId]              INT              NOT NULL,
    [CompanyId]           INT              NOT NULL,
    [EntityId]            INT              NULL,
    [EntityCode]          NVARCHAR(256)    NULL,
    [EntityName]          NVARCHAR(256)    NULL,
    [EntityDescription]   NVARCHAR(1024)   NULL,
    [EntityNo]            INT              NULL,
    [EntityDate]          DATETIME         NULL,
    [EntityReference]     NVARCHAR(64)     NULL,
    [EntityAssociation]   NVARCHAR(64)     NULL,
    [Description]         NVARCHAR(MAX)    NULL,
    [rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Core_OperationLog_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Core_OperationLog_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_OperationLog] PRIMARY KEY CLUSTERED ([OperationLogID] ASC)
    , CONSTRAINT [FK_Core_OperationLog_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Core_OperationLog_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Core_OperationLog_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_OperationLog_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_OperationLog_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[OperationSourceList]([OperationSourceListID])
    , CONSTRAINT [FK_Core_OperationLog_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
)
GO

CREATE TABLE [Core].[OperationLogArchive] (
    [OperationLogArchiveID]  INT              NOT NULL,
    [BranchID]               INT              NOT NULL,
    [FiscalPeriodID]         INT              NOT NULL,
    [OperationID]            INT              NOT NULL,
    [SourceID]               INT              NULL,
    [EntityTypeID]           INT              NULL,
    [SourceListID]           INT              NULL,
    [Date]                   DATETIME         NOT NULL,
    [Time]                   TIME(7)          NOT NULL,
    [UserId]                 INT              NOT NULL,
    [CompanyId]              INT              NOT NULL,
    [EntityId]               INT              NULL,
    [EntityCode]             NVARCHAR(256)    NULL,
    [EntityName]             NVARCHAR(256)    NULL,
    [EntityDescription]      NVARCHAR(1024)   NULL,
    [EntityNo]               INT              NULL,
    [EntityDate]             DATETIME         NULL,
    [EntityReference]        NVARCHAR(64)     NULL,
    [EntityAssociation]      NVARCHAR(64)     NULL,
    [Description]            NVARCHAR(MAX)    NULL,
    [rowguid]                UNIQUEIDENTIFIER CONSTRAINT [DF_Core_OperationLogArchive_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]           DATETIME         CONSTRAINT [DF_Core_OperationLogArchive_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_OperationLogArchive] PRIMARY KEY CLUSTERED ([OperationLogArchiveID] ASC)
    , CONSTRAINT [FK_Core_OperationLogArchive_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Metadata_Operation] FOREIGN KEY ([OperationID]) REFERENCES [Metadata].[Operation]([OperationID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Metadata_Source] FOREIGN KEY ([SourceID]) REFERENCES [Metadata].[OperationSource]([OperationSourceID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Metadata_SourceList] FOREIGN KEY ([SourceListID]) REFERENCES [Metadata].[OperationSourceList]([OperationSourceListID])
    , CONSTRAINT [FK_Core_OperationLogArchive_Metadata_EntityType] FOREIGN KEY ([EntityTypeID]) REFERENCES [Metadata].[EntityType]([EntityTypeID])
)
GO

CREATE TABLE [Finance].[Account] (
    [AccountID]              INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]               INT              NULL,
    [FiscalPeriodID]         INT              NOT NULL,
    [BranchID]               INT              NOT NULL,
    [GroupID]                INT              NULL,
    [CurrencyID]             INT              NULL,
    [BranchScope]            SMALLINT         NOT NULL,
    [CreatedByID]            INT              NOT NULL,
    [CreatedByName]          NVARCHAR(64)     NOT NULL,
    [CreatedDate]            DATETIME         NOT NULL,
    [ModifiedByID]           INT              NOT NULL,
    [ModifiedByName]         NVARCHAR(64)     NOT NULL,
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
    [AccountCollectionAccountID]  INT              IDENTITY (1, 1) NOT NULL,
	[CollectionID]                INT              NOT NULL,
    [AccountID]                   INT              NOT NULL,
    [BranchID]                    INT              NOT NULL,
    [FiscalPeriodID]              INT              NOT NULL,
    [rowguid]                     UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountCollectionAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]                DATETIME         CONSTRAINT [DF_Finance_AccountCollectionAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountCollectionAccount] PRIMARY KEY CLUSTERED ([AccountCollectionAccountID] ASC)
    , CONSTRAINT [FK_Finance_AccountCollectionAccount_Finance_Collection] FOREIGN KEY ([CollectionID]) REFERENCES [Finance].[AccountCollection]([CollectionID])
    , CONSTRAINT [FK_Finance_AccountCollectionAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Finance_AccountCollectionAccount_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Finance_AccountCollectionAccount_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
)
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


CREATE TABLE [Finance].[Voucher] (
    [VoucherID]       INT              IDENTITY (1, 1) NOT NULL,
    [FiscalPeriodID]  INT              NOT NULL,
    [BranchID]        INT              NOT NULL,
    [StatusID]        INT              NOT NULL,
	[OriginID]        INT              CONSTRAINT [DF_Finance_Voucher_OriginID] DEFAULT (1) NOT NULL,
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
    , CONSTRAINT [FK_Finance_Voucher_Finance_Status] FOREIGN KEY ([StatusID]) REFERENCES [Core].[DocumentStatus]([StatusID])
	, CONSTRAINT [FK_Finance_Voucher_Finance_VoucherOrigin] FOREIGN KEY ([OriginID]) REFERENCES [Finance].[VoucherOrigin]([OriginID])
)
GO

CREATE TABLE [Finance].[DetailAccount] (
    [DetailAccountID]   INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]          INT              NULL,
	[FiscalPeriodID]    INT              NOT NULL,
	[CurrencyID]        INT              NULL,
	[BranchID]          INT              NOT NULL,
	[BranchScope]       SMALLINT         CONSTRAINT [DF_Finance_DetailAccount_BranchScope] DEFAULT (0) NOT NULL,
    [CreatedByID]       INT              NOT NULL,
    [CreatedByName]     NVARCHAR(64)     NOT NULL,
    [CreatedDate]       DATETIME         NOT NULL,
    [ModifiedByID]      INT              NOT NULL,
    [ModifiedByName]    NVARCHAR(64)     NOT NULL,
    [Code]              NVARCHAR(16)     NOT NULL,
    [FullCode]          NVARCHAR(256)    NOT NULL,
    [Name]              NVARCHAR(256)    NOT NULL,
    [Level]             SMALLINT         CONSTRAINT [DF_Finance_DetailAccount_Level] DEFAULT (0) NOT NULL,
    [Description]       NVARCHAR(512)    NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_DetailAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Finance_DetailAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_DetailAccount] PRIMARY KEY CLUSTERED ([DetailAccountID] ASC)
    , CONSTRAINT [FK_Finance_DetailAccount_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
    , CONSTRAINT [FK_Finance_DetailAccount_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_DetailAccount_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
    , CONSTRAINT [FK_Finance_DetailAccount_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency] ([CurrencyID])
)
GO

CREATE TABLE [Finance].[CostCenter] (
    [CostCenterID]     INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]         INT              NULL,
    [FiscalPeriodID]   INT              NOT NULL,
    [BranchID]         INT              NOT NULL,
	[BranchScope]      SMALLINT         CONSTRAINT [DF_Finance_CostCenter_BranchScope] DEFAULT (0) NOT NULL,
    [CreatedByID]      INT              NOT NULL,
    [CreatedByName]    NVARCHAR(64)     NOT NULL,
    [CreatedDate]      DATETIME         NOT NULL,
    [ModifiedByID]     INT              NOT NULL,
    [ModifiedByName]   NVARCHAR(64)     NOT NULL,
    [Code]             NVARCHAR(16)     NOT NULL,
    [FullCode]         NVARCHAR(256)    NOT NULL,
    [Name]             NVARCHAR(256)    NOT NULL,
    [Level]            SMALLINT         CONSTRAINT [DF_Finance_CostCenter_Level] DEFAULT (0) NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_CostCenter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_CostCenter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_CostCenter] PRIMARY KEY CLUSTERED ([CostCenterID] ASC)
    , CONSTRAINT [FK_Finance_CostCenter_Finance_Parent] FOREIGN KEY ([ParentID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Finance_CostCenter_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_CostCenter_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
)
GO

CREATE TABLE [Finance].[Project] (
    [ProjectID]      INT              IDENTITY (1, 1) NOT NULL,
    [ParentID]       INT              NULL,
	[FiscalPeriodID] INT              NOT NULL,
	[BranchID]       INT              NOT NULL,
	[BranchScope]    SMALLINT         CONSTRAINT [DF_Finance_Project_BranchScope] DEFAULT (0) NOT NULL,
    [CreatedByID]    INT              NOT NULL,
    [CreatedByName]  NVARCHAR(64)     NOT NULL,
    [CreatedDate]    DATETIME         NOT NULL,
    [ModifiedByID]   INT              NOT NULL,
    [ModifiedByName] NVARCHAR(64)     NOT NULL,
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

CREATE TABLE [CashFlow].[SourceApp] (
    [SourceAppID]    INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]       INT              NOT NULL,
    [FiscalPeriodID] INT              NOT NULL,
    [BranchScope]    SMALLINT         NOT NULL,
    [Code]           NVARCHAR(16)     NOT NULL,
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

CREATE TABLE [Finance].[VoucherLine] (
    [VoucherLineID]   INT              IDENTITY (1, 1) NOT NULL,
    [VoucherID]       INT              NOT NULL,
    [FiscalPeriodID]  INT              NOT NULL,
    [BranchID]        INT              NOT NULL,
    [AccountID]       INT              NOT NULL,
    [DetailAccountID] INT              NULL,
    [CostCenterID]    INT              NULL,
    [ProjectID]       INT              NULL,
    [CurrencyID]      INT              NULL,
    [SourceAppID]     INT              NULL,
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
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_VoucherLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_VoucherLine_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_VoucherLine] PRIMARY KEY CLUSTERED ([VoucherLineID] ASC)
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Voucher] FOREIGN KEY ([VoucherID]) REFERENCES [Finance].[Voucher]([VoucherID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency]([CurrencyID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Finance_VoucherLine_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
    , CONSTRAINT [FK_Finance_VoucherLine_CashFlow_SourceApp] FOREIGN KEY ([SourceAppID]) REFERENCES [CashFlow].[SourceApp]([SourceAppID])
)
GO

CREATE TABLE [Finance].[AccountDetailAccount] (
    [AccountDetailAccountID] INT              IDENTITY (1, 1) NOT NULL,
    [AccountID]              INT              NOT NULL,
    [DetailAccountID]        INT              NOT NULL,
    [rowguid]                UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_AccountDetailAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]           DATETIME         CONSTRAINT [DF_Finance_AccountDetailAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_AccountDetailAccount] PRIMARY KEY CLUSTERED ([AccountDetailAccountID] ASC)
    , CONSTRAINT [FK_Finance_AccountDetailAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
    , CONSTRAINT [FK_Finance_AccountDetailAccount_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount] ([DetailAccountID])
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

CREATE TABLE [Check].[CheckBook] (
    [CheckBookID]     INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]        INT              NOT NULL,
    [AccountID]       INT              NOT NULL,
    [DetailAccountID] INT              NULL,
    [CostCenterID]    INT              NULL,
    [ProjectID]       INT              NULL,
    [CreatedByID]     INT              NOT NULL,
    [ModifiedByID]    INT              NOT NULL,
    [FiscalPeriodID]  INT              CONSTRAINT [DF_Check_CheckBook_FiscalPeriodID] DEFAULT (0) NOT NULL,
    [TextNo]          NVARCHAR(16)     NULL,
    [Name]            NVARCHAR(64)     NOT NULL,
    [IssueDate]       DATETIME         NOT NULL,
    [StartNo]         NVARCHAR(32)     NOT NULL,
    [EndNo]           NVARCHAR(32)     NOT NULL,
    [BankName]        NVARCHAR(32)     NULL,
    [IsArchived]      BIT              NULL,
    [SeriesNo]        NVARCHAR(32)     NOT NULL,
    [SayyadStartNo]   NVARCHAR(16)     NOT NULL,
    [CreatedDate]     DATETIME         NOT NULL,
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
    [SayyadNo]          NVARCHAR(16)     NOT NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Check_CheckBookPage_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Check_CheckBookPage_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Check_CheckBookPage] PRIMARY KEY CLUSTERED ([CheckBookPageID] ASC)
    , CONSTRAINT [FK_Check_CheckBookPage_Check_CheckBook] FOREIGN KEY ([CheckBookID]) REFERENCES [Check].[CheckBook]([CheckBookID])
)
GO

CREATE TABLE [Reporting].[Dashboard] (
    [DashboardID]      INT              IDENTITY (1, 1) NOT NULL,
    [UserID]           INT              NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_Dashboard_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_Dashboard_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_Dashboard] PRIMARY KEY CLUSTERED ([DashboardID] ASC)
)
GO

CREATE TABLE [Reporting].[DashboardTab] (
    [DashboardTabID]   INT              IDENTITY (1, 1) NOT NULL,
    [DashboardID]      INT              NOT NULL,
    [Index]            INT              NOT NULL,
	[Title]            NVARCHAR(128)    NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_DashboardTab_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_DashboardTab_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_DashboardTab] PRIMARY KEY CLUSTERED ([DashboardTabID] ASC)
    , CONSTRAINT [FK_Reporting_DashboardTab_Reporting_Dashboard] FOREIGN KEY ([DashboardID]) REFERENCES [Reporting].[Dashboard]([DashboardID])
)
GO

CREATE TABLE [Reporting].[WidgetFunction] (
    [WidgetFunctionID] INT              IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR(64)     NOT NULL,
    [Description]  NVARCHAR(512)    NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_WidgetFunction_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Reporting_WidgetFunction_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_WidgetFunction] PRIMARY KEY CLUSTERED ([WidgetFunctionID] ASC)
)
GO

CREATE TABLE [Reporting].[WidgetType] (
    [WidgetTypeID]     INT              IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_WidgetType_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_WidgetType_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_WidgetType] PRIMARY KEY CLUSTERED ([WidgetTypeID] ASC)
)
GO

CREATE TABLE [Reporting].[FunctionParameter] (
    [FunctionParameterID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR(64)     NOT NULL,
    [Alias]                 NVARCHAR(64)     NOT NULL,
    [Type]                  NVARCHAR(64)     NOT NULL,
    [DefaultValue]          NVARCHAR(128)    NOT NULL,
    [Description]           NVARCHAR(512)    NULL,
    [rowguid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_FunctionParameter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME         CONSTRAINT [DF_Reporting_FunctionParameter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_FunctionParameter] PRIMARY KEY CLUSTERED ([FunctionParameterID] ASC)
)
GO

CREATE TABLE [Reporting].[UsedParameter] (
    [UsedParameterID]   INT              IDENTITY (1, 1) NOT NULL,
    [ParameterID]       INT              NOT NULL,
    [FunctionID]        INT              NOT NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_UsedParameter_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_Reporting_UsedParameter_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_UsedParameter] PRIMARY KEY CLUSTERED ([UsedParameterID] ASC)
    , CONSTRAINT [FK_Reporting_UsedParameter_Reporting_Parameter] FOREIGN KEY ([ParameterID]) REFERENCES [Reporting].[FunctionParameter]([FunctionParameterID])
    , CONSTRAINT [FK_Reporting_UsedParameter_Reporting_Function] FOREIGN KEY ([FunctionID]) REFERENCES [Reporting].[WidgetFunction]([WidgetFunctionID])
)
GO

CREATE TABLE [Reporting].[Widget] (
    [WidgetID]         INT              IDENTITY (1, 1) NOT NULL,
    [CreatedByID]      INT              NOT NULL,
    [FunctionID]       INT              NOT NULL,
    [TypeID]           INT              NOT NULL,
    [Title]            NVARCHAR(128)    NOT NULL,
    [DefaultSettings]  NVARCHAR(1024)   NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_Widget_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_Widget_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_Widget] PRIMARY KEY CLUSTERED ([WidgetID] ASC)
    , CONSTRAINT [FK_Reporting_Widget_Reporting_WidgetFunction] FOREIGN KEY ([FunctionID]) REFERENCES [Reporting].[WidgetFunction]([WidgetFunctionID])
    , CONSTRAINT [FK_Reporting_Widget_Reporting_WidgetType] FOREIGN KEY ([TypeID]) REFERENCES [Reporting].[WidgetType]([WidgetTypeID])
)
GO

CREATE TABLE [Reporting].[WidgetAccount] (
    [WidgetAccountID]  INT              IDENTITY (1, 1) NOT NULL,
    [WidgetID]         INT              NOT NULL,
    [AccountID]        INT              NULL,
    [DetailAccountID]  INT              NULL,
    [CostCenterID]     INT              NULL,
    [ProjectID]        INT              NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_WidgetAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Reporting_WidgetAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_WidgetAccount] PRIMARY KEY CLUSTERED ([WidgetAccountID] ASC)
    , CONSTRAINT [FK_Reporting_WidgetAccount_Reporting_Widget] FOREIGN KEY ([WidgetID]) REFERENCES [Reporting].[Widget]([WidgetID])
    , CONSTRAINT [FK_Reporting_WidgetAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account]([AccountID])
    , CONSTRAINT [FK_Reporting_WidgetAccount_Finance_DetailAccount] FOREIGN KEY ([DetailAccountID]) REFERENCES [Finance].[DetailAccount]([DetailAccountID])
    , CONSTRAINT [FK_Reporting_WidgetAccount_Finance_CostCenter] FOREIGN KEY ([CostCenterID]) REFERENCES [Finance].[CostCenter]([CostCenterID])
    , CONSTRAINT [FK_Reporting_WidgetAccount_Finance_Project] FOREIGN KEY ([ProjectID]) REFERENCES [Finance].[Project]([ProjectID])
)
GO

CREATE TABLE [Reporting].[TabWidget] (
    [TabWidgetID]     INT              IDENTITY (1, 1) NOT NULL,
    [TabID]           INT              NOT NULL,
    [WidgetID]        INT              NOT NULL,
    [Settings]        NVARCHAR(1024)   NOT NULL,
    [DefaultSettings] NVARCHAR(1024)   NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Reporting_TabWidget_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Reporting_TabWidget_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Reporting_TabWidget] PRIMARY KEY CLUSTERED ([TabWidgetID] ASC)
    , CONSTRAINT [FK_Reporting_TabWidget_Reporting_Widget] FOREIGN KEY ([WidgetID]) REFERENCES [Reporting].[Widget]([WidgetID])
    , CONSTRAINT [FK_Reporting_TabWidget_Reporting_DashboardTab] FOREIGN KEY ([TabID]) REFERENCES [Reporting].[DashboardTab]([DashboardTabID])
)
GO

CREATE TABLE [Auth].[RoleWidget] (
    [RoleWidgetID] INT              IDENTITY (1, 1) NOT NULL,
    [RoleID]       INT              NOT NULL,
    [WidgetID]     INT              NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_RoleWidget_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Auth_RoleWidget_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_RoleWidget] PRIMARY KEY CLUSTERED ([RoleWidgetID] ASC)
    , CONSTRAINT [FK_Auth_RoleWidget_Reporting_Widget] FOREIGN KEY ([WidgetID]) REFERENCES [Reporting].[Widget]([WidgetID])
)
GO

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
	[ProvinceCode]        NVARCHAR(4)      NOT NULL,
    [CityCode]            NVARCHAR(16)     NOT NULL,
    [Description]         NVARCHAR(1024)   NULL,
	[rowguid]             UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_CustomerTaxInfo_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]        DATETIME         CONSTRAINT [DF_Finance_CustomerTaxInfo_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_CustomerTaxInfo] PRIMARY KEY CLUSTERED ([CustomerTaxInfoID] ASC)
    , CONSTRAINT [FK_Finance_CustomerTaxInfo_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
)
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

CREATE TABLE [Metadata].[Province] (
    [ProvinceID]     INT              IDENTITY (1, 1) NOT NULL,
	[Name]           NVARCHAR(64)     NOT NULL,
    [Code]           NVARCHAR(4)      NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_Province_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_Province_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_Province] PRIMARY KEY CLUSTERED ([ProvinceID] ASC)
)
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

CREATE TABLE [CashFlow].[CashRegister] (
    [CashRegisterID]   INT              IDENTITY (1, 1) NOT NULL,
    [FiscalPeriodID]   INT              NOT NULL,
    [BranchID]         INT              NOT NULL,
    [BranchScope]      SMALLINT         NOT NULL,
    [Name]             NVARCHAR(256)    NOT NULL,
    [Description]      NVARCHAR(256)    NULL,
    [CreatedByID]      INT              NOT NULL,
    [CreatedByName]    NVARCHAR(64)     NOT NULL,
    [CreatedDate]      DATETIME         NOT NULL,
    [ModifiedByID]     INT              NOT NULL,
    [ModifiedByName]   NVARCHAR(64)     NOT NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_CashFlow_CashRegister_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_CashFlow_CashRegister_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_CashFlow_CashRegister] PRIMARY KEY CLUSTERED ([CashRegisterID] ASC)
    , CONSTRAINT [FK_CashFlow_CashRegister_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod]([FiscalPeriodID])
    , CONSTRAINT [FK_CashFlow_CashRegister_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
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

CREATE TABLE [CashFlow].[PayReceive] (
    [PayReceiveID]      INT              IDENTITY (1, 1) NOT NULL,
    [FiscalPeriodID]    INT              NOT NULL,
    [BranchID]          INT              NOT NULL,
    [IssuedByID]        INT              NOT NULL,
    [ModifiedByID]      INT              NOT NULL,
    [ConfirmedByID]     INT              NULL,
    [ApprovedByID]      INT              NULL,
    [Type]              SMALLINT         NOT NULL,
    [TextNo]            NVARCHAR(16)     NOT NULL,
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

CREATE TABLE [CashFlow].[PayReceiveAccount] (
    [PayReceiveAccountID]   INT              IDENTITY (1, 1) NOT NULL,
    [AccountID]             INT              NULL,
    [CostCenterID]          INT              NULL,
    [ProjectID]             INT              NULL,
    [PayReceiveID]          INT              NOT NULL,
    [DetailAccountID]       INT              NULL,
    [Amount]                MONEY            NOT NULL,
    [Remarks]               NVARCHAR(512)    NULL,
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














































































































































































































CREATE TABLE [ProductScope].[Brand] (
    [BrandID]           INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]          INT              NOT NULL,
    [BranchScope]       SMALLINT         NOT NULL,
    [Name]              NVARCHAR(64)     NOT NULL,
    [EnName]            NVARCHAR(64)     NULL,
    [Description]       NVARCHAR(1024)   NULL,
    [SocialLink]        NVARCHAR(64)     NULL,
    [Website]           NVARCHAR(64)     NULL,
    [MetaKeyword]       NVARCHAR(64)     NULL,
    [IsActive]          BIT              NULL,
    [FiscalPeriodID]    INT              NOT NULL,
    [CreatedByID]       INT              NOT NULL,
    [CreatedByName]     NVARCHAR(64)     NOT NULL,
    [CreatedDate]       DATETIME         NOT NULL,
    [ModifiedByID]      INT              NOT NULL,
    [ModifiedByName]    NVARCHAR(64)     NOT NULL,
    [rowguid]           UNIQUEIDENTIFIER CONSTRAINT [DF_ProductScope_Brand_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]      DATETIME         CONSTRAINT [DF_ProductScope_Brand_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_ProductScope_Brand] PRIMARY KEY CLUSTERED ([BrandID] ASC)
    , CONSTRAINT [FK_ProductScope_Brand_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
)
GO

CREATE TABLE [ProductScope].[Unit] (
    [UnitID]         INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]       INT              NOT NULL,
    [BranchScope]    SMALLINT         NOT NULL,
    [Name]           NVARCHAR(64)     NULL,
    [EnName]         NVARCHAR(64)     NULL,
    [Description]    NVARCHAR(64)     NULL,
    [Symbol]         NVARCHAR(64)     NULL,
    [Status]         SMALLINT         NULL,
    [IsActive]       BIT              NULL,
    [FiscalPeriodID]    INT              NOT NULL,
    [CreatedByID]       INT              NOT NULL,
    [CreatedByName]     NVARCHAR(64)     NOT NULL,
    [CreatedDate]       DATETIME         NOT NULL,
    [ModifiedByID]      INT              NOT NULL,
    [ModifiedByName]    NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_ProductScope_Unit_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_ProductScope_Unit_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_ProductScope_Unit] PRIMARY KEY CLUSTERED ([UnitID] ASC)
    , CONSTRAINT [FK_ProductScope_Unit_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
)
GO

CREATE TABLE [ProductScope].[Property] (
    [PropertyID]     INT              IDENTITY (1, 1) NOT NULL,
    [BranchID]       INT              NOT NULL,
    [BranchScope]    SMALLINT         NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [EnName]         NVARCHAR(64)     NULL,
    [Description]    NVARCHAR(1024)   NULL,
    [Type]           SMALLINT         NULL,
    [Prefix]         NVARCHAR(64)     NULL,
    [Suffix]         NVARCHAR(64)     NULL,
    [IsActive]       BIT              NULL,
    [FiscalPeriodID]    INT              NOT NULL,
    [CreatedByID]       INT              NOT NULL,
    [CreatedByName]     NVARCHAR(64)     NOT NULL,
    [CreatedDate]       DATETIME         NOT NULL,
    [ModifiedByID]      INT              NOT NULL,
    [ModifiedByName]    NVARCHAR(64)     NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_ProductScope_Property_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_ProductScope_Property_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_ProductScope_Property] PRIMARY KEY CLUSTERED ([PropertyID] ASC)
    , CONSTRAINT [FK_ProductScope_Property_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch]([BranchID])
)
GO

-- Insert system records...
SET IDENTITY_INSERT [Reporting].[WidgetFunction] ON 
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (1, N'Function_DebitTurnover')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (2, N'Function_CreditTurnover')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (3, N'Function_NetTurnover')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (4, N'Function_Balance')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (5, N'FunctionXB_LiquidRatio')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (6, N'FunctionXT_GrossSales')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (7, N'FunctionXT_NetSales')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (8, N'FunctionXB_BankBalance')
INSERT [Reporting].[WidgetFunction] ([WidgetFunctionID], [Name]) VALUES (9, N'FunctionXB_CashBalance')
SET IDENTITY_INSERT [Reporting].[WidgetFunction] OFF

SET IDENTITY_INSERT [Reporting].[WidgetType] ON 
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (1, N'Chart_ColumnChart')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (2, N'Chart_BarChart')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (3, N'Chart_LineGraph')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (4, N'Chart_PieChart')
INSERT [Reporting].[WidgetType] ([WidgetTypeID], [Name]) VALUES (10, N'Gauge_Circular')
SET IDENTITY_INSERT [Reporting].[WidgetType] OFF

SET IDENTITY_INSERT [Reporting].[FunctionParameter] ON
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (1, N'FromDate', N'from', N'System.DateTime', N'FiscalPeriodStart')
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (2, N'ToDate', N'to', N'System.DateTime', N'FiscalPeriodEnd')
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (3, N'DateUnit', N'unit', N'System.Int32', N'Monthly')
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (4, N'MinValue', N'min', N'System.Int32', N'0')
INSERT [Reporting].[FunctionParameter] ([FunctionParameterID], [Name], [Alias], [Type], [DefaultValue])
  VALUES (5, N'MaxValue', N'max', N'System.Int32', N'100')
SET IDENTITY_INSERT [Reporting].[FunctionParameter] OFF

SET IDENTITY_INSERT [Reporting].[UsedParameter] ON
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (1, 1, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (2, 1, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (3, 1, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (4, 2, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (5, 2, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (6, 2, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (7, 3, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (8, 3, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (9, 3, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (10, 4, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (11, 4, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (12, 4, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (13, 5, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (14, 5, 4)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (15, 5, 5)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (16, 6, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (17, 6, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (18, 6, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (19, 7, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (20, 7, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (21, 7, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (22, 8, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (23, 8, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (24, 8, 3)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (25, 9, 1)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (26, 9, 2)
INSERT [Reporting].[UsedParameter] ([UsedParameterID], [FunctionID], [ParameterID]) VALUES (27, 9, 3)
SET IDENTITY_INSERT [Reporting].[UsedParameter] OFF

SET IDENTITY_INSERT [Core].[DocumentStatus] ON
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (1, N'NotChecked')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (2, N'Checked')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (3, N'Finalized')
SET IDENTITY_INSERT [Core].[DocumentStatus] OFF

SET IDENTITY_INSERT [Core].[DocumentType] ON
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (1, N'Voucher')
SET IDENTITY_INSERT [Core].[DocumentType] OFF

SET IDENTITY_INSERT [Config].[UserValueCategory] ON
INSERT INTO [Config].[UserValueCategory] ([CategoryID], [NameKey])
    VALUES (1, 'BankName')
SET IDENTITY_INSERT [Config].[UserValueCategory] OFF

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
    VALUES (6, 'QuickSearchSettings', 3, 2, 'QuickSearchConfig', N'{}', N'{}', 'QuickSearchSettingsDescription', 0)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (8, 'SystemConfigurationSettings', 2, 1, 'SystemConfig', N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendar":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true,"inventoryMode": 1}', N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendar":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true,"inventoryMode": 1}', 'SystemConfigurationDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (9, 'FinanceReportSettings', 2, 1, 'FinanceReportConfig', N'{"openingAsFirstVoucher":false,"startTurnoverAsInitBalance":false}', N'{"openingAsFirstVoucher":false,"startTurnoverAsInitBalance":false}', 'FinanceReportSettingsDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (10, 'FormLabelSettings', 2, 3, 'FormLabelConfig', N'{}', N'{}', NULL, 0)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (11, 'UserProfileSettings', 3, 1, 'UserProfileConfig', N'{}', N'{}', 'UserProfileSettingsDescription', 0)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (12, 'ReceiptSettings', 2, 1, 'ReceiptConfig', N'{"registerFlowConfig":{"confirmAfterSave":true, "approveAfterConfirm": true, "registerAfterApprove": true},"registerConfig":{"registerWithLastValidVoucher": true, "registerWithNewCreatedVoucher": false, "checkedVoucher": false}}', N'{"registerFlowConfig":{"confirmAfterSave":true, "approveAfterConfirm": true, "registerAfterApprove": true},"registerConfig":{"registerOnLastValidVoucher": true, "registerOnCreatedVoucher": false, "checkedVoucher": false}}', 'ReceiptSettingsDescription', 1)
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (13, 'PaymentSettings', 2, 1, 'PaymentConfig', N'{"registerFlowConfig":{"confirmAfterSave":true, "approveAfterConfirm": true, "registerAfterApprove": true},"registerConfig":{"registerWithLastValidVoucher": true, "registerWithNewCreatedVoucher": false, "checkedVoucher": false}}', N'{"registerFlowConfig":{"confirmAfterSave":true, "approveAfterConfirm": true, "registerAfterApprove": true},"registerConfig":{"registerOnLastValidVoucher": true, "registerOnCreatedVoucher": false, "checkedVoucher": false}}', 'PaymentSettingsDescription', 1)
SET IDENTITY_INSERT [Config].[Setting] OFF

SET IDENTITY_INSERT [Config].[ViewSetting] ON
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (1, 5, 1, 'ViewTreeConfig', N'{"viewId":1,"maxDepth":3,"levels":[{"no":1,"name":"LevelGeneral","codeLength":3,"isEnabled": true,"isUsed":true},{"no":2,"name":"LevelAuxiliary","codeLength":3,"isEnabled": true,"isUsed":true},{"no":3,"name":"LevelDetail","codeLength":4,"isEnabled": true,"isUsed":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}', N'{"viewId":1,"maxDepth":3,"levels":[{"no":1,"name":"LevelGeneral","codeLength":3,"isEnabled": true,"isUsed":true},{"no":2,"name":"LevelAuxiliary","codeLength":3,"isEnabled": true,"isUsed":true},{"no":3,"name":"LevelDetail","codeLength":4,"isEnabled": true,"isUsed":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (2, 5, 6, 'ViewTreeConfig', N'{"viewId":6,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}', N'{"viewId":6,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (3, 5, 7, 'ViewTreeConfig', N'{"viewId":7,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}', N'{"viewId":7,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}')
INSERT INTO [Config].[ViewSetting] (ViewSettingID, SettingID, ViewID, ModelType, [Values], DefaultValues)
    VALUES (4, 5, 8, 'ViewTreeConfig', N'{"viewId":8,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}', N'{"viewId":8,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}')
SET IDENTITY_INSERT [Config].[ViewSetting] OFF

SET IDENTITY_INSERT [Metadata].[EntityType] ON
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (1, N'Account', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (2, N'AccountCollectionAccount', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (4, N'AccountGroup', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (5, N'Branch', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (6, N'CostCenter', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (7, N'Currency', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (9, N'DetailAccount', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (10, N'FiscalPeriod', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (11, N'OperationLog', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (12, N'Project', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (15, N'Setting', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (17, N'Voucher', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (18, N'DraftVoucher', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (19, N'DashboardTab', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (20, N'Widget', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (21, N'CheckBook', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (22, N'CashRegister', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (23, N'SourceApp', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (24, N'Receipt', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (25, N'Payment', NULL)












































INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (100001, N'Brand', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (100002, N'Unit', NULL)
INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])
    VALUES (100003, N'Property', NULL)
SET IDENTITY_INSERT [Metadata].[EntityType] OFF

SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (1, N'View', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (2, N'Create', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (3, N'Edit', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (4, N'Delete', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (5, N'Filter', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (6, N'Print', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (7, N'Save', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (8, N'Archive', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (9, N'SetDefault', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (10, N'Design', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (11, N'Check', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (12, N'UndoCheck', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (13, N'Confirm', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (14, N'UndoConfirm', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (15, N'Approve', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (16, N'UndoApprove', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (17, N'Finalize', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (18, N'UndoFinalize', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (19, N'Mark', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (20, N'QuickReportDesign', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (21, N'GroupDelete', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (30, N'ViewArchive', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (31, N'CalendarChange', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (32, N'CurrencyChange', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (33, N'DecimalCountChange', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (34, N'DefaultCodingChange', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (35, N'RoleAccess', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (36, N'CreateLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (37, N'EditLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (38, N'DeleteLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (39, N'GroupDeleteLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (40, N'CreateRate', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (41, N'EditRate', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (42, N'DeleteRate', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (43, N'PrintRates', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (44, N'GroupDeleteRates', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (45, N'ViewRates', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (46, N'GroupCheck', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (47, N'GroupUndoCheck', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (48, N'GroupFinalize', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (49, N'GroupUndoFinalize', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (50, N'GroupConfirm', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (51, N'GroupUndoConfirm', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (52, N'Normalize', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (53, N'GroupNormalize', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (54, N'Export', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (55, N'ExportRates', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (56, N'FilterRates', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (58, N'PrintPreview', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (60, N'CreatePages', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (61, N'DeletePages', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (62, N'CancelPage', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (63, N'UndoCancelPage', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (64, N'ConnectToCheck', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (65, N'DisconnectFromCheck', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (66, N'AssignCashRegisterUser', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (67, N'UndoArchive', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (68, N'Register', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (69, N'RemoveInvalidAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (70, N'AggregateAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (71, N'CreateAccountLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (72, N'EditAccountLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (73, N'DeleteAccountLine', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (74, N'GroupDeleteAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (75, N'PrintAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (76, N'PrintPreviewAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (77, N'FilterAccountLines', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (78, N'ExportAccountLines', NULL)
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
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (89, N'Deactivate', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (90, N'Reactivate', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (91, N'PrintForm', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (92, N'PrintPreviewForm', NULL)
INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])
    VALUES (93, N'UndoRegister', NULL)
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (1, N'Journal', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (2, N'AccountBook', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (3, N'CurrencyBook', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (4, N'TestBalance', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (5, N'ItemBalance', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (6, N'BalanceByAccount', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (9, N'EnvironmentParams', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (10, N'ProfitLoss', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (11, N'AccountRelations', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (12, N'BalanceSheet', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (13, N'SystemIssue', NULL)
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])
    VALUES (15, N'CheckBookReport', NULL)
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

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
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (42, N'BalanceByOneAccount')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (43, N'BalanceByAllAccounts')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (44, N'BalanceByOneDetailAccount')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (45, N'BalanceByAllDetailAccounts')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (46, N'BalanceByOneCostCenter')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (47, N'BalanceByAllCostCenters')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (48, N'BalanceByOneProject')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (49, N'BalanceByAllProjects')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (50, N'ProfitLoss')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (51, N'ProfitLossByCostCenter')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (52, N'ProfitLossByProject')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (53, N'ProfitLossByBranch')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (54, N'ProfitLossByFiscalPeriod')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (55, N'BalanceSheet')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (56, N'UnbalancedVouchers')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (57, N'VouchersWithNoArticle')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (58, N'ArticlesHavingZeroAmount')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (59, N'ArticlesWithMissingAccount')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (60, N'ArticlesWithInvalidAccountItems')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (61, N'MissingVoucherNumbers')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (62, N'AccountsWithInvalidBalance')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID],[Name]) VALUES (63, N'AccountsWithInvalidPeriodTurnover')
SET IDENTITY_INSERT [Metadata].[OperationSourceList] OFF

SET IDENTITY_INSERT [Metadata].[Subsystem] ON
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (2, N'Accounting')
INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (3, N'Treasury')




INSERT INTO [Metadata].[Subsystem] ([SubsystemID], [Name]) VALUES (100000, N'ProductScope')
SET IDENTITY_INSERT [Metadata].[Subsystem] OFF

SET IDENTITY_INSERT [Metadata].[OperationSourceType] ON
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (1, N'BaseData')
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (2, N'OperationalForms')
INSERT INTO [Metadata].[OperationSourceType] ([OperationSourceTypeID], [Name]) VALUES (3, N'Reports')
SET IDENTITY_INSERT [Metadata].[OperationSourceType] OFF

SET IDENTITY_INSERT [Metadata].[CustomForm] ON
INSERT INTO [Metadata].[CustomForm] ([CustomFormID], [Name]) VALUES (1, 'ProfitLoss')
SET IDENTITY_INSERT [Metadata].[CustomForm] OFF

SET IDENTITY_INSERT [Config].[LabelSetting] ON
INSERT INTO [Config].[LabelSetting] ([LabelSettingID], [SettingID], [CustomFormID], [LocaleID], [ModelType], [Values], [DefaultValues])
    VALUES (1, 10, 1, 1, 'FormLabelConfig', N'{"formId": 1, "localeId": 1, "labelMap": {"GrossProfitCalculation": "Gross profit calculation", "NetRevenue": "Net revenue", "GrossProfit": "Gross profit", "SoldProductCost": "Sold product cost", "OperationalCost": "Operational cost", "OperationalCostTotal": "Operational cost total", "OperationalProfit": "Operational profit", "OtherCostAndRevenue": "Other cost and revenue", "OtherCostAndRevenueNet": "Other cost and revenue net", "ProfitBeforeTax": "Profit before tax", "Tax": "Tax", "NetProfit": "Net profit" }}', N'{"formId": 1, "localeId": 1, "labelMap": {"GrossProfitCalculation": "Gross profit calculation", "NetRevenue": "Net revenue", "GrossProfit": "Gross profit", "SoldProductCost": "Sold product cost", "OperationalCost": "Operational cost", "OperationalCostTotal": "Operational cost total", "OperationalProfit": "Operational profit", "OtherCostAndRevenue": "Other cost and revenue", "OtherCostAndRevenueNet": "Other cost and revenue net", "ProfitBeforeTax": "Profit before tax", "Tax": "Tax", "NetProfit": "Net profit" }}')
INSERT INTO [Config].[LabelSetting] ([LabelSettingID], [SettingID], [CustomFormID], [LocaleID], [ModelType], [Values], [DefaultValues])
    VALUES (2, 10, 1, 2, 'FormLabelConfig', N'{"formId": 1, "localeId": 2, "labelMap": {"GrossProfitCalculation": "محاسبه سود ناخالص", "NetRevenue": "درآمد خالص", "GrossProfit": "سود ناخالص", "SoldProductCost": "بهای تمام شده کالای فروش رفته", "OperationalCost": "هزینه های عملیاتی", "OperationalCostTotal": "جمع هزینه های عملیاتی", "OperationalProfit": "سود عملیاتی", "OtherCostAndRevenue": "سایر هزینه ها و درآمدها", "OtherCostAndRevenueNet": "خالص سایر هزینه ها و درآمدها", "ProfitBeforeTax": "سود قبل از کسر مالیات", "Tax": "مالیات", "NetProfit": "سود خالص" }}', N'{"formId": 1, "localeId": 2, "labelMap": {"GrossProfitCalculation": "محاسبه سود ناخالص", "NetRevenue": "درآمد خالص", "GrossProfit": "سود ناخالص", "SoldProductCost": "بهای تمام شده کالای فروش رفته", "OperationalCost": "هزینه های عملیاتی", "OperationalCostTotal": "جمع هزینه های عملیاتی", "OperationalProfit": "سود عملیاتی", "OtherCostAndRevenue": "سایر هزینه ها و درآمدها", "OtherCostAndRevenueNet": "خالص سایر هزینه ها و درآمدها", "ProfitBeforeTax": "سود قبل از کسر مالیات", "Tax": "مالیات", "NetProfit": "سود خالص" }}')
SET IDENTITY_INSERT [Config].[LabelSetting] OFF

SET IDENTITY_INSERT [Finance].[VoucherOrigin] ON
INSERT INTO [Finance].[VoucherOrigin] ([OriginID], [Name]) VALUES (1, N'NormalVoucher')
INSERT INTO [Finance].[VoucherOrigin] ([OriginID], [Name]) VALUES (2, N'OpeningVoucher')
INSERT INTO [Finance].[VoucherOrigin] ([OriginID], [Name]) VALUES (3, N'ClosingTempAccounts')
INSERT INTO [Finance].[VoucherOrigin] ([OriginID], [Name]) VALUES (4, N'ClosingVoucher')
SET IDENTITY_INSERT [Finance].[VoucherOrigin] OFF


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
INSERT INTO [Finance].[AccountCollectionCategory] ([CategoryID], [Name]) VALUES (1, N'BalanceSheet')
INSERT INTO [Finance].[AccountCollectionCategory] ([CategoryID], [Name]) VALUES (2, N'ProfitLoss')
INSERT INTO [Finance].[AccountCollectionCategory] ([CategoryID], [Name]) VALUES (3, N'Treasury')
INSERT INTO [Finance].[AccountCollectionCategory] ([CategoryID], [Name]) VALUES (4, N'SalesPurchase')
INSERT INTO [Finance].[AccountCollectionCategory] ([CategoryID], [Name]) VALUES (5, N'ClosingAccounts')
INSERT INTO [Finance].[AccountCollectionCategory] ([CategoryID], [Name]) VALUES (6, N'Warehouse')
INSERT INTO [Finance].[AccountCollectionCategory] ([CategoryID], [Name]) VALUES (7, N'Property')
SET IDENTITY_INSERT [Finance].[AccountCollectionCategory] OFF

SET IDENTITY_INSERT [Finance].[AccountCollection] ON
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])
    VALUES (1, 1, N'LiquidAssets', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])
    VALUES (2, 1, N'NonLiquidAssets', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])
    VALUES (3, 1, N'LiquidLiabilities', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (4, 1, N'NonLiquidLiabilities', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (5, 1, N'StakeholderEquity', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (6, 1, N'ContraAccounts', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (9, 2, N'Sales', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (10, 2, N'SalesRefundDiscounts', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (11, 2, N'CostOfGoodsSold', 1, 0, 1)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (12, 2, N'Purchase', 1, 0, 0)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (13, 2, N'PurchaseRefundDiscounts', 1, 0, 0)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (14, 2, N'OperationalCosts', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (15, 2, N'OtherRevenuesCosts', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (16, 3, N'CashFund', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])   
    VALUES (17, 3, N'Bank', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])   
    VALUES (18, 3, N'NotesReceivable', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (19, 3, N'NotesPayable', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])   
    VALUES (20, 3, N'GuaranteedNotesReceivable', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (21, 3, N'GuaranteedNotesPayable', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (22, 3, N'FloatNotes', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (23, 3, N'BouncedNotes', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (24, 3, N'PettyCash', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (25, 4, N'Sales', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (26, 4, N'SalesRefund', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (27, 4, N'Purchase', 1, 0, 0)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (28, 4, N'PurchaseRefund', 1, 0, 0)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (29, 4, N'SalesInvoiceCharges', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])
    VALUES (30, 4, N'PurchaseInvoiceCharges', 1, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (31, 4, N'TradeDebtors', 1, 2, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (32, 4, N'TradeCreditors', 1, 2, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (33, 4, N'SalesDiscount', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (34, 4, N'PurchaseDiscount', 0, 0, 0)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (35, 4, N'FinalCost', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (36, 4, N'OtherSellerPurchaser', 0, 1, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (37, 4, N'TaxPayable', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (38, 4, N'TollPayable', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (39, 4, N'TaxReceivable', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (40, 4, N'TollReceivable', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])
    VALUES (41, 5, N'Opening', 0, 1, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (42, 5, N'Closing', 0, 1, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (43, 5, N'Performance', 0, 1, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (44, 5, N'CurrentYearEarnings', 0, 1, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (45, 5, N'RetainedEarnings', 0, 1, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (46, 6, N'Inventory', 0, 2, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (47, 6, N'WageControl', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (48, 6, N'OverheadControl', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (49, 7, N'Property', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode]) 
    VALUES (50, 7, N'PropertyEarnings', 0, 0, 2)
INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode])  
    VALUES (51, 7, N'TransitionalProperty', 0, 0, 2)
SET IDENTITY_INSERT [Finance].[AccountCollection] OFF


SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (1, 2, 1, NULL, 1, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (2, 2, 1, NULL, 1, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (3, 2, 1, NULL, 1, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (4, 2, 1, NULL, 1, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (5, 2, 1, NULL, 1, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (6, 2, 1, NULL, 1, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (7, 2, 1, NULL, 1, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (8, 2, 1, NULL, 1, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (9, 2, 1, NULL, 1, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (10, 2, 1, NULL, 2, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (11, 2, 1, NULL, 2, 7, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (12, 2, 1, NULL, 4, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (13, 2, 1, NULL, 4, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (14, 2, 1, NULL, 4, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (15, 2, 1, NULL, 4, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (16, 2, 1, NULL, 4, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (17, 2, 1, NULL, 4, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (18, 2, 1, NULL, 4, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (19, 2, 1, NULL, 4, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (20, 2, 1, NULL, 4, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (21, 2, 1, NULL, 5, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (22, 2, 1, NULL, 5, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (23, 2, 1, NULL, 5, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (24, 2, 1, NULL, 5, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (25, 2, 1, NULL, 5, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (26, 2, 1, NULL, 5, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (27, 2, 1, NULL, 5, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (28, 2, 1, NULL, 5, 35, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (29, 2, 1, NULL, 5, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (30, 2, 1, NULL, 5, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (31, 2, 1, NULL, 6, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (32, 2, 1, NULL, 6, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (33, 2, 1, NULL, 6, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (34, 2, 1, NULL, 6, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (35, 2, 1, NULL, 6, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (36, 2, 1, NULL, 6, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (37, 2, 1, NULL, 6, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (38, 2, 1, NULL, 6, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (39, 2, 1, NULL, 6, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (40, 2, 1, NULL, 7, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (41, 2, 1, NULL, 7, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (42, 2, 1, NULL, 7, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (43, 2, 1, NULL, 7, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (44, 2, 1, NULL, 7, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (45, 2, 1, NULL, 7, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (46, 2, 1, NULL, 7, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (47, 2, 1, NULL, 7, 40, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (48, 2, 1, NULL, 7, 41, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (49, 2, 1, NULL, 7, 42, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (50, 2, 1, NULL, 7, 43, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (51, 2, 1, NULL, 7, 44, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (52, 2, 1, NULL, 7, 45, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (53, 2, 1, NULL, 7, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (54, 2, 1, NULL, 7, 55, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (55, 2, 1, NULL, 7, 56, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (56, 2, 1, NULL, 7, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (57, 2, 1, NULL, 9, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (58, 2, 1, NULL, 9, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (59, 2, 1, NULL, 9, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (60, 2, 1, NULL, 9, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (61, 2, 1, NULL, 9, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (62, 2, 1, NULL, 9, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (63, 2, 1, NULL, 9, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (64, 2, 1, NULL, 9, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (65, 2, 1, NULL, 9, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (66, 2, 1, NULL, 10, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (67, 2, 1, NULL, 10, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (68, 2, 1, NULL, 10, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (69, 2, 1, NULL, 10, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (70, 2, 1, NULL, 10, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (71, 2, 1, NULL, 10, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (72, 2, 1, NULL, 10, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (73, 2, 1, NULL, 10, 35, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (74, 2, 1, NULL, 10, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (75, 2, 1, NULL, 10, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (76, 2, 2, NULL, 11, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (77, 2, 2, NULL, 11, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (78, 2, 2, NULL, 11, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (79, 2, 2, NULL, 11, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (80, 2, 2, NULL, 11, 8, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (81, 2, 2, NULL, 11, 30, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (82, 2, 2, NULL, 11, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (83, 2, 2, NULL, 11, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (84, 2, 1, NULL, 12, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (85, 2, 1, NULL, 12, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (86, 2, 1, NULL, 12, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (87, 2, 1, NULL, 12, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (88, 2, 1, NULL, 12, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (89, 2, 1, NULL, 12, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (90, 2, 1, NULL, 12, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (91, 2, 1, NULL, 12, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (92, 2, 1, NULL, 12, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (93, 2, 1, NULL, 15, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (94, 2, 1, NULL, 15, 7, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (95, 2, 1, NULL, 15, 31, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (96, 2, 2, NULL, 17, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (97, 2, 2, NULL, 17, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (98, 2, 2, NULL, 17, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (99, 2, 2, NULL, 17, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100, 2, 2, NULL, 17, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (101, 2, 2, NULL, 17, 11, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (102, 2, 2, NULL, 17, 12, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (103, 2, 2, NULL, 17, 13, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (104, 2, 2, NULL, 17, 14, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (105, 2, 2, NULL, 17, 15, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (106, 2, 2, NULL, 17, 16, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (107, 2, 2, NULL, 17, 17, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (108, 2, 2, NULL, 17, 18, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (109, 2, 2, NULL, 17, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (110, 2, 2, NULL, 17, 36, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (111, 2, 2, NULL, 17, 37, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (112, 2, 2, NULL, 17, 38, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (113, 2, 2, NULL, 17, 39, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (114, 2, 2, NULL, 17, 46, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (115, 2, 2, NULL, 17, 47, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (116, 2, 2, NULL, 17, 48, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (117, 2, 2, NULL, 17, 49, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (118, 2, 2, NULL, 17, 50, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (119, 2, 2, NULL, 17, 51, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (120, 2, 2, NULL, 17, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (121, 2, 2, NULL, 18, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (122, 2, 2, NULL, 18, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (123, 2, 2, NULL, 18, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (124, 2, 2, NULL, 18, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (125, 2, 2, NULL, 18, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (126, 2, 2, NULL, 18, 11, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (127, 2, 2, NULL, 18, 12, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (128, 2, 2, NULL, 18, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (129, 2, 2, NULL, 18, 36, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (130, 2, 2, NULL, 18, 37, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (131, 2, 2, NULL, 18, 38, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (132, 2, 2, NULL, 18, 39, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (133, 2, 2, NULL, 18, 46, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (134, 2, 2, NULL, 18, 47, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (135, 2, 2, NULL, 18, 52, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (136, 2, 2, NULL, 18, 53, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (137, 2, 2, NULL, 18, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (138, 2, 3, NULL, 19, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (139, 2, 3, NULL, 19, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (140, 2, 3, NULL, 19, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (141, 2, 3, NULL, 19, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (142, 2, 3, NULL, 19, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (143, 2, 3, NULL, 20, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (144, 2, 3, NULL, 20, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (145, 2, 3, NULL, 20, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (146, 2, 3, NULL, 20, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (147, 2, 3, NULL, 20, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (148, 2, 3, NULL, 20, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (149, 2, 3, NULL, 20, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (150, 2, 3, NULL, 20, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (151, 3, 2, NULL, 21, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (152, 3, 2, NULL, 21, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (153, 3, 2, NULL, 21, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (154, 3, 2, NULL, 21, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (155, 3, 2, NULL, 21, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (156, 3, 2, NULL, 21, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (157, 3, 2, NULL, 21, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (158, 3, 2, NULL, 21, 60, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (159, 3, 2, NULL, 21, 61, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (160, 3, 2, NULL, 21, 62, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (161, 3, 2, NULL, 21, 63, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (162, 3, 2, NULL, 21, 64, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (163, 3, 2, NULL, 21, 65, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (164, 3, 1, NULL, 22, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (165, 3, 1, NULL, 22, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (166, 3, 1, NULL, 22, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (167, 3, 1, NULL, 22, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (168, 3, 1, NULL, 22, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (169, 3, 1, NULL, 22, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (170, 3, 1, NULL, 22, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (171, 3, 1, NULL, 22, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (172, 3, 1, NULL, 22, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (173, 3, 1, NULL, 22, 66, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (174, 3, 1, NULL, 23, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (175, 3, 1, NULL, 23, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (176, 3, 1, NULL, 23, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (177, 3, 1, NULL, 23, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (178, 3, 1, NULL, 23, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (179, 3, 1, NULL, 23, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (180, 3, 1, NULL, 23, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (181, 3, 1, NULL, 23, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (182, 3, 1, NULL, 23, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (183, 2, 3, 1, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (184, 2, 3, 1, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (185, 2, 3, 1, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (186, 2, 3, 1, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (187, 2, 3, 1, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (188, 2, 3, 2, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (189, 2, 3, 2, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (190, 2, 3, 2, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (191, 2, 3, 2, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (192, 2, 3, 2, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (193, 2, 3, 3, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (194, 2, 3, 3, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (195, 2, 3, 3, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (196, 2, 3, 3, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (197, 2, 3, 3, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (198, 2, 3, 4, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (199, 2, 3, 4, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (200, 2, 3, 4, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (201, 2, 3, 4, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (202, 2, 3, 4, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (203, 2, 3, 5, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (204, 2, 3, 5, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (205, 2, 3, 5, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (206, 2, 3, 5, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (207, 2, 3, 5, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (208, 2, 3, 6, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (209, 2, 3, 6, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (210, 2, 3, 6, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (211, 2, 3, 6, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (212, 2, 3, 6, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (213, 2, 1, 9, NULL, 32, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (214, 2, 1, 9, NULL, 33, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (215, 2, 1, 9, NULL, 34, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (216, 2, 3, 10, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (217, 2, 3, 10, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (218, 2, 3, 10, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (219, 2, 3, 10, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (220, 2, 3, 10, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (221, 2, 1, 11, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (222, 2, 1, 11, NULL, 7, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (223, 2, 3, 12, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (224, 2, 3, 12, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (225, 2, 3, 12, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (226, 2, 3, 12, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (227, 2, 3, 12, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (228, 2, 3, 13, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (229, 2, 3, 13, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (230, 2, 3, 13, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (231, 2, 3, 13, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (232, 2, 3, 13, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (233, 3, 3, 15, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (234, 3, 3, 15, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (235, 3, 3, 15, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (236, 3, 3, 15, NULL, 8, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (237, 3, 3, 15, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (238, 3, 3, 15, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (239, 3, 3, 15, NULL, 67, 1)
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
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (324, 3, 2, NULL, 24, 93, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (325, 3, 2, NULL, 25, 93, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (326, 2, 2, NULL, 11, 21, 1)

















































































































































































































































































































































































































INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100000, 100000, 1, NULL, 100001, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100001, 100000, 1, NULL, 100001, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100002, 100000, 1, NULL, 100001, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100003, 100000, 1, NULL, 100001, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100004, 100000, 1, NULL, 100001, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100005, 100000, 1, NULL, 100001, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100006, 100000, 1, NULL, 100001, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100007, 100000, 1, NULL, 100001, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100008, 100000, 1, NULL, 100001, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100009, 100000, 1, NULL, 100002, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100010, 100000, 1, NULL, 100002, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100011, 100000, 1, NULL, 100002, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100012, 100000, 1, NULL, 100002, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100013, 100000, 1, NULL, 100002, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100014, 100000, 1, NULL, 100002, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100015, 100000, 1, NULL, 100002, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100016, 100000, 1, NULL, 100002, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100017, 100000, 1, NULL, 100002, 58, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100018, 100000, 1, NULL, 100003, 1, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100019, 100000, 1, NULL, 100003, 2, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100020, 100000, 1, NULL, 100003, 3, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100021, 100000, 1, NULL, 100003, 4, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100022, 100000, 1, NULL, 100003, 5, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100023, 100000, 1, NULL, 100003, 6, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100024, 100000, 1, NULL, 100003, 21, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100025, 100000, 1, NULL, 100003, 54, 1)
INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], [SourceID], [EntityTypeID], [OperationID], [IsEnabled])
    VALUES (100026, 100000, 1, NULL, 100003, 58, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

