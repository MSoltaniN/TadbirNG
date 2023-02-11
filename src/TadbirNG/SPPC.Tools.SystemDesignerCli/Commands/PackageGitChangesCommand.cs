using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zip;
using Ionic.Zlib;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesignerCli
{
    public class PackageGitChangesCommand : ICliCommand
    {
        public void Execute()
        {
            var tempPath = Path.GetTempFileName();
            File.Delete(tempPath);
            Directory.CreateDirectory(tempPath);
            Console.WriteLine($"Copying all files modified today to {tempPath}...");
            var newSources = new List<string>();
            var rootFolder = PathConfig.SolutionRoot;
            var filters = new string[] { "*.cs", "*.resx", "*.tt", "*.json" };
            Array.ForEach(filters, filter => newSources.AddRange(GetModifiedFiles(rootFolder, filter)));

            rootFolder = PathConfig.ResourceRoot;
            filters = new string[] { "*.sql", "*.xml" };
            Array.ForEach(filters, filter => newSources.AddRange(GetModifiedFiles(rootFolder, filter)));

            foreach (var grp in newSources.GroupBy(source => Path.GetDirectoryName(source)))
            {
                EnsurePathExists(grp.Key, tempPath);
            }

            Array.ForEach(newSources.ToArray(), source =>
            {
                var sourcePath = Path.Combine(PathConfig.GitRepoRoot, source);
                var targetPath = Path.Combine(tempPath, source);
                File.Copy(sourcePath, targetPath);
            });

            Zip(ZipFileName, tempPath);
            Console.WriteLine(
                $"Modified files successfully packed to {Path.Combine(Environment.CurrentDirectory, ZipFileName)}.");
        }

        private static IList<string> GetModifiedFiles(string sourceRoot, string filter)
        {
            var today = DateTime.Now.Date;
            var absoluteRoot = $"{FileUtility.GetAbsolutePath(PathConfig.GitRepoRoot)}\\";
            return new DirectoryInfo(sourceRoot)
                .GetFiles(filter, SearchOption.AllDirectories)
                .Where(file => !file.FullName.Contains("\\bin\\")
                    && !file.FullName.Contains("\\obj\\")
                    && !file.FullName.Contains("\\_codegen_\\")
                    && File.GetLastWriteTime(file.FullName) >= today)
                .Select(file => file.FullName.Replace(absoluteRoot, String.Empty))
                .ToList();
        }

        private static void EnsurePathExists(string path, string targetRoot)
        {
            string targetPath = targetRoot;
            var items = path.Split('\\');
            foreach (var item in items)
            {
                targetPath = Path.Combine(targetPath, item);
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
            }
        }

        private static void Zip(string zipFile, string sourceFolder, string password = null)
        {
            var zip = new ZipFile()
            {
                CompressionLevel = CompressionLevel.BestCompression,
                CompressionMethod = CompressionMethod.BZip2,
                Encryption = EncryptionAlgorithm.WinZipAes128,
                FlattenFoldersOnExtract = true,
                Password = password
            };
            using (zip)
            {
                zip.AddDirectory(sourceFolder);
                zip.Save(zipFile);
            }
        }

        private const string ZipFileName = "TadbirNG-Changes.zip";
    }
}
