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
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Auth_User_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Auth_User_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Auth_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
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
    [Name]             NVARCHAR(64)     NOT NULL,
    [StartDate]        DATETIME         NOT NULL,
    [EndDate]          DATETIME         NOT NULL,
    [Description]      NVARCHAR(512)    NULL,
    [rowguid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_FiscalPeriod_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Finance_FiscalPeriod_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_FiscalPeriod] PRIMARY KEY CLUSTERED ([FiscalPeriodID] ASC)
)
GO

CREATE TABLE [Finance].[Account] (
    [AccountID]      INT              IDENTITY (1, 1) NOT NULL,
	[BranchID]       INT              NOT NULL,
	[FiscalPeriodID] INT              NOT NULL,
    [Code]           NVARCHAR(512)    NOT NULL,
    [Name]           NVARCHAR(512)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Account_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Finance_Account_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Account] PRIMARY KEY CLUSTERED ([AccountID] ASC)
    , CONSTRAINT [FK_Finance_Account_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
    , CONSTRAINT [FK_Finance_Account_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
)
GO

CREATE TABLE [Finance].[Transaction] (
    [TransactionID]   INT              IDENTITY (1, 1) NOT NULL,
	[BranchID]        INT              NOT NULL,
	[FiscalPeriodID]  INT              NOT NULL,
	[CreatorID]       INT              NOT NULL,
	[ModifierID]      INT              NOT NULL,
	[VerifierID]      INT              NOT NULL,
	[ApproverID]      INT              NOT NULL,
    [No]              NVARCHAR(64)     NOT NULL,
    [Date]            DATETIME         NOT NULL,
    [Description]     NVARCHAR(512)    NULL,
    [Status]          NVARCHAR(64)     NOT NULL,
    [IsVerified]      BIT              NOT NULL,
    [IsApproved]      BIT              NOT NULL,
    [rowguid]         UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_Transaction_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]    DATETIME         CONSTRAINT [DF_Finance_Transaction_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_Transaction] PRIMARY KEY CLUSTERED ([TransactionID] ASC)
    , CONSTRAINT [FK_Finance_Transaction_Corporate_Branch] FOREIGN KEY ([BranchID]) REFERENCES [Corporate].[Branch] ([BranchID])
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

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

