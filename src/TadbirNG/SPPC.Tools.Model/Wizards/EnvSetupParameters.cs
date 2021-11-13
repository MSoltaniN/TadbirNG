using System;
using System.IO;

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
                return Path.Combine(_model.RootFolder, "res", "TadbirSys_CreateDbObjects.sql");
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
                return Path.Combine(_model.RootFolder, "res", "Tadbir_CreateDbObjects.sql");
            }
        }

        public string SampleDataDbScript
        {
            get
            {
                return Path.Combine(_model.RootFolder, "res", "SampleData", "Tadbir_TestData.sql");
            }
        }

        public static string CreateLicenseDbScript
        {
            get { return _createLicenseDbScript; }
        }

        public static string InsertDevLicenseScript
        {
            get { return _insertDevLicenseScript; }
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
        private static readonly string _createLicenseDbScript = @"
USE master;
GO

CREATE LOGIN [NgTadbirUser]
WITH PASSWORD = 'Demo1234',
DEFAULT_DATABASE = master,
CHECK_POLICY = OFF,
CHECK_EXPIRATION = OFF;
GO

ALTER SERVER ROLE securityadmin ADD MEMBER NgTadbirUser;
GO

ALTER SERVER ROLE dbcreator ADD MEMBER NgTadbirUser;
GO

CREATE DATABASE [NGLicense]
GO

ALTER DATABASE [NGLicense] SET COMPATIBILITY_LEVEL = 130
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE=OFF
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
    [RowGuid]       UNIQUEIDENTIFIER CONSTRAINT [DF_License_RowGuid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_License_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_License] PRIMARY KEY CLUSTERED ([LicenseID] ASC)
    , CONSTRAINT [FK_License_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID])
) ON [PRIMARY]
GO
";
        private static readonly string _insertDevLicenseScript = @"
USE [NGLicense]
GO

SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT [dbo].[Customer] ([CustomerID], [CustomerKey], [CompanyName], [Industry], [EmployeeCount], [MainAddress], [ContactFirstName], [ContactLastName], [WorkPhone], [WorkFax], [CellPhone])
    VALUES (1, N'{0}', N'تیم توسعه تدبیر وب', N'تولید نرم افزار', N'ده تا بیست نفر', N'(نامشخص)', N'{1}', N'{2}', N'02112345678', N'02112345678', N'1234567890')
SET IDENTITY_INSERT [dbo].[Customer] OFF

SET IDENTITY_INSERT [dbo].[License] ON
INSERT [dbo].[License] ([LicenseID], [CustomerID], [CustomerKey], [LicenseKey], [UserCount], [Edition], [StartDate], [EndDate], [ActiveModules], [IsActivated])
    VALUES (1, 1, N'{0}', N'{3}', 5, N'Enterprise', '2021-11-01', '2022-11-01', 1023, 0)
SET IDENTITY_INSERT [dbo].[License] OFF
";
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
