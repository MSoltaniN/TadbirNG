USE [master]
GO

CREATE DATABASE [NGLicense]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NGLicense', FILENAME = N'NGLicense.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NGLicense_log', FILENAME = N'NGLicense_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [NGLicense] SET COMPATIBILITY_LEVEL = 120
GO

ALTER AUTHORIZATION ON DATABASE::NGLicense TO NgTadbirUser;
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
	[OfflineLimit]  INT          CONSTRAINT [DF_License_OfflineLimit] DEFAULT (0) NOT NULL,
    [RowGuid]       UNIQUEIDENTIFIER CONSTRAINT [DF_License_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_License_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_License] PRIMARY KEY CLUSTERED ([LicenseID] ASC)
    , CONSTRAINT [FK_License_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID])
) ON [PRIMARY]
GO

-- Temporary data for testing purposes...
SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT [dbo].[Customer] ([CustomerID], [CustomerKey], [CompanyName], [Industry], [EmployeeCount], [MainAddress], [ContactFirstName], [ContactLastName], [WorkPhone], [WorkFax], [CellPhone])
    VALUES (1, N'7b9782da-8558-4243-a6de-3c61a3278c4c', N'پردازش موازی سامان', N'تولید نرم افزار', N'بیشتر از صد نفر', N'میدان ولی عصر - ساختمان تجارت ایرانیان - طبقه 2 - واحد 12', N'بهزاد', N'مقصودی', N'02188928030', N'02188928032', N'0016472742566')
INSERT [dbo].[Customer] ([CustomerID], [CustomerKey], [CompanyName], [Industry], [EmployeeCount], [MainAddress], [ContactFirstName], [ContactLastName], [WorkPhone], [WorkFax], [CellPhone])
    VALUES (2, N'32f700af-493e-4a46-a191-2acb333bc02e', N'صنایع چوب و کاغذ چوکا', N'جنگلداری - چوب و کاغذ', N'بیشتر از صد نفر', N'تهران - صندوق پستی', N'نادر', N'قربانی', N'02122456789', N'02122789456', N'09121357924')
INSERT [dbo].[Customer] ([CustomerID], [CustomerKey], [CompanyName], [Industry], [EmployeeCount], [MainAddress], [ContactFirstName], [ContactLastName], [WorkPhone], [WorkFax], [CellPhone])
    VALUES (3, N'b247dc03-a649-4c49-ba1c-48cd527e02ab', N'فراورده های پالایشگاهی نفت بهران', N'پالایشگاه نفت و گاز', N'بیشتر از صد نفر', N'تهران - صندوق پستی 9876', N'سیمین', N'رستگاری', N'', N'02122567890', N'09124567890')
SET IDENTITY_INSERT [dbo].[Customer] OFF

SET IDENTITY_INSERT [dbo].[License] ON
INSERT [dbo].[License] ([LicenseID], [CustomerID], [CustomerKey], [LicenseKey], [UserCount], [Edition], [StartDate], [EndDate], [ActiveModules], [IsActivated])
    VALUES (1, 1, N'7b9782da-8558-4243-a6de-3c61a3278c4c', N'3dfde58e-2dd9-48cc-b024-5f878d60355d', 10, N'Enterprise', '2021-09-01', '2022-09-01', 1023, 0)
INSERT [dbo].[License] ([LicenseID], [CustomerID], [CustomerKey], [LicenseKey], [UserCount], [Edition], [StartDate], [EndDate], [ActiveModules], [IsActivated])
    VALUES (2, 2, N'32f700af-493e-4a46-a191-2acb333bc02e', N'c66d116a-71fb-4515-a578-4a7f19248a86', 40, N'Enterprise', '2020-11-18', '2021-11-18', 511, 0)
INSERT [dbo].[License] ([LicenseID], [CustomerID], [CustomerKey], [LicenseKey], [UserCount], [Edition], [StartDate], [EndDate], [ActiveModules], [IsActivated])
    VALUES (3, 3, N'b247dc03-a649-4c49-ba1c-48cd527e02ab', N'77d7095d-1ea3-474d-8b3d-b2b4e2e6cd31', 25, N'Professional', '2020-12-01', '2021-12-01', 511, 0)
INSERT [dbo].[License] ([LicenseID], [CustomerID], [CustomerKey], [LicenseKey], [UserCount], [Edition], [StartDate], [EndDate], [ActiveModules], [IsActivated])
    VALUES (4, 1, N'7b9782da-8558-4243-a6de-3c61a3278c4c', N'74f62ef4-b3f3-4141-89bb-1633baf2fa7b', 25, N'Standard', '2021-09-05', '2022-09-05', 31, 0)
SET IDENTITY_INSERT [dbo].[License] OFF

SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS OFF
GO
