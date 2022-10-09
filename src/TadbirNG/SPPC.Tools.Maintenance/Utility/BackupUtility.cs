using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.Data.SqlClient;
using SPPC.Framework.Persistence;

namespace SPPC.Tools.Maintenance
{
    public class BackupUtility
    {
        public event EventHandler<TaskStartedEventArgs> TaskStarted;

        public event EventHandler<TaskFinishedEventArgs> TaskFinished;

        public event EventHandler<FtpProgressEventArgs> FtpProgress;

        public void BackupNgTadbirDatabases()
        {
            var remoteUri = ConfigurationManager.AppSettings["DbRemoteUri"];
            var ftpUtility = new FtpUtility();
            ftpUtility.FtpProgress += FtpUtility_FtpProgress;
            var sqlUtility = new SqlServerUtility(ConfigurationManager.AppSettings["InstanceName"]);
            var dbNames = GetNgTadbirDatabases();
            var backupPath = String.Empty;
            var uploadPath = String.Empty;
            foreach (var dbName in dbNames)
            {
                try
                {
                    RaiseTaskStarted(dbName, TaskType.Backup, TargetType.Database);
                    var options = DbBackupOptions.DefaultFileOptions;
                    options.SetDbName(dbName);
                    backupPath = sqlUtility.BackupDatabase(dbName, options);
                    RaiseTaskFinished(dbName, TaskType.Backup, TargetType.Database);

                    RaiseTaskStarted(dbName, TaskType.FtpUpload, TargetType.BackupFile);
                    uploadPath = ftpUtility.UploadFile(backupPath, remoteUri);
                    RaiseTaskFinished(dbName, TaskType.FtpUpload, TargetType.BackupFile);
                }
                catch (Exception exception)
                {
                    RaiseTaskFinished(dbName, TaskType.Backup, TargetType.Database, false, exception);
                }
                finally
                {
                    if (!String.IsNullOrEmpty(backupPath) && File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }

                    if (!String.IsNullOrEmpty(uploadPath) && File.Exists(uploadPath))
                    {
                        File.Delete(uploadPath);
                    }
                }
            }
        }

        public void BackupNgTadbirSites()
        {
            var uploadPath = String.Empty;
            var remoteUri = ConfigurationManager.AppSettings["SiteRemoteUri"];
            var sitePaths = GetNgTadbirSites();
            var ftpUtility = new FtpUtility();
            ftpUtility.FtpProgress += FtpUtility_FtpProgress;
            foreach (var sitePath in sitePaths)
            {
                var siteName = Path.GetFileName(sitePath);
                try
                {
                    RaiseTaskStarted(siteName, TaskType.Backup, TargetType.Site);
                    uploadPath = ftpUtility.UploadFolder(sitePath, remoteUri, BackupNameFromSitePath(sitePath));
                    RaiseTaskFinished(siteName, TaskType.Backup, TargetType.Site);
                }
                catch (Exception exception)
                {
                    RaiseTaskFinished(siteName, TaskType.Backup, TargetType.Site, false, exception);
                }
                finally
                {
                    if (!String.IsNullOrEmpty(uploadPath) && File.Exists(uploadPath))
                    {
                        File.Delete(uploadPath);
                    }
                }
            }
        }

        private static string[] GetNgTadbirDatabases()
        {
            var dbNames = new List<string> { "NGTadbirSys", "NGLicense" };
            var dbServer = ConfigurationManager.AppSettings["InstanceName"];
            var csBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = dbServer,
                InitialCatalog = "NGTadbirSys",
                UserID = "NgTadbirUser",
                Password = "Demo1234"
            };
            var dbConsole = new SqlServerConsole()
            {
                ConnectionString = csBuilder.ConnectionString
            };
            var result = dbConsole.ExecuteQuery("SELECT DbName FROM [Config].[CompanyDb] WHERE [IsActive] = 1");
            dbNames.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => row["DbName"].ToString()));
            return dbNames.ToArray();
        }

        private static string[] GetNgTadbirSites()
        {
            var root = Path.Combine(ConfigurationManager.AppSettings["WebRoot"], "TadbirNG");
            return new DirectoryInfo(root)
                .GetDirectories("*.*", SearchOption.TopDirectoryOnly)
                .Select(di => di.FullName)
                .ToArray();
        }

        private static string BackupNameFromSitePath(string sitePath)
        {
            var baseName = Path.GetFileName(sitePath).Replace(".", "-");
            return $"{baseName}-{DateTime.Now:yyyy-MM-dd}";
        }

        private void FtpUtility_FtpProgress(object sender, FtpProgressEventArgs e)
        {
            RaiseFtpProgressEvent(e);
        }

        private void RaiseTaskStarted(string name, TaskType task, TargetType target)
        {
            var raised = new TaskStartedEventArgs(name, task, target);
            TaskStarted?.Invoke(this, raised);
        }

        private void RaiseTaskFinished(
            string name, TaskType task, TargetType target, bool succeeded = true, Exception exception = null)
        {
            var raised = new TaskFinishedEventArgs(name, task, target, succeeded, exception);
            TaskFinished?.Invoke(this, raised);
        }

        private void RaiseFtpProgressEvent(FtpProgressEventArgs args)
        {
            FtpProgress?.Invoke(this, args);
        }
    }
}
