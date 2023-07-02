using System;
using System.IO;
using SPPC.Tadbir.Common;

namespace SPPC.Tools.Model
{
    public class EnvSetupParameters
    {
        public EnvSetupParameters(EnvSetupWizardModel model)
        {
            _model = model;
        }

        public string WebApiSettings
        {
            get
            {
                return Path.Combine(_model.RootFolder, "src", "TadbirNG", "SPPC.Tadbir.Web.Api",
                    "appSettings.Development.json");
            }
        }

        public string LicenseApiSettings
        {
            get
            {
                return Path.Combine(_model.RootFolder, "src", "TadbirNG", "SPPC.Licensing.Web",
                    "appSettings.Development.json");
            }
        }

        public string LocalLicenseApiSettings
        {
            get
            {
                return Path.Combine(_model.RootFolder, "src", "TadbirNG", "SPPC.Licensing.Local.Web",
                    "appSettings.Development.json");
            }
        }

        public string AngularEnvPath
        {
            get
            {
                return Path.Combine(_model.RootFolder, "src", "TadbirNG", "SPPC.Tadbir.Web",
                    "ClientApp", "src", "environments", "environment.ts");
            }
        }

        public string WebApiRootPath
        {
            get
            {
                return Path.Combine(_model.RootFolder,
                    "src", "TadbirNG", "SPPC.Tadbir.Web.Api", "wwwroot");
            }
        }

        public string SystemDbScript
        {
            get
            {
                return Path.Combine(_model.RootFolder, "res", ScriptConstants.SysDbCreateScript);
            }
        }

        public string SystemDbTriggers
        {
            get
            {
                return Path.Combine(_model.RootFolder, "res", "TadbirSys_CreateTriggers.sql");
            }
        }

        public string SystemDbJobs
        {
            get
            {
                return Path.Combine(_model.RootFolder, "res", "TadbirSys_CreateJobs.sql");
            }
        }

        public string SystemDataDbScript
        {
            get
            {
                return Path.Combine(_model.RootFolder, "res", "SampleData", "TadbirSys_TestData.sql");
            }
        }

        public string SampleDbScript
        {
            get
            {
                return Path.Combine(_model.RootFolder, "res", ScriptConstants.DbCreateScript);
            }
        }

        public string SampleDataDbScript
        {
            get
            {
                return Path.Combine(_model.RootFolder, "res", "SampleData", "Tadbir_TestData.sql");
            }
        }

        public static string CreateLoginAndLicenseScript
        {
            get { return _createLoginAndLicenseScript; }
        }

        public static string CreateLicenseDbScript
        {
            get { return _createLicenseDbScript; }
        }

        public static string InsertDevLicenseScript
        {
            get { return _insertDevLicenseScript; }
        }

        public static string QueryExistingKeyScript
        {
            get { return _queryExistingKeyScript; }
        }

        public static string QueryExistingDatabase
        {
            get { return _queryExistingDatabase; }
        }

        public static string QueryLicenseActivation
        {
            get { return _queryLicenseActivation; }
        }

        public static string QueryServerJob
        {
            get { return _queryServerJob; }
        }

        public static string CreateSampleInitScript
        {
            get { return _createSampleInitScript; }
        }

        public static string LocalServerUrl
        {
            get { return "http://localhost:7473"; }
        }

        public string Connection
        {
            get
            {
                return String.Format("Server={0};Database=master;User ID={1};Password={2};Trusted_Connection=False",
                    _model.DbServerName, _model.DbUserName, _model.DbPassword);
            }
        }

        private readonly EnvSetupWizardModel _model;
        private static readonly string _createLoginAndLicenseScript = @"
IF NOT EXISTS(
SELECT [sid]
FROM [sys].[syslogins]
WHERE [name] = 'NgTadbirUser')
BEGIN
  CREATE LOGIN [NgTadbirUser]
  WITH PASSWORD = 'Demo1234',
  DEFAULT_DATABASE = master,
  CHECK_POLICY = OFF,
  CHECK_EXPIRATION = OFF;

  ALTER SERVER ROLE securityadmin ADD MEMBER NgTadbirUser;

  ALTER SERVER ROLE dbcreator ADD MEMBER NgTadbirUser;

  ALTER SERVER ROLE sysadmin ADD MEMBER NgTadbirUser;
END

IF NOT EXISTS(
SELECT [database_id]
FROM [sys].[databases]
WHERE [name] = 'NGLicense')
BEGIN
  CREATE DATABASE [NGLicense]

  ALTER DATABASE [NGLicense] SET COMPATIBILITY_LEVEL = 130

  ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE=OFF

  ALTER AUTHORIZATION ON DATABASE::NGLicense TO NgTadbirUser;
END";
        private static readonly string _createLicenseDbScript = @"
USE [NGLicense]

IF NOT EXISTS(
SELECT [name]
FROM [sys].[tables]
WHERE [name] = 'Customer')
BEGIN

SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

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

SET ANSI_NULLS OFF

SET QUOTED_IDENTIFIER OFF

END";
        private static readonly string _insertDevLicenseScript = @"
USE [NGLicense]
GO

IF NOT EXISTS(
SELECT [CustomerID]
FROM [dbo].[Customer]
WHERE [CustomerID] = 1)
BEGIN
SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT [dbo].[Customer] ([CustomerID], [CustomerKey], [CompanyName], [Industry], [EmployeeCount], [MainAddress], [ContactFirstName], [ContactLastName], [WorkPhone], [WorkFax], [CellPhone])
    VALUES (1, N'{0}', N'تیم توسعه تدبیر وب', N'تولید نرم افزار', N'ده تا بیست نفر', N'(نامشخص)', N'{1}', N'{2}', N'02112345678', N'02112345678', N'1234567890')
SET IDENTITY_INSERT [dbo].[Customer] OFF
END

IF NOT EXISTS(
SELECT [LicenseID]
FROM [dbo].[License]
WHERE [LicenseID] = 1)
BEGIN
SET IDENTITY_INSERT [dbo].[License] ON
INSERT [dbo].[License] ([LicenseID], [CustomerID], [CustomerKey], [LicenseKey], [UserCount], [Edition], [StartDate], [EndDate], [ActiveModules], [IsActivated])
    VALUES (1, 1, N'{0}', N'{3}', 10, N'Enterprise', '{4}', '{5}', 1023, 0)
SET IDENTITY_INSERT [dbo].[License] OFF
END";
        private static readonly string _queryExistingKeyScript = @"
USE [NGLicense]

SELECT [LicenseKey], [CustomerKey]
FROM [dbo].[License]
WHERE [LicenseID] = 1";
        private static readonly string _queryExistingDatabase = @"
SELECT [database_id]
FROM [sys].[databases]
WHERE [name] = '{0}'";
        private static readonly string _queryLicenseActivation = @"
USE [NGLicense]

SELECT [IsActivated]
FROM [dbo].[License]
WHERE [LicenseID] = 1";
        private static readonly string _queryServerJob = @"
SELECT [job_id] 
FROM [msdb].[dbo].[sysjobs_view] 
WHERE [name] = N'CleanUp_CloseExpiredSessions'";
        private static readonly string _createSampleInitScript = @"
CREATE DATABASE NGTadbir
GO

ALTER DATABASE [NGTadbir] SET COMPATIBILITY_LEVEL = 130
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE=OFF
GO

ALTER AUTHORIZATION ON DATABASE::NGTadbir TO NgTadbirUser;
GO

USE [NGTadbir]
GO
";
    }
}
