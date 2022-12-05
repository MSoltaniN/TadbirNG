USE master
GO

CREATE DATABASE [ChallengeDb]
GO

ALTER DATABASE [ChallengeDb] SET COMPATIBILITY_LEVEL = 130
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE=OFF
GO

USE [ChallengeDb]
GO

CREATE SCHEMA [Metadata]
GO

CREATE SCHEMA [Core]
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

CREATE TABLE [Core].[School] (
    [SchoolID]       INT              IDENTITY (1, 1) NOT NULL,
    [CityID]         INT              NOT NULL,
    [ProvinceID]     INT              NOT NULL,
    [Name]           NVARCHAR(64)     NOT NULL,
    [AdminSystem]    NVARCHAR(32)     NOT NULL,
    [Manager]        NVARCHAR(32)     NULL,
    [Capacity]       INT              NOT NULL,
    [Tuition]        MONEY            NOT NULL,
    [Address]        NVARCHAR(256)    NULL,
    [FoundedDate]    DATETIME         NOT NULL,
    [IsListed]       BIT              NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Core_School_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Core_School_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_School] PRIMARY KEY CLUSTERED ([SchoolID] ASC)
    , CONSTRAINT [FK_Core_School_Metadata_City] FOREIGN KEY ([CityID]) REFERENCES [Metadata].[City]([CityID])
    , CONSTRAINT [FK_Core_School_Metadata_Province] FOREIGN KEY ([ProvinceID]) REFERENCES [Metadata].[Province]([ProvinceID])
)
GO
