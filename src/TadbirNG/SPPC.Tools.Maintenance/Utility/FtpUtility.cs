using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net.Security;
using FluentFTP;
using FluentFTP.Client.BaseClient;
using SPPC.Framework.Common;

namespace SPPC.Tools.Maintenance
{
    public class FtpUtility
    {
        public FtpUtility()
        {
            _host = ConfigurationManager.AppSettings["FtpHost"];
            _user = ConfigurationManager.AppSettings["FtpUser"];
            _password = ConfigurationManager.AppSettings["FtpPassword"];
            _tempUri = Path.Combine(".", "temp");
            if (!Directory.Exists(_tempUri))
            {
                Directory.CreateDirectory(_tempUri);
            }
        }

        public event EventHandler<FtpProgressEventArgs> FtpProgress;

        public string UploadFile(string localUri, string remoteUri, bool compressed = true)
        {
            Verify.ArgumentNotNullOrEmptyString(localUri, nameof(localUri));
            Verify.ArgumentNotNullOrEmptyString(remoteUri, nameof(remoteUri));
            if (File.Exists(localUri))
            {
                using var ftp = new FtpClient(_host, _user, _password);
                ftp.ValidateCertificate += Ftp_ValidateCertificate;
                ftp.Connect();
                var sourceUri = compressed
                    ? GetCompressedFileUri(localUri)
                    : localUri;
                var remoteFile = $"{remoteUri}/{Path.GetFileName(sourceUri)}";
                ftp.UploadFile(sourceUri, remoteFile,
                    FtpRemoteExists.Overwrite, false, FtpVerify.None, RaiseFtpProgressEvent);
                ftp.Disconnect();
                return compressed ? sourceUri : null;
            }

            return null;
        }

        public string UploadFolder(string localUri, string remoteUri, string compressedFileName)
        {
            using var ftp = new FtpClient(_host, _user, _password);
            ftp.ValidateCertificate += Ftp_ValidateCertificate;
            ftp.Connect();
            if (!String.IsNullOrEmpty(compressedFileName))
            {
                var sourceUri = GetCompressedFolderUri(localUri, compressedFileName);
                var remoteFile = $"{remoteUri}/{Path.GetFileName(sourceUri)}";
                ftp.UploadFile(sourceUri, remoteFile,
                    FtpRemoteExists.Overwrite, false, FtpVerify.None, RaiseFtpProgressEvent);
                ftp.Disconnect();
                return sourceUri;
            }
            else
            {
                ftp.UploadDirectory(localUri, remoteUri,
                    FtpFolderSyncMode.Update, FtpRemoteExists.Skip, FtpVerify.None, null, RaiseFtpProgressEvent);
                ftp.Disconnect();
                return null;
            }
        }

        private string GetCompressedFileUri(string localUri)
        {
            if (File.Exists(localUri))
            {
                var zipUri = Path.Combine(_tempUri, $"{Path.GetFileNameWithoutExtension(localUri)}.zip");
                using (var stream = new FileStream(zipUri, FileMode.Create))
                {
                    using var archive = new ZipArchive(stream, ZipArchiveMode.Create, false);
                    archive.CreateEntryFromFile(localUri, Path.GetFileName(localUri));
                }

                return zipUri;
            }

            return null;
        }

        private string GetCompressedFolderUri(string localUri, string compressedName)
        {
            var zipUri = Path.Combine(_tempUri, $"{compressedName}.zip");
            ZipFile.CreateFromDirectory(localUri, zipUri, CompressionLevel.Optimal, true);
            return zipUri;
        }

        private void Ftp_ValidateCertificate(BaseFtpClient control, FtpSslValidationEventArgs e)
        {
            e.Accept = true;
            e.PolicyErrors = SslPolicyErrors.None;
        }

        private void RaiseFtpProgressEvent(FtpProgress progress)
        {
            FtpProgress?.Invoke(this, new FtpProgressEventArgs(progress));
        }

        private readonly string _host;
        private readonly string _user;
        private readonly string _password;
        private readonly string _tempUri;
    }
}
