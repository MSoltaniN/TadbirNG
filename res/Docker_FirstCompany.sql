USE master
GO

CREATE DATABASE NGTadbir
GO

ALTER AUTHORIZATION ON DATABASE::NGTadbir TO NgTadbirUser;
GO

USE NGTadbirSys
GO

SET IDENTITY_INSERT [Config].[CompanyDb] ON
INSERT INTO [Config].[CompanyDb] ([CompanyID], [Name], [DbName], [Server], [IsActive])
VALUES (1, N'شرکت نمونه', 'NGTadbir', 'DbServer', 1)
SET IDENTITY_INSERT [Config].[CompanyDb] OFF

-- The following commands can be removed, but are now necessary because Angular client has a bug that looks for non-existing FiscalPeriodID = 1
USE NGTadbir
GO

INSERT INTO [Corporate].[Branch] ([CompanyID], [Name], [Level])
VALUES (1, N'دفتر مرکزی', 0)

INSERT INTO [Finance].[FiscalPeriod] ([CompanyID], [Name], [StartDate], [EndDate], [InventoryMode])
VALUES (1, N'سال 1400', '2021-03-21', '2022-03-20', 1)
