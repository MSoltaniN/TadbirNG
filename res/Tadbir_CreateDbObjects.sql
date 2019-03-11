USE [XferDemo]
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

--SET IDENTITY_INSERT [Finance].[AccountCollectionCategory] ON 
--GO
--INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (1, N'ترازنامه ', N'49e54def-d6b9-4cd3-a065-8feef13026d9', CAST(N'2019-01-15T12:26:01.053' AS DateTime))
--GO
--INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (2, N'سود و زیان', N'21ed1889-c1a5-46dc-b352-60b577f42154', CAST(N'2019-01-15T12:26:13.500' AS DateTime))
--GO
--INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (3, N'خزانه داری  ', N'53757fa5-5e85-495d-ac53-a8f5a4869f6b', CAST(N'2019-01-15T12:26:26.410' AS DateTime))
--GO
--INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (4, N'خرید و فروش', N'2e9ec944-c56b-4350-8489-0e2c24a1757d', CAST(N'2019-01-15T12:26:37.690' AS DateTime))
--GO
--INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (5, N'بستن حساب ها ', N'247284c1-6d8c-4e9c-9fd2-8dc7aeb0d290', CAST(N'2019-01-15T12:26:52.327' AS DateTime))
--GO
--INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (6, N'انبار', N'8c22dd59-094b-4ca8-b49f-094c8e157b50', CAST(N'2019-01-15T12:27:05.800' AS DateTime))
--GO
--INSERT [Finance].[AccountCollectionCategory] ([CategoryID], [Name], [rowguid], [ModifiedDate]) VALUES (7, N'اموال', N'be57a7ce-a837-4b84-9cfa-e6e2b6f638f9', CAST(N'2019-01-15T12:27:19.193' AS DateTime))
--GO
--SET IDENTITY_INSERT [Finance].[AccountCollectionCategory] OFF
--GO


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

--SET IDENTITY_INSERT [Finance].[AccountCollection] ON 
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (1, 1, N'داراييهاى جارى  ', 1, 0, 2, N'afc7ae6a-4520-4263-a5a6-b32e0829118b', CAST(N'2019-01-15T12:32:19.127' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (2, 1, N'داراييهاى غيرجارى ', 1, 0, 2, N'7c34827f-3f03-4be0-8d30-ec4ebb1cc6fe', CAST(N'2019-01-15T12:32:37.267' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (3, 1, N'بدهيهاى جارى ', 1, 0, 2, N'3557c04b-fde4-40a7-b8f3-004f4b75cac0', CAST(N'2019-01-15T12:33:01.480' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (4, 1, N'بدهيهاى غيرجارى', 1, 0, 2, N'0f4021af-2e5c-4ce0-8757-26ee3dd64df4', CAST(N'2019-01-15T12:33:33.307' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (5, 1, N'حقوق صاحبان سرمايه ', 1, 0, 2, N'1755af0b-37c9-4758-a451-68f84af5439a', CAST(N'2019-01-15T12:33:49.270' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (6, 1, N'حسابهاى انتظامى', 1, 0, 2, N'1cb5f6d6-3caf-4f67-a091-c63c0b15bbeb', CAST(N'2019-01-15T12:34:08.290' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (9, 2, N'فروش', 1, 0, 2, N'f1210802-59a7-4d86-803b-e90e45d373f9', CAST(N'2019-01-15T12:36:50.283' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (10, 2, N'برگشت  از فروش و تخفیفات ', 1, 0, 2, N'be473c3e-273c-498d-9f27-fbf9c2b70f3c', CAST(N'2019-01-15T12:37:47.480' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (11, 2, N'قيمت تمام شده كالاى فروش رفته', 1, 0, 1, N'8af11bc5-21fb-4af6-bf18-0517fa8c5d40', CAST(N'2019-01-15T12:38:36.130' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (12, 2, N'خرید', 1, 0, 0, N'da24deea-1c98-4ad9-9363-13327e32eaec', CAST(N'2019-01-15T12:39:19.490' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (13, 2, N'برگشت از خرید و تخفیفات ', 1, 0, 0, N'e1b78a0b-e200-415a-818a-fce1048c5735', CAST(N'2019-01-15T12:40:04.470' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (14, 2, N'هزينه هاى عملياتى', 1, 0, 2, N'4bc26140-7c45-4e2a-96d5-5ccf6266f713', CAST(N'2019-01-15T12:40:41.230' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (15, 2, N'سایر هزينه ها و درآمد ها', 1, 0, 2, N'94299081-734d-4f47-a99c-ec7b9ae8580c', CAST(N'2019-01-15T12:41:03.997' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (16, 3, N'صندوق ', 1, 0, 2, N'df0b3009-6075-451f-a235-c26ffae7f06c', CAST(N'2019-01-15T12:45:09.350' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (17, 3, N'بانک ', 1, 0, 2, N'6ed87a7a-848a-4855-8862-4b8e7b7d3f97', CAST(N'2019-01-15T12:45:23.713' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (18, 3, N'اسناد دریافتنی ', 1, 0, 2, N'ad3c0eb5-3cf0-4369-af43-2b3fa514a850', CAST(N'2019-01-15T12:45:40.737' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (19, 3, N'اسناد پرداختنی', 1, 0, 2, N'c7ed884b-e4e5-40ce-86ed-a5c4d8b35323', CAST(N'2019-01-15T12:46:11.560' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (20, 3, N'اسناد دریافتنی تضمینی ', 1, 0, 2, N'2b287b0b-1211-44d7-8176-57d2ad21b3e5', CAST(N'2019-01-15T12:46:37.440' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (21, 3, N'اسناد پرداختنی تضمینی ', 1, 0, 2, N'e859c4d9-a385-415a-91e5-48b0efd9b966', CAST(N'2019-01-15T12:46:54.073' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (22, 3, N'اسناد درجریان وصول ', 1, 0, 2, N'fcfc7d6b-eda1-4aef-b0ce-ae6b38102dac', CAST(N'2019-01-15T12:47:06.490' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (23, 3, N'اسناد برگشتی ', 1, 0, 2, N'ebc53de1-f7a2-47b6-8e12-c597d757c4f0', CAST(N'2019-01-15T12:47:22.033' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (24, 3, N'تنخواه گردان ها ', 1, 0, 2, N'94bc42d6-d86c-432a-9657-4e7e98666eda', CAST(N'2019-01-15T12:47:54.697' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (25, 4, N'فروش ', 1, 0, 2, N'8c4e7347-736f-44f3-b7f8-20dffb73ade2', CAST(N'2019-01-15T12:48:23.877' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (26, 4, N'برگشت از فروش ', 1, 0, 2, N'99906d33-c220-4b91-9f3b-f9dee4da979d', CAST(N'2019-01-15T12:48:36.030' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (27, 4, N'خرید ', 1, 0, 0, N'40805c86-0018-4976-a0d2-0e2dcc820c62', CAST(N'2019-01-15T12:48:50.010' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (28, 4, N'برگشت از خرید ', 1, 0, 0, N'b7d8b020-058b-4487-9a18-2781eb62f75f', CAST(N'2019-01-15T12:49:02.973' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (29, 4, N'اضافات فاکتور فروش ', 1, 0, 2, N'88f8c1ce-d96f-49f2-8702-70402c6fcc29', CAST(N'2019-01-15T12:49:16.573' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (30, 4, N'اضافات فاکتور خرید', 1, 0, 2, N'c9b09d2f-25d9-4192-aa55-5cc6f492348f', CAST(N'2019-01-15T12:49:33.737' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (31, 4, N'بدهکاران تجاری ', 1, 2, 2, N'b85de96f-f48d-4208-bcd0-78e8bd9d2c44', CAST(N'2019-01-15T12:50:00.620' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (32, 4, N'بستانکاران تجاری ', 1, 2, 2, N'56d36c06-fe0c-4a88-84b6-09c4cee7c646', CAST(N'2019-01-15T12:50:32.900' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (33, 4, N'تخفیفات فروش ', 0, 0, 2, N'43eaf605-e1c4-48a6-b532-761dee9e64db', CAST(N'2019-01-15T12:50:51.310' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (34, 4, N'تخفیفات خرید ', 0, 0, 0, N'83b0e472-c21a-4800-b479-65b25d476716', CAST(N'2019-01-15T12:51:41.607' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (35, 4, N'قیمت تمام شده ', 0, 0, 2, N'a41051cf-3919-4eef-a9bb-f5e100afd6d0', CAST(N'2019-01-15T12:52:06.367' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (36, 4, N'فروشنده / خریدار  متفرقه ', 0, 1, 2, N'b20122c9-8a39-445d-bbe0-3f424c0529e2', CAST(N'2019-01-15T12:52:29.343' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (37, 4, N'مالیات پرداختنی ', 0, 0, 2, N'cb889886-cc01-4583-9ad2-267adfff47b2', CAST(N'2019-01-15T12:52:51.373' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (38, 4, N'عوارض پرداختنی', 0, 0, 2, N'60332921-671e-4f04-af3e-3c0b9ebadebd', CAST(N'2019-01-15T12:53:11.953' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (39, 4, N'مالیات دریافتنی', 0, 0, 2, N'c7a96c21-8160-417b-8185-7e25846d5247', CAST(N'2019-01-15T12:53:31.907' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (40, 4, N'عوارض دریافتنی', 0, 0, 2, N'521498a4-28c1-477d-89e6-95365e01e4fb', CAST(N'2019-01-15T12:53:55.050' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (41, 5, N'افتتاحیه ', 0, 1, 2, N'de1f3fb4-383f-4222-af54-62f3211b5d61', CAST(N'2019-01-15T12:54:36.443' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (42, 5, N'اختتامیه ', 0, 1, 2, N'7670e838-690b-4011-90e1-5b4370f932af', CAST(N'2019-01-15T12:54:51.660' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (43, 5, N'عملکرد ', 0, 1, 2, N'44c8cf51-a426-433d-a950-e8d62653f553', CAST(N'2019-01-15T12:55:11.510' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (44, 5, N'سود و زیان سال جاری', 0, 1, 2, N'3b1cab2a-7f71-4088-a35d-82fc44d42f44', CAST(N'2019-01-15T12:55:30.237' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (45, 5, N'سود و زیان انباشته ', 0, 1, 2, N'4119925a-e825-4a3a-b86f-1899c9ddaf33', CAST(N'2019-01-15T12:55:50.610' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (46, 6, N'موجودی کالا ', 0, 2, 2, N'af71d08f-dc59-4de7-a745-04563f083aa9', CAST(N'2019-01-15T12:56:51.717' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (47, 6, N'کنترل دستمزد', 0, 0, 2, N'67f35b34-d512-47cf-9602-f02d4b7d1cd1', CAST(N'2019-01-15T13:26:26.080' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (48, 6, N'کنترل سربار ', 0, 0, 2, N'082edb78-743f-4f6c-b70a-a989421ed780', CAST(N'2019-01-15T13:27:04.580' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (49, 7, N'اموال ', 0, 0, 2, N'70db4c8e-1763-4f92-a0fa-f478c2af259a', CAST(N'2019-01-15T13:27:42.067' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (50, 7, N'سود و زیان عملیات اموال', 0, 0, 2, N'05cd9415-9a94-42a2-9321-189eb3677e0d', CAST(N'2019-01-15T13:28:04.187' AS DateTime))
--GO
--INSERT [Finance].[AccountCollection] ([CollectionID], [CategoryID], [Name], [MultiSelect], [TypeLevel], [InventoryMode], [rowguid], [ModifiedDate]) VALUES (51, 7, N'اموال انتقالی ', 0, 0, 2, N'0058abb5-6531-44f7-b1a1-fa78c0d67a30', CAST(N'2019-01-15T13:28:52.073' AS DateTime))
--GO
--SET IDENTITY_INSERT [Finance].[AccountCollection] OFF
--GO

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
    [VoucherID]         INT              IDENTITY (1, 1) NOT NULL,
	[FiscalPeriodID]    INT              NOT NULL,
	[BranchID]          INT              NOT NULL,
    [DocumentID]        INT              NULL,
	[StatusID]          INT              NOT NULL,
    [CreatedByID]       INT              NOT NULL,
    [ModifiedByID]      INT              NOT NULL,
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

-- Insert suggested account groups...
--SET IDENTITY_INSERT [Finance].[AccountGroup] ON
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (1, N'داراييهاى جارى', 0, N'CategoryAsset', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (2, N'داراييهاى غيرجارى', 0, N'CategoryAsset', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (3, N'بدهيهاى جارى', 0, N'CategoryLiability', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (4, N'بدهيهاى غيرجارى', 0, N'CategoryLiability', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (5, N'حقوق صاحبان سرمايه', 0, N'CategoryCapital', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (6, N'فروش', 0, N'CategorySales', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (7, N'ساير درآمدها', 0, N'CategoryIncome', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (8, N'قيمت تمام شده كالاى فروش رفته', 1, N'CategoryExpense', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (9, N'خرید', -1, N'CategoryPurchase', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (10, N'هزينه هاى عملياتى', 0, N'CategoryExpense', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (11, N'هزينه هاى غيرعملياتى', 0, N'CategoryExpense', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (12, N'رابط', 0, N'CategoryAssociation', NULL)
--INSERT INTO [Finance].[AccountGroup] (GroupID, Name, InventoryMode, Category, Description) VALUES (13, N'حسابهاى انتظامى', 0, N'CategoryCoordination', NULL)
--SET IDENTITY_INSERT [Finance].[AccountGroup] OFF

-- Insert suggested account coding...
--SET IDENTITY_INSERT [Finance].[Account] ON
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (101, NULL, 1, 1, 1, 0, N'111', N'111', N'وجوه نقد', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (102, 101, 1, 1, 1, 0, N'001', N'111001', N'صندوقها', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (103, 102, 1, 1, 1, 0, N'0001', N'1110010001', N'صندوق', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (104, 101, 1, 1, 1, 0, N'002', N'111002', N'تنخواه گردانها', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (105, 101, 1, 1, 1, 0, N'003', N'111003', N'وجوه در راه', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (106, NULL, 1, 1, 1, 0, N'112', N'112', N'موجودى نزد بانكها', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (107, 106, 1, 1, 1, 0, N'001', N'112001', N'بانك ملى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (108, 106, 1, 1, 1, 0, N'002', N'112002', N'بانك صادرات', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (109, 106, 1, 1, 1, 0, N'003', N'112003', N'بانك تجارت', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (110, 106, 1, 1, 1, 0, N'004', N'112004', N'بانك ملت', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (111, 106, 1, 1, 1, 0, N'005', N'112005', N'بانك سپه', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (112, 106, 1, 1, 1, 0, N'006', N'112006', N'بانك مسكن', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (113, 106, 1, 1, 1, 0, N'007', N'112007', N'بانك رفاه كارگران', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (114, 106, 1, 1, 1, 0, N'008', N'112008', N'بانك كشاورزى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (115, 106, 1, 1, 1, 0, N'009', N'112009', N'بانک پاسارگاد', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (116, 106, 1, 1, 1, 0, N'010', N'112010', N'بانک پارسیان', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (117, 106, 1, 1, 1, 0, N'011', N'112011', N'بانک شهر', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (118, 106, 1, 1, 1, 0, N'012', N'112012', N'بانک اقتصاد نوین', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (119, NULL, 1, 1, 1, 0, N'113', N'113', N'سرمايه گذاريهاى كوتاه مدت', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (120, NULL, 1, 1, 1, 0, N'114', N'114', N'اسناد دريافتنى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (121, 120, 1, 1, 1, 0, N'001', N'114001', N'اسناد دريافتنى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (122, 120, 1, 1, 1, 0, N'002', N'114002', N'اسناد در جريان وصول', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (123, 120, 1, 1, 1, 0, N'003', N'114003', N'اسناد برگشتى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (124, NULL, 1, 1, 1, 0, N'115', N'115', N'حسابهاى دريافتنى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (125, 124, 1, 1, 1, 0, N'001', N'115001', N'بدهكاران تجارى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (126, NULL, 1, 1, 1, 0, N'116', N'116', N'ساير حسابها و اسناد دريافتنى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (127, 126, 1, 1, 1, 0, N'001', N'116001', N'وام كاركنان', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (128, 126, 1, 1, 1, 0, N'002', N'116002', N'مساعده كاركنان', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (129, 126, 1, 1, 1, 0, N'003', N'116003', N'جارى كاركنان', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (130, NULL, 1, 1, 1, 0, N'117', N'117', N'موجودى مواد و كالا', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (131, NULL, 1, 1, 1, 0, N'118', N'118', N'سپرده ها و پيش پرداختها', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (132, 131, 1, 1, 1, 0, N'001', N'118001', N'پيش پرداخت ماليات ارزش افزوده', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (133, 132, 1, 1, 1, 0, N'0001', N'1180010001', N'ماليات ارزش افزوده دريافتنى', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (134, 132, 1, 1, 1, 0, N'0002', N'1180010002', N'عوارض ارزش افزوده دريافتنى', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (135, NULL, 1, 1, 1, 0, N'119', N'119', N'سفارشات', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (136, NULL, 1, 1, 1, 0, N'151', N'151', N'داراييهاى ثابت مشهود', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (137, 136, 1, 1, 1, 0, N'001', N'151001', N'ساختمانها', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (138, 136, 1, 1, 1, 0, N'002', N'151002', N'ماشين آلات و تاسيسات', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (139, 136, 1, 1, 1, 0, N'003', N'151003', N'وسايط نقليه', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (140, 136, 1, 1, 1, 0, N'004', N'151004', N'اثاثه و منصوبات', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (141, 136, 1, 1, 1, 0, N'005', N'151005', N'تجهيزات', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (142, 136, 1, 1, 1, 0, N'006', N'151006', N'اموال انتقالى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (143, 136, 1, 1, 1, 0, N'007', N'151007', N'استهلاك انباشته', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (144, NULL, 1, 1, 1, 0, N'152', N'152', N'سرمايه گذاريهاى بلند مدت', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (145, NULL, 1, 1, 1, 0, N'153', N'153', N'ساير داراييها', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (146, NULL, 1, 1, 1, 0, N'211', N'211', N'اسناد پرداختنى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (147, 146, 1, 1, 1, 0, N'001', N'211001', N'اسناد پرداختنى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (148, NULL, 1, 1, 1, 0, N'212', N'212', N'حسابهاى پرداختنى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (149, 148, 1, 1, 1, 0, N'001', N'212001', N'بستانكاران تجارى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (150, NULL, 1, 1, 1, 0, N'213', N'213', N'ساير حسابها و اسناد پرداختنى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (151, 150, 1, 1, 1, 0, N'001', N'213001', N'بيمه پرداختنى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (152, 151, 1, 1, 1, 0, N'0001', N'2130010001', N'بيمه تامين اجتماعى شعبه ....', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (153, 150, 1, 1, 1, 0, N'002', N'213002', N'ماليات پرداختنى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (154, 153, 1, 1, 1, 0, N'0001', N'2130020001', N'ماليات حقوق', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (155, 153, 1, 1, 1, 0, N'0002', N'2130020002', N'ماليات عملكرد', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (156, 153, 1, 1, 1, 0, N'0003', N'2130020003', N'ماليات تكليفى', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (157, 153, 1, 1, 1, 0, N'0004', N'2130020004', N'ماليات ارزش افزوده پرداختنى', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (158, 153, 1, 1, 1, 0, N'0005', N'2130020005', N'عوارض ارزش افزوده پرداختنى', 2)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (159, 150, 1, 1, 1, 0, N'003', N'213003', N'حقوق پرداختنى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (160, NULL, 1, 1, 1, 0, N'214', N'214', N'پيش دريافتها', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (161, NULL, 1, 1, 1, 0, N'215', N'215', N'ذخيره ماليات', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (162, NULL, 1, 1, 1, 0, N'216', N'216', N'سود سهام پيشنهادى و پرداختنى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (163, NULL, 1, 1, 1, 0, N'217', N'217', N'تسهيلات و اعتبارات مالى دريافتى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (164, NULL, 1, 1, 1, 0, N'251', N'251', N'اسناد پرداختنى بلند مدت', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (165, NULL, 1, 1, 1, 0, N'252', N'252', N'حسابهاى پرداختنى بلند مدت', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (166, NULL, 1, 1, 1, 0, N'253', N'253', N'تسهيلات مالى دريافتى بلند مدت', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (167, NULL, 1, 1, 1, 0, N'254', N'254', N'ذخيره مزاياى پايان خدمت كاركنان', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (168, NULL, 1, 1, 1, 0, N'311', N'311', N'سرمايه', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (169, NULL, 1, 1, 1, 0, N'312', N'312', N'اندوخته قانونى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (170, NULL, 1, 1, 1, 0, N'313', N'313', N'ساير اندوخته ها', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (171, NULL, 1, 1, 1, 0, N'314', N'314', N'مازاد تجديد ارزيابى داراييهاى ثابت', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (172, NULL, 1, 1, 1, 0, N'315', N'315', N'سود( زيان )انباشته', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (173, NULL, 1, 1, 1, 0, N'411', N'411', N'فروش', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (174, 173, 1, 1, 1, 0, N'001', N'411001', N'فروش', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (175, NULL, 1, 1, 1, 0, N'412', N'412', N'برگشت از فروش و تخفيفات', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (176, 175, 1, 1, 1, 0, N'001', N'412001', N'برگشت از فروش', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (177, 175, 1, 1, 1, 0, N'002', N'412002', N'تخفيفات فروش', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (178, NULL, 1, 1, 1, 0, N'451', N'451', N'ساير درآمدها', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (179, 178, 1, 1, 1, 0, N'001', N'451001', N'سود سپرده بانكى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (180, 178, 1, 1, 1, 0, N'002', N'451002', N'درآمد متفرقه', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (181, NULL, 1, 1, 1, 0, N'452', N'452', N'سود و زيان عمليات اموال', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (182, NULL, 1, 1, 1, 0, N'551', N'551', N'قيمت تمام شده كالاى فروش رفته', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (183, NULL, 1, 1, 1, 0, N'552', N'552', N'كنترل دستمزد و سربار', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (184, 183, 1, 1, 1, 0, N'001', N'552001', N'كنترل دستمزد', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (185, 183, 1, 1, 1, 0, N'002', N'552002', N'كنترل سربار', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (186, NULL, 1, 1, 1, 0, N'610', N'610', N'هزينه هاى عمومى و اداری', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (187, 186, 1, 1, 1, 0, N'001', N'610001', N'هزینه آبدارخانه و پذيرايى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (188, 186, 1, 1, 1, 0, N'002', N'610002', N'هزینه اياب و ذهاب', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (189, 186, 1, 1, 1, 0, N'003', N'610003', N'هزینه ملزومات مصرفى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (190, 186, 1, 1, 1, 0, N'004', N'610004', N'هزینه پيك و آژانس', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (191, 186, 1, 1, 1, 0, N'005', N'610005', N'هزینه شارژ ماهيانه', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (192, 186, 1, 1, 1, 0, N'006', N'610006', N'هزینه تعمير و نگهدارى اثاثه و تجهيزات ادارى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (193, 186, 1, 1, 1, 0, N'007', N'610007', N'هزینه استهلاك اثاثه و تجهيزات ادارى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (194, NULL, 1, 1, 1, 0, N'611', N'611', N'هزينه هاى حقوق و دستمزد', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (195, 194, 1, 1, 1, 0, N'001', N'611001', N'هزینه حقوق پايه', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (196, 194, 1, 1, 1, 0, N'002', N'611002', N'هزینه اضافه كارى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (197, 194, 1, 1, 1, 0, N'003', N'611003', N'هزینه حق مسكن و خواربار', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (198, 194, 1, 1, 1, 0, N'004', N'611004', N'هزینه حق بن', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (199, 194, 1, 1, 1, 0, N'005', N'611005', N'هزینه حق اولاد', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (200, 194, 1, 1, 1, 0, N'006', N'611006', N'هزینه بيمه سهم كارفرما', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (201, 194, 1, 1, 1, 0, N'007', N'611007', N'هزینه عيدى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (202, 194, 1, 1, 1, 0, N'008', N'611008', N'هزینه سنوات', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (203, 194, 1, 1, 1, 0, N'009', N'611009', N'هزینه بازخريد مرخصى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (204, 194, 1, 1, 1, 0, N'010', N'611010', N'هزینه پاداش', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (205, 194, 1, 1, 1, 0, N'011', N'611011', N'هزینه كسركار', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (206, NULL, 1, 1, 1, 0, N'612', N'612', N'هزينه هاى عملياتى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (207, 206, 1, 1, 1, 0, N'001', N'612001', N'هزینه تعمير و نگهدارى ماشين آلات', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (208, 206, 1, 1, 1, 0, N'002', N'612002', N'هزینه استهلاك ماشين آلات', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (209, NULL, 1, 1, 1, 0, N'613', N'613', N'هزينه هاى توزيع و فروش', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (210, 209, 1, 1, 1, 0, N'001', N'613001', N'هزینه هدايا و كمكها', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (211, 209, 1, 1, 1, 0, N'002', N'613002', N'هزینه چاپ و تبليغات', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (212, 209, 1, 1, 1, 0, N'003', N'613003', N'هزینه پورسانت فروش', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (213, NULL, 1, 1, 1, 0, N'614', N'614', N'هزينه هاى مالى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (214, 213, 1, 1, 1, 0, N'001', N'614001', N'هزينه بهره تسهيلات بانكى', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (215, 213, 1, 1, 1, 0, N'002', N'614002', N'هزینه کارمزد بانکی', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (216, 213, 1, 1, 1, 0, N'003', N'614003', N'هزینه صدور دسته چک', 1)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (217, NULL, 1, 1, 1, 0, N'711', N'711', N'افتتاحيه', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (218, NULL, 1, 1, 1, 0, N'712', N'712', N'اختتاميه', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (219, NULL, 1, 1, 1, 0, N'713', N'713', N'عملكرد', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (220, NULL, 1, 1, 1, 0, N'714', N'714', N'سود و زيان', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (221, NULL, 1, 1, 1, 0, N'811', N'811', N'حسابهاى انتظامى', 0)
--INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, BranchScope, Code, FullCode, Name, [Level])
--    VALUES (222, NULL, 1, 1, 1, 0, N'812', N'812', N'طرف حسابهاى انتظامى', 0)
--SET IDENTITY_INSERT [Finance].[Account] OFF


SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO
