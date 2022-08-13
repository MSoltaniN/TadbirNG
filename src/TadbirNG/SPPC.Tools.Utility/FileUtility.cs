using System;
using System.IO;
using System.Linq;

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

        public static string GetAbsolutePath(string relativePath, string relativeToPath = null)
        {
            var absolutePath = relativeToPath ?? Environment.CurrentDirectory;
            var parts = relativePath.Split(Path.DirectorySeparatorChar);
            foreach (var part in parts)
            {
                if (part == "..")
                {
                    absolutePath = Path.GetDirectoryName(absolutePath);
                }
                else if (part != ".")
                {
                    absolutePath = Path.Combine(absolutePath, part);
                }
            }

            return absolutePath;
        }

        public static int GetFolderSize(string root)
        {
            var directoryInfo = new DirectoryInfo(root);
            var bytesCount = directoryInfo
                .GetFiles("*.*", new EnumerationOptions() { RecurseSubdirectories = true })
                .Select(fi => fi.Length)
                .Sum();
            return (int)Math.Round((double)bytesCount / 1024);
        }

        public static void DeleteFolder(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            foreach (var file in dirInfo.GetFiles("*.*", SearchOption.AllDirectories))
            {
                File.Delete(file.FullName);
            }

            DeleteFolderRecursive(path);
        }

        private static void DeleteFolderRecursive(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            foreach (var folder in dirInfo.GetDirectories("*.*", SearchOption.TopDirectoryOnly))
            {
                DeleteFolderRecursive(folder.FullName);
            }

            Directory.Delete(path);
        }
    }
}
