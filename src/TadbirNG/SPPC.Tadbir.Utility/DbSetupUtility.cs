using System;
using System.IO;
using System.Reflection;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Configuration;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// Provides operations for dynamic database setup
    /// </summary>
    /// <remarks>
    /// <para>This class supports the following scenarios for database setup :</para>
    /// <para>1. Setup : This is when application databases are being setup for the first time.</para>
    /// <para>2. Modify : This is when application databases are being modified after setup,
    /// using the same setup app version and update scripts.</para>
    /// <para>3. Update : This is when application databases are being modified during update process.</para>
    /// </remarks>
    public class DbSetupUtility
    {
        static DbSetupUtility()
        {
            LoadScripts();
        }

        public DbSetupUtility(ISqlConsole dbConsole, IBuildSettings settings)
        {
            Verify.ArgumentNotNull(dbConsole, nameof(dbConsole));
            Verify.ArgumentNotNull(settings, nameof(settings));
            _dbConsole = dbConsole;
            _settings = settings;
            CurrentLoginName = SysParameterUtility.AllParameters.Db.LoginName;
        }

        public void ConfigureDatabase()
        {
            var dbParams = SysParameterUtility.AllParameters.Db;

            // Create default login if not already created + Add required database roles...
            SetupDefaultLogin();

            // Create system database if not already created + Add required settings...
            if (!ExistsSystemDatabase())
            {
                CreateSystemDatabase();
                CreateOtherDbObjects();
            }
            else
            {
                UpdateDatabaseOwner(dbParams.SysDbName, _settings.DbUserName);
                UpdateJobOwner(CurrentLoginName, _settings.DbUserName);
            }

            if (!ExistsFirstCompany())
            {
                CreateFirstCompany();
                AddRequiredCompanyData();
            }
            else
            {
                UpdateDatabaseOwner(dbParams.FirstDbName, _settings.DbUserName);
            }
        }

        public void UpgradeDatabase()
        {
        }

        private static void LoadScripts()
        {
            var resNamespace = "SPPC.Tadbir.Utility.Scripts.";
            var extension = ".sql";
            SetupLoginScript = LoadScript($"{resNamespace}SetupDefaultLogin{extension}");
            CreateSysDbScript = LoadScript($"{resNamespace}TadbirSys_CreateDbObjects{extension}");
            UpdateTemplatesScript = LoadScript($"{resNamespace}TadbirSys_QRTemplates{extension}");
            CreateTriggersScript = LoadScript($"{resNamespace}TadbirSys_CreateTriggers{extension}");
            CreateJobsScript = LoadScript($"{resNamespace}TadbirSys_CreateJobs{extension}");
            FirstCompanyScript = LoadScript($"{resNamespace}Tadbir_FirstCompany{extension}");
            CreateFirstDbScript = LoadScript($"{resNamespace}Tadbir_CreateDbObjects{extension}");
            TestEnvironmentScript = LoadScript($"{resNamespace}Tadbir_TestEnvironment{extension}");
            StateAndCitiesScript = LoadScript($"{resNamespace}Tadbir_StatesAndCities{extension}");
        }

        private static string LoadScript(string resourceName)
        {
            var script = String.Empty;
            var assembly = Assembly.GetAssembly(typeof(DbSetupUtility));
            if (assembly != null)
            {
                using var reader = new StreamReader(
                    assembly.GetManifestResourceStream(resourceName));
                script = reader.ReadToEnd();
            }

            return script;
        }

        private void SetupDefaultLogin()
        {
            if (!String.IsNullOrEmpty(SetupLoginScript))
            {
                var script = SetupLoginScript
                    .Replace("@LoginName", _settings.DbUserName)
                    .Replace("@Password", _settings.DbPassword);
                _dbConsole.ExecuteNonQuery(script);
            }
        }

        private bool ExistsSystemDatabase()
        {
            return ExistsDatabase(SysParameterUtility.AllParameters.Db.SysDbName);
        }

        private bool ExistsFirstCompany()
        {
            return ExistsDatabase(SysParameterUtility.AllParameters.Db.FirstDbName);
        }

        private bool ExistsDatabase(string dbName)
        {
            var script = @"
SELECT [database_id]
FROM [sys].[databases]
WHERE [name] = '@DbName'";
            script = script.Replace("@DbName", dbName);
            var result = _dbConsole.ExecuteQuery(script);
            return result.Rows.Count > 0;
        }

        private void CreateSystemDatabase()
        {
            var dbParams = SysParameterUtility.AllParameters.Db;
            if (!String.IsNullOrEmpty(CreateSysDbScript))
            {
                var script = CreateSysDbScript
                    .Replace("@SysDbName", dbParams.SysDbName)
                    .Replace("@LoginName", _settings.DbUserName)
                    .Replace("@AdminUserName", dbParams.AdminUserName)
                    .Replace("@AdminPasswordHash", dbParams.AdminPasswordHash)
                    .Replace("@AdminFirstName", dbParams.AdminFirstName)
                    .Replace("@AdminLastName", dbParams.AdminLastName);
                _dbConsole.ExecuteNonQuery(script);
            }

            if (!String.IsNullOrEmpty(UpdateTemplatesScript))
            {
                var script = UpdateTemplatesScript
                    .Replace("@SysDbName", dbParams.SysDbName);
                _dbConsole.ExecuteNonQuery(script);
            }
        }

        private void CreateOtherDbObjects()
        {
            var dbParams = SysParameterUtility.AllParameters.Db;
            if (!String.IsNullOrEmpty(CreateTriggersScript))
            {
                var script = CreateTriggersScript
                    .Replace("@SysDbName", dbParams.SysDbName);
                _dbConsole.ExecuteNonQuery(script);
            }

            if (!String.IsNullOrEmpty(CreateJobsScript))
            {
                var script = CreateJobsScript
                    .Replace("@LoginName", _settings.DbUserName)
                    .Replace("@SysDbName", dbParams.SysDbName);
                _dbConsole.ExecuteNonQuery(script);
            }
        }

        private void CreateFirstCompany()
        {
            var dbParams = SysParameterUtility.AllParameters.Db;
            if (!String.IsNullOrEmpty(FirstCompanyScript))
            {
                var script = FirstCompanyScript
                    .Replace("@FirstDbName", dbParams.FirstDbName)
                    .Replace("@LoginName", _settings.DbUserName)
                    .Replace("@SysDbName", dbParams.SysDbName)
                    .Replace("@FirstCompanyName", dbParams.FirstCompanyName)
                    .Replace("@DbServerName", _settings.DbServerName);
                _dbConsole.ExecuteNonQuery(script);
            }

            if (!String.IsNullOrEmpty(CreateFirstDbScript))
            {
                var script = CreateFirstDbScript
                    .Replace("@FirstDbName", dbParams.FirstDbName);
                _dbConsole.ExecuteNonQuery(script);
            }
        }

        private void AddRequiredCompanyData()
        {
            var dbParams = SysParameterUtility.AllParameters.Db;
            if (!String.IsNullOrEmpty(TestEnvironmentScript))
            {
                var script = TestEnvironmentScript
                    .Replace("@FirstDbName", dbParams.FirstDbName);
                _dbConsole.ExecuteNonQuery(script);
            }

            if (!String.IsNullOrEmpty(StateAndCitiesScript))
            {
                var script = StateAndCitiesScript
                    .Replace("@FirstDbName", dbParams.FirstDbName);
                _dbConsole.ExecuteNonQuery(script);
            }
        }

        private void UpdateDatabaseOwner(string dbName, string ownerName)
        {
            var script = @"
ALTER AUTHORIZATION ON DATABASE::@DbName TO @OwnerName;
GO";
            script = script
                .Replace("@DbName", dbName)
                .Replace("@OwnerName", ownerName);
            _dbConsole.ExecuteQuery(script);
        }

        private void UpdateJobOwner(string currentOwner, string newOwner)
        {
            var script = @"
USE msdb;
GO

EXEC dbo.sp_manage_jobs_by_login
    @action = N'REASSIGN',
    @current_owner_login_name = N'@CurrentOwner',
    @new_owner_login_name = N'@NewOwner';
GO";
            script = script
                .Replace("@CurrentOwner", currentOwner)
                .Replace("@NewOwner", newOwner);
            _dbConsole.ExecuteQuery(script);
        }

        private readonly ISqlConsole _dbConsole;
        private readonly IBuildSettings _settings;
        private static string CurrentLoginName;
        private static string SetupLoginScript;
        private static string CreateSysDbScript;
        private static string UpdateTemplatesScript;
        private static string CreateTriggersScript;
        private static string CreateJobsScript;
        private static string FirstCompanyScript;
        private static string CreateFirstDbScript;
        private static string TestEnvironmentScript;
        private static string StateAndCitiesScript;
    }
}
