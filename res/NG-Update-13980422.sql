USE [NGTadbir]
GO

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

