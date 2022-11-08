USE master
GO

CREATE DATABASE [@FirstDbName]
GO

ALTER DATABASE [@FirstDbName] SET COMPATIBILITY_LEVEL = 130
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE=OFF
GO

ALTER AUTHORIZATION ON DATABASE::@FirstDbName TO @LoginName;
GO

USE @SysDbName
GO

SET IDENTITY_INSERT [Config].[CompanyDb] ON
INSERT INTO [Config].[CompanyDb] ([CompanyID], [Name], [DbName], [Server], [IsActive])
VALUES (1, N'@FirstCompanyName', '@FirstDbName', '@DbServerName', 1)
SET IDENTITY_INSERT [Config].[CompanyDb] OFF
