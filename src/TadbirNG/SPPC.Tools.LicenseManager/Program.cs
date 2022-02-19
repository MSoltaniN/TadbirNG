using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;

namespace SPPC.Tools.LicenseManager
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //UpdateLicenseFile();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        static void UpdateLicenseFile()
        {
            var path = @"..\..\..\src\TadbirNG\SPPC.Licensing.Local.Web\wwwroot\tadbir.lic";
            var crypto = new CryptoService(new CertificateManager());
            var licenseData = File.ReadAllText(path, Encoding.UTF8);
            var licenseModel = JsonHelper.To<LicenseFileModel>(crypto.Decrypt(licenseData));
            licenseModel.OfflineLimit = 3;
            File.WriteAllText(path,
                crypto.Encrypt(
                    JsonHelper.From(licenseModel, false, null, false)), Encoding.UTF8);
        }
    }
}
