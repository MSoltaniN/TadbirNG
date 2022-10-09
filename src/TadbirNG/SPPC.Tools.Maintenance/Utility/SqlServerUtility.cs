using System.IO;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;
using SPPC.Framework.Common;

namespace SPPC.Tools.Maintenance
{
    public class SqlServerUtility
    {
        public SqlServerUtility(string instanceName)
        {
            Verify.ArgumentNotNullOrEmptyString(instanceName, nameof(instanceName));
            _instanceName = instanceName;
        }

        public string[] GetDbNames()
        {
            var server = new Server(_instanceName);
            return server.Databases
                .Cast<Database>()
                .Select(db => db.Name)
                .ToArray();
        }

        public string BackupDatabase(string dbName, DbBackupOptions options)
        {
            Verify.ArgumentNotNullOrEmptyString(dbName, nameof(dbName));
            Verify.ArgumentNotNull(options, nameof(options));
            var server = new Server(_instanceName);
            var backup = new Backup()
            {
                Action = options.Action,
                BackupSetDescription = options.Description,
                BackupSetName = options.Name,
                Database = dbName,
                ExpirationDate = options.ExpirationDate,
                Incremental = options.Incremental,
                LogTruncation = options.LogTruncation
            };
            var backupPath = Path.Combine(server.BackupDirectory, options.FileName);
            var device = new BackupDeviceItem(backupPath, DeviceType.File);
            backup.Devices.Add(device);
            backup.SqlBackup(server);
            return backupPath;
        }

        private readonly string _instanceName;
    }
}
