﻿using System;
using System.IO;
using System.Linq;
using Ionic.Zip;
using Ionic.Zlib;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Utility
{
    public class ArchiveUtility
    {
        public ArchiveUtility(string toolsPath = null, bool redirectOutput = true)
        {
            // NOTE: The following line ONLY works in Windows environment, because Windows versions of
            // gzip and tar are added to misc/tools. Apparently, Linux environment already has gzip in Path.
            SetToolsPath(toolsPath);
            RedirectOutput(redirectOutput);
        }

        public static void Zip(string zipFile, string sourceFolder, string password = null)
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

        public void Cab(string sourceFolder)
        {
            Verify.ArgumentNotNullOrEmptyString(sourceFolder, nameof(sourceFolder));
            if (Directory.Exists(sourceFolder))
            {
                var directiveFile = "files.txt";
                var cabFile = $"{Path.GetFileName(sourceFolder)}";
                var fileNames = String.Join(Environment.NewLine,
                    new DirectoryInfo(sourceFolder)
                        .GetFiles("*.*", SearchOption.AllDirectories)
                        .Select(file => $"\"{file.FullName}\""));
                File.WriteAllText(directiveFile, fileNames);
                _runner.Run($"makecab /F {directiveFile} /D CabinetNameTemplate={cabFile}*.cab /D CompressionType=LZX /D MaxDiskSize={FileSize.FromMegaBytes(30)} /D DiskDirectoryTemplate=");
                File.Delete(directiveFile);
            }
        }

        public void GZip(string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));
            _runner.Run(String.Format(ToolConstants.GzipTemplate, sourceFile));
        }

        public void GunZip(string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));
            _runner.Run(String.Format(ToolConstants.GunzipTemplate, sourceFile));
        }

        public void Tar(string tarFile, string sourceFile)
        {
            Verify.ArgumentNotNullOrEmptyString(tarFile, nameof(tarFile));
            Verify.ArgumentNotNullOrEmptyString(sourceFile, nameof(sourceFile));
            _runner.Run(String.Format(ToolConstants.TarTemplate, tarFile, sourceFile));
        }

        public void UnTar(string tarFile)
        {
            Verify.ArgumentNotNullOrEmptyString(tarFile, nameof(tarFile));
            _runner.Run(String.Format(ToolConstants.UntarTemplate, tarFile));
        }

        private static void SetToolsPath(string toolsPath)
        {
            if (!String.IsNullOrEmpty(toolsPath))
            {
                var currentPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
                var path = FileUtility.GetAbsolutePath(toolsPath, currentPath);
                EnvironmentHelper.AddProcessVariable("Path", path);
            }
        }

        private static void Runner_OutputReceived(object sender, OutputReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Output))
            {
                Console.WriteLine(e.Output.Replace("\n", Environment.NewLine));
            }
        }

        private void RedirectOutput(bool redirect = true)
        {
            if (redirect)
            {
                _runner.OutputReceived += Runner_OutputReceived;
            }
        }

        private readonly CliRunner _runner = new();
    }
}
