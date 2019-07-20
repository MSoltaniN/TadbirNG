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

