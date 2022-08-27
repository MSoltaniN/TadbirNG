using System;
using System.IO;
using System.IO.Compression;
using Fwk = SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Utility
{
    public class Checksum
    {
        public static string Calculate(string folderPath)
        {
            Fwk::Verify.ArgumentNotNullOrEmptyString(folderPath, nameof(folderPath));

            string checksum = String.Empty;
            if (Directory.Exists(folderPath))
            {
                string tempPath = FileUtility.GetTempFolderPath();
                var zipPath = Path.Combine(tempPath, ZipFileName);
                ZipFile.CreateFromDirectory(folderPath, zipPath, CompressionLevel.Fastest, false);
                checksum = CryptoService.Default
                    .CreateHash(File.ReadAllBytes(zipPath))
                    .ToLower();
                File.Delete(zipPath);
                Directory.Delete(tempPath);
            }

            return checksum;
        }

        public static bool Verify(string folderPath, string checksum)
        {
            var expectedChecksum = Calculate(folderPath);
            return String.Compare(expectedChecksum, checksum, true) == 0;
        }

        private const string ZipFileName = "items.zip";
    }
}
