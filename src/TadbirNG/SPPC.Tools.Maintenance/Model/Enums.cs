using System;

namespace SPPC.Tools.Maintenance
{
    public enum TaskType
    {
        None = 0,
        Backup = 1,
        Restore = 2,
        FtpUpload = 3,
        FtpDownload = 4
    }

    public enum TargetType
    {
        None = 0,
        Database = 1,
        Site = 2,
        BackupFile = 3
    }
}
