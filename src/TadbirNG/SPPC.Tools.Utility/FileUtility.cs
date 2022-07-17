using System;
using System.IO;

namespace SPPC.Tools.Utility
{
    public class FileUtility
    {
        public static string GetTempFolderPath()
        {
            var tempFile = Path.GetTempFileName();
            var tempFolder = String.Format($"bla-{Path.GetFileName(tempFile)}");
            File.Delete(tempFile);
            var tempPath = Path.Combine(Path.GetDirectoryName(tempFile), tempFolder);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            return tempPath;
        }

        public static void BackupFile(string filePath, string tempPath)
        {
            if (File.Exists(filePath))
            {
                File.Copy(filePath, Path.Combine(tempPath, Path.GetFileName(filePath)));
            }
        }

        public static void RestoreFile(string filePath, string tempPath)
        {
            string backupPath = Path.Combine(tempPath, Path.GetFileName(filePath));
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, filePath, true);
                File.Delete(backupPath);
            }
        }

        public static void BackupFiles(string[] files, string tempPath)
        {
            Array.ForEach(files, file => BackupFile(file, tempPath));
        }

        public static void RestoreFiles(string[] files, string tempPath)
        {
            Array.ForEach(files, file => RestoreFile(file, tempPath));
        }
    }
}
