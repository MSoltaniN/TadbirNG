using System;
using Microsoft.SqlServer.Management.Smo;
using SPPC.Framework.Common;

namespace SPPC.Tools.Maintenance
{
    public class DbBackupOptions
    {
        public DbBackupOptions()
        {
            FileName = String.Empty;
        }

        public static DbBackupOptions DefaultFileOptions
        {
            get { return GetDefaultFileOptions(); }
        }

        public BackupActionType Action { get; set; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool Incremental { get; set; }

        public BackupTruncateLogType LogTruncation { get; set; }

        public void SetDbName(string dbName)
        {
            Verify.ArgumentNotNull(dbName, nameof(dbName));
            Name = $"{dbName} Backup";
            FileName = FileName.EndsWith("bak")
                ? $"{dbName}{FileName}"
                : $"{dbName}.bak";
            Description = Incremental
                ? $"Incremental backup of {dbName}"
                : $"Full backup of {dbName}";
        }

        private static DbBackupOptions GetDefaultFileOptions()
        {
            return new DbBackupOptions()
            {
                Action = BackupActionType.Database,
                FileName = $"{DateTime.Now:yyyy-MM-dd}.bak",
                ExpirationDate = DateTime.MaxValue,
                Incremental = false,
                LogTruncation = BackupTruncateLogType.Truncate
            };
        }
    }
}
