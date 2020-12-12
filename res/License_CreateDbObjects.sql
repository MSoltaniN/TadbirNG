USE [master]
GO

CREATE DATABASE [NGLicense]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NGLicense', FILENAME = N'C:\SqlDb\NGLIcense.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NGLicense_log', FILENAME = N'C:\SqlDb\NGLicense_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [NGLicense] SET COMPATIBILITY_LEVEL = 120
GO

USE [NGLicense]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[CustomerID]       INT           IDENTITY(1,1) NOT NULL,
	[CustomerKey]      CHAR(36)      NULL,
	[CompanyName]      NVARCHAR(64)  NOT NULL,
	[Industry]         NVARCHAR(64)  NOT NULL,
	[EmployeeCount]    NVARCHAR(64)  NOT NULL,
	[MainAddress]      NVARCHAR(512) NOT NULL,
	[ContactFirstName] NVARCHAR(64)  NOT NULL,
	[ContactLastName]  NVARCHAR(64)  NOT NULL,
	[WorkPhone]        NVARCHAR(16)  NULL,
	[WorkFax]          NVARCHAR(16)  NOT NULL,
	[CellPhone]        NVARCHAR(16)  NOT NULL,
    [RowGuid]          UNIQUEIDENTIFIER CONSTRAINT [DF_Customer_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]     DATETIME         CONSTRAINT [DF_Customer_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[License](
	[LicenseID]     INT          IDENTITY(1,1) NOT NULL,
	[CustomerID]    INT          NOT NULL,
	[CustomerKey]   CHAR(36)     NULL,
	[LicenseKey]    CHAR(36)     NOT NULL,
	[HardwareKey]   VARCHAR(256) NULL,
	[ClientKey]     VARCHAR(512) NULL,
	[Secret]        VARCHAR(32)  NULL,
	[UserCount]     INT          NOT NULL,
	[Edition]       VARCHAR(32)  NOT NULL,
	[StartDate]     DATETIME     NOT NULL,
	[EndDate]       DATETIME     NOT NULL,
	[ActiveModules] INT          NOT NULL,
	[IsActivated]   BIT          CONSTRAINT [DF_License_IsActivated] DEFAULT (0) NOT NULL,
    [RowGuid]       UNIQUEIDENTIFIER CONSTRAINT [DF_License_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_License_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_License] PRIMARY KEY CLUSTERED ([LicenseID] ASC)
    , CONSTRAINT [FK_License_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID])
) ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS OFF
GO
