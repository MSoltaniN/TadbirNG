using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Licensing;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Licensing;

namespace SPPC.Tools.TadbirActivator.Cli
{
    class Program
    {
        private static ILicenseUtility Service
        {
            get
            {
                if (_service == null)
                {
                    string root = ConfigurationManager.AppSettings["OnlineServerRoot"];
                    _service = LicenseUtility.CreateDefault(root);
                }

                return _service;
            }
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            DisplayBanner();
            _apiLicensePath = ConfigurationManager.AppSettings["ApiLicensePath"];
            if (!EnsureAppIsInstalled())
            {
                Console.WriteLine("Press any key to quit...");
                Console.ReadKey(true);
                return;
            }

            char commandKey = GetValidCommand();
            while (commandKey != 'q')
            {
                RunCommand(commandKey);
                commandKey = GetValidCommand();
            }
        }

        private static void DisplayBanner()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("Tadbir License Activator (v1.0)");
            Console.WriteLine("(c) Copyright {0}, SPPC, All Rights Reserved", DateTime.Now.Year);
            Console.WriteLine("============================================================");
            Console.WriteLine();
        }

        private static bool EnsureAppIsInstalled()
        {
            bool isInstalled = true;
            if (!File.Exists(_apiLicensePath))
            {
                Console.WriteLine("Error : Tadbir is not installed or its data has become corrupt.");
                Console.WriteLine("Please ensure Tadbir is properly installed before running the activator.");
                isInstalled = false;
            }

            return isInstalled;
        }

        private static char GetValidCommand()
        {
            var keys = new char[] { 'i', 'a', 'q' };
            var key = Char.ToLower(GetUserCommand());
            while (!keys.Contains(key))
            {
                Console.WriteLine();
                Console.WriteLine("Error : Invalid command.");
                key = Char.ToLower(GetUserCommand());
            }

            return key;
        }

        private static char GetUserCommand()
        {
            Console.WriteLine();
            Console.WriteLine("[i/I] : Display license information");
            Console.WriteLine("[a/A] : Activate Tadbir license on this machine");
            Console.WriteLine("[q/Q] : Quit Tadbir license activator");
            Console.WriteLine();
            Console.Write("Please choose a command : ");

            var keyInfo = Console.ReadKey(false);
            return keyInfo.KeyChar;
        }

        private static void RunCommand(char commandKey)
        {
            if (commandKey == 'i')
            {
                ShowLicenseInfo();
            }
            else if (commandKey == 'a')
            {
                ActivateLicense();
            }
            else
            {
                return;
            }
        }

        private static void ShowLicenseInfo()
        {
            string json = File.ReadAllText(_apiLicensePath);
            var license = JsonHelper.To<LicenseViewModel>(json);

            Console.WriteLine();
            Console.WriteLine("Company Name : {0}", license.CustomerName);
            Console.WriteLine("License Owner : {0}", license.ContactName);
            Console.WriteLine("Edition : {0}", license.Edition);
            Console.WriteLine("User Count : {0}", license.UserCount);
            Console.WriteLine("Valid From : {0}", JalaliDateTime.FromDateTime(license.StartDate).ToShortDateString());
            Console.WriteLine("Valid Until : {0}", JalaliDateTime.FromDateTime(license.EndDate).ToShortDateString());
            Console.WriteLine("Active Modules : {0}", GetActiveModules(license.ActiveModules));
            Console.WriteLine();
        }

        private static string GetActiveModules(int activeModules)
        {
            var modules = new List<string>();
            var modulesFlags = (Subsystems)activeModules;
            if (HasModule(modulesFlags, Subsystems.Accounting))
            {
                modules.Add("Accounting");
            }

            if (HasModule(modulesFlags, Subsystems.Budgeting))
            {
                modules.Add("Budgeting");
            }

            if (HasModule(modulesFlags, Subsystems.CashFlow))
            {
                modules.Add("Cash Flow");
            }

            if (HasModule(modulesFlags, Subsystems.Cheque))
            {
                modules.Add("Cheque");
            }

            if (HasModule(modulesFlags, Subsystems.Inventory))
            {
                modules.Add("Inventory");
            }

            if (HasModule(modulesFlags, Subsystems.Personnel))
            {
                modules.Add("Personnel");
            }

            if (HasModule(modulesFlags, Subsystems.Purchase))
            {
                modules.Add("Purchase");
            }

            if (HasModule(modulesFlags, Subsystems.Sales))
            {
                modules.Add("Sales");
            }

            if (HasModule(modulesFlags, Subsystems.WagePayment))
            {
                modules.Add("Wage Payment");
            }

            if (HasModule(modulesFlags, Subsystems.Warehousing))
            {
                modules.Add("Warehousing");
            }

            return String.Join(", ", modules.ToArray());
        }

        private static bool HasModule(Subsystems modules, Subsystems module)
        {
            return (modules & module) != 0;
        }

        private static void ActivateLicense()
        {
            Console.WriteLine();
            Console.WriteLine("In Progress...");

            try
            {
                var activation = GetActivationData();
                string license = Service.GetActivatedLicense(activation);
                if (String.IsNullOrEmpty(license))
                {
                    Console.WriteLine("This Tadbir installation is already activated.");
                    return;
                }

                string licenseRoot = ConfigurationManager.AppSettings["LicensePath"];
                string licensePath = Path.Combine(licenseRoot, Constants.LicenseFile);
                File.WriteAllText(licensePath, license);
                ExportCertificate(licenseRoot, license);
                Console.WriteLine("Tadbir was successfully activated.");
            }
            catch (Exception ex)
            {
                var message = String.Format("Error : {1}{0}{0}StackTrace : {2}",
                    Environment.NewLine, ex.Message, ex.StackTrace);
                Console.WriteLine(message);
                Console.WriteLine("Unable to establish a connection to the Internet.");
            }
        }

        private static ActivationModel GetActivationData()
        {
            var deviceId = new DeviceIdProvider();
            var activation = new ActivationModel()
            {
                InstanceKey = GetInstanceId(),
                HardwareKey = deviceId.GetRemoteDeviceId(GetRemoteConnection()),
            };

            var manager = new CertificateGenerator();
            _certificate = manager.GenerateSelfSigned();
            activation.ClientKey = Convert.ToBase64String(_certificate.GetPublicKey());
            return activation;
        }

        private static string GetInstanceId()
        {
            string instance = null;
            var type = typeof(Program);
            using (var reader = new StreamReader(
                type.Assembly.GetManifestResourceStream("SPPC.Tools.TadbirActivator.Cli.instance.id")))
            {
                instance = reader.ReadToEnd();
            }

            return instance;
        }

        private static void ExportCertificate(string root, string licenseData)
        {
            string path = Path.Combine(root, Constants.CertificateFile);
            var license = Service.LoadLicense(licenseData);
            var certificateBytes = _certificate.Export(X509ContentType.Pkcs12, license.Secret);
            File.WriteAllBytes(path, certificateBytes);
        }

        private static RemoteConnection GetRemoteConnection()
        {
            return new RemoteConnection()
            {
                Domain = ConfigurationManager.AppSettings["SSH-Domain"],
                Port = Int32.Parse(ConfigurationManager.AppSettings["SSH-Port"]),
                User = ConfigurationManager.AppSettings["SSH-User"],
                Password = ConfigurationManager.AppSettings["SSH-Password"]
            };
        }

        private static string _apiLicensePath;
        private static ILicenseUtility _service;
        private static X509Certificate2 _certificate;
    }
}
