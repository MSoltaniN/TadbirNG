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

USE NGTadbir
GO
