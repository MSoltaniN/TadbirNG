using System;
using SPPC.Framework.Service;
using SPPC.Licensing.Api;
using SPPC.Licensing.Model;

namespace SPPC.Tools.DeliveryCli
{
    public class InputUtility
    {
        public static void DisplayBanner()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("TadbirNG Customer Delivery tool (v1.2)");
            Console.WriteLine($"(c) Copyright {DateTime.Now.Year}, SPPC, All Rights Reserved");
            Console.WriteLine("============================================================");
            Console.WriteLine();
        }

        public static bool ConfirmLicenseId()
        {
            Console.WriteLine("This utility requires a valid license identifier.");
            Console.Write("Do you wish to continue? (y[Y]/n[N]) ");
            var key = Console.ReadKey();
            while (Char.ToLower(key.KeyChar) != 'y' && Char.ToLower(key.KeyChar) != 'n')
            {
                Console.WriteLine();
                Console.WriteLine("Invalid key!");
                Console.Write("(y[Y]/n[N]) ");
                key = Console.ReadKey();
            }

            Console.WriteLine();
            return Char.ToLower(key.KeyChar) == 'y';
        }

        public static LicenseModel QueryLicense()
        {
            var license = default(LicenseModel);
            Console.WriteLine("Please enter license id for the package you wish to deliver.");
            int licenseId = QueryLicenseId();
            var service = new ServiceClient(_rootUrl);
            while (license == null)
            {
                if (licenseId == 0)
                {
                    break;
                }

                license = service.Get<LicenseModel>(LicenseApi.License, licenseId);
                if (license == null)
                {
                    Console.WriteLine("ERROR: No license by given id could be found.");
                    licenseId = QueryLicenseId();
                }
            }

            return license;
        }

        public static int QueryLicenseId()
        {
            int licenseId;
            Console.Write("License id (enter 0 to cancel) : ");
            string userValue = Console.ReadLine();
            while (!Int32.TryParse(userValue, out licenseId))
            {
                Console.WriteLine();
                Console.WriteLine("ERROR: License id is invalid. Please enter an integer value.");
                Console.Write("License id (enter 0 to cancel) : ");
                userValue = Console.ReadLine();
            }

            return licenseId;
        }

        public static string QueryZipFilePassword()
        {
            Console.Write("Please enter a password for delivery zip file : ");
            string password;
            do
            {
                password = Console.ReadLine();
                if (String.IsNullOrEmpty(password))
                {
                    Console.WriteLine("ERROR: Password cannot be empty. Please enter a valid password.");
                }
                else if (password.Length < _minPasswordLength)
                {
                    Console.WriteLine($"ERROR: Please enter a password with at least {_minPasswordLength} characters.");
                }
            } while (String.IsNullOrEmpty(password) || password.Length < _minPasswordLength);

            return password;
        }

        private const string _rootUrl = "http://130.185.76.7:9094";
        private const int _minPasswordLength = 4;
    }
}
