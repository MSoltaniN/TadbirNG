USE [TadbirDemo]
GO

/****** Object: Table [dbo].[User] Script Date: 2017-02-15 2:44:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE SCHEMA [Auth]
GO

CREATE SCHEMA [Contact]
GO

CREATE SCHEMA [Corporate]
GO

CREATE SCHEMA [Finance]
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
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Corporate_Branch_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Corporate_Branch_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Corporate_Branch] PRIMARY KEY CLUSTERED ([BranchID] ASC)
    , CONSTRAINT [FK_Corporate_Branch_Corporate_Company] FOREIGN KEY ([CompanyID]) REFERENCES [Corporate].[Company] ([CompanyID])
)
GO

CREATE TABLE [Finance].[FiscalPeriod] (
    [FiscalPeriodID]   INT              IDENTITY (1, 1) NOT NULL,
	[BranchID]         INT              NOT NULL,
    [Name]             NVARCHAR(64)     NOT NULL,
    [StartDate]        DATETIME         NOT NULL,
    [EndDate]          DATETIME         NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_FiscalPeriod_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_FiscalPeriod_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_FiscalPeriod] PRIMARY KEY CLUSTERED ([FiscalPeriodID] ASC)
    , CONSTRAINT [FK_Finance_FiscalPeriod_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
)
GO

CREATE TABLE [Finance].[Account] (
    [AccountID]      INT              IDENTITY (1, 1) NOT NULL,
	[FiscalPeriodID] INT              NOT NULL,
    [Code]           NVARCHAR(512)    NOT NULL,
    [Name]           NVARCHAR(512)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Account_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_Account_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Account] PRIMARY KEY CLUSTERED ([AccountID] ASC)
    , CONSTRAINT [FK_Finance_Account_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
)
GO

CREATE TABLE [Finance].[Transaction] (
    [TransactionID]   INT              IDENTITY (1, 1) NOT NULL,
	[FiscalPeriodID]  INT              NOT NULL,
	[CreatorID]       INT              NOT NULL,
	[ModifierID]      INT              NOT NULL,
	[VerifierID]      INT              NULL,
	[ApproverID]      INT              NULL,
    [No]              NVARCHAR(64)     NOT NULL,
    [Date]            DATETIME         NOT NULL,
    [Description]     NVARCHAR(512)    NULL,
    [Status]          NVARCHAR(64)     NOT NULL,
    [IsVerified]      BIT              NOT NULL,
    [IsApproved]      BIT              NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Transaction_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_Transaction_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Transaction] PRIMARY KEY CLUSTERED ([TransactionID] ASC)
    , CONSTRAINT [FK_Finance_Transaction_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
    , CONSTRAINT [FK_Finance_Transaction_Auth_User_Creator] FOREIGN KEY ([CreatorID]) REFERENCES [Auth].[User] ([UserID])
    , CONSTRAINT [FK_Finance_Transaction_Auth_User_Modifier] FOREIGN KEY ([ModifierID]) REFERENCES [Auth].[User] ([UserID])
    , CONSTRAINT [FK_Finance_Transaction_Auth_User_Verifier] FOREIGN KEY ([VerifierID]) REFERENCES [Auth].[User] ([UserID])
    , CONSTRAINT [FK_Finance_Transaction_Auth_User_Approver] FOREIGN KEY ([ApproverID]) REFERENCES [Auth].[User] ([UserID])
)
GO

CREATE TABLE [Finance].[TransactionLine] (
    [LineID]        INT              IDENTITY (1, 1) NOT NULL,
	[TransactionID] INT              NOT NULL,
	[AccountID]     INT              NOT NULL,
	[CurrencyID]    INT              NOT NULL,
    [Description]   NVARCHAR(512)    NULL,
    [Debit]         MONEY            NOT NULL,
    [Credit]        MONEY            NOT NULL,
    [rowguid]       UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_TransactionLine_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_Finance_TransactionLine_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_TransactionLine] PRIMARY KEY CLUSTERED ([LineID] ASC)
    , CONSTRAINT [FK_Finance_TransactionLine_Finance_Transaction] FOREIGN KEY ([TransactionID]) REFERENCES [Finance].[Transaction] ([TransactionID])
    , CONSTRAINT [FK_Finance_TransactionLine_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
    , CONSTRAINT [FK_Finance_TransactionLine_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency] ([CurrencyID])
)
GO

-- Create system records for security (NOTE: These records will be migrated to SYS database in a later stage)

-- admin user is added with password 'Admin@Tadbir1395'
SET IDENTITY_INSERT [Auth].[User] ON
INSERT INTO [Auth].[User] (UserID, UserName, PasswordHash, IsEnabled) VALUES (1, N'admin', '5ab4a25e31220c3b103aef3e32596211b90238a0d5933288efbd36c5154b82ff', 1)
SET IDENTITY_INSERT [Auth].[User] OFF

SET IDENTITY_INSERT [Auth].[Role] ON
INSERT INTO [Auth].[Role] (RoleID, Name, [Description]) VALUES (1, N'راهبر سیستم', N'این نقش دارای کلیه دسترسی های تعریف شده در برنامه بوده و قابل اصلاح یا حذف نیست.')
SET IDENTITY_INSERT [Auth].[Role] OFF

SET IDENTITY_INSERT [Auth].[UserRole] ON
INSERT INTO [Auth].[UserRole] (UserRoleID, UserID, RoleID) VALUES (1, 1, 1)
SET IDENTITY_INSERT [Auth].[UserRole] OFF

SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (1, N'مدیریت سرفصل های مالی', N'Account')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (2, N'مدیریت اسناد مالی', N'Transaction')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (3, N'مدیریت کاربران', N'User')
INSERT INTO [Auth].[PermissionGroup] (PermissionGroupID, Name, EntityName) VALUES (4, N'مدیریت نقش ها', N'Role')
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
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (9, 3, N'مشاهده کاربران', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (10, 3, N'ایجاد کاربر', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (11, 3, N'اصلاح کاربر', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (12, 4, N'مشاهده نقش ها', 1)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (13, 4, N'ایجاد نقش', 2)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (14, 4, N'اصلاح نقش', 4)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (15, 4, N'حذف نقش', 8)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (16, 4, N'تخصیص نقش به کاربر', 16)
INSERT INTO [Auth].[Permission] (PermissionID, GroupID, Name, Flag) VALUES (17, 4, N'حذف کاربر از نقش', 32)
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
SET IDENTITY_INSERT [Auth].[RolePermission] OFF

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

