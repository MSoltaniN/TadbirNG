using System;
using System.Data;
using BabakSoft.Platform.Data;
using SPPC.Framework.Cryptography;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public class LicenseRepository
    {
        #region Customer Operations

        public int InsertCustomer(CustomerModel customer)
        {
            var dal = new SqlDataLayer(_connection);
            string commandTemplate = @"
INSERT INTO [dbo].[Customer] ([CustomerKey], [CompanyName], [Industry], [EmployeeCount], [MainAddress],
    [ContactFirstName], [ContactLastName], [WorkPhone], [WorkFax], [CellPhone])
VALUES('{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}', N'{7}', N'{8}', N'{9}')";
            string command = String.Format(commandTemplate, customer.CustomerKey, customer.CompanyName,
                customer.Industry, customer.EmployeeCount, customer.HeadquartersAddress,
                customer.ContactFirstName, customer.ContactLastName, customer.WorkPhone,
                customer.WorkFax, customer.CellPhone);
            return dal.ExecuteNonQuery(command);
        }

        #endregion

        #region License Operations

        public int GetLicenseId(string customerKey, string licenseKey)
        {
            int licenseId = 0;
            var dal = new SqlDataLayer(_connection);
            string command = String.Format(@"
SELECT CustomerID
FROM [dbo].[Customer]
WHERE CustomerKey = '{0}'", customerKey);
            object item = dal.QueryScalar(command);
            if (item != null)
            {
                int customerId = Convert.ToInt32(item);
                command = String.Format(@"
SELECT LicenseID
FROM [dbo].[License]
WHERE CustomerID = {0} AND LicenseKey = '{1}'", customerId, licenseKey);

                item = dal.QueryScalar(command);
                licenseId = (item != null)
                    ? Convert.ToInt32(item)
                    : 0;
            }

            return licenseId;
        }

        public LicenseModel GetLicense(string licenseKey, string customerKey)
        {
            var dal = new SqlDataLayer(_connection);
            string command = String.Format(
                @"SELECT * FROM [dbo].[License] WHERE LicenseKey = '{0}'", licenseKey);
            var licenseData = dal.Query(command);
            var license = GetLicense(licenseData, customerKey);
            license.Secret = licenseData.Rows[0]["Secret"].ToString();
            return license;
        }

        public LicenseModel GetActivatedLicense(ActivationModel activation)
        {
            var license = default(LicenseModel);
            var dal = new SqlDataLayer(_connection);
            string secret = GetNewLicenseSecret();
            string command = String.Format(@"
UPDATE [dbo].[License]
SET HardwareKey = '{0}', ClientKey = '{1}', Secret = '{2}', IsActivated = 1
WHERE LicenseKey = '{3}'",
                activation.HardwareKey, activation.ClientKey, secret, activation.InstanceKey.LicenseKey);
            int rowsAffected = dal.ExecuteNonQuery(command);
            if (rowsAffected == 1)
            {
                command = String.Format(
                    "SELECT * FROM [dbo].[License] WHERE LicenseKey = '{0}'",
                    activation.InstanceKey.LicenseKey);
                var rawLicense = dal.Query(command);
                license = GetLicense(rawLicense, activation.InstanceKey.CustomerKey);
            }

            license.Secret = secret;
            return license;
        }

        public int InsertLicense(LicenseModel license)
        {
            int rowsAffected = 0;
            var dal = new SqlDataLayer(_connection);
            string command = String.Format(@"
SELECT CustomerID
FROM [dbo].[Customer]
WHERE CustomerKey = '{0}'", license.InstanceKey.CustomerKey);
            var item = dal.QueryScalar(command);
            if (item != null)
            {
                int customerId = Convert.ToInt32(item);
                string commandTemplate = @"
INSERT INTO [dbo].[License] ([CustomerID], [LicenseKey], [UserCount], [Edition],
    [StartDate], [EndDate], [ActiveModules])
VALUES({0}, '{1}', {2}, '{3}', '{4}', '{5}', {6})";
                command = String.Format(commandTemplate, customerId, license.InstanceKey.LicenseKey,
                    license.UserCount, license.Edition, license.ContractStart, license.ContractEnd,
                    license.ActiveModules);
                rowsAffected = dal.ExecuteNonQuery(command);
            }

            return rowsAffected;
        }

        public string GetEncryptedLicense(LicenseModel license)
        {
            var serializer = new JsonSerializer();
            var base64 = serializer.Serialize(license);
            var crypto = new CryptoService();
            return crypto.Encrypt(base64);
        }

        public bool? GetActivationStatus(string licenseKey)
        {
            bool? isActivated = null;
            var dal = new SqlDataLayer(_connection);
            string command = String.Format(@"
SELECT IsActivated
FROM [dbo].[License]
WHERE LicenseKey = '{0}'", licenseKey);
            object item = dal.QueryScalar(command);
            if (item != null)
            {
                isActivated = Convert.ToBoolean(item);
            }

            return isActivated;
        }

        #endregion

        private LicenseModel GetLicense(DataTable table, string customerKey)
        {
            var license = default(LicenseModel);
            if (table.Rows.Count > 0)
            {
                var row = table.Rows[0];
                license = new LicenseModel()
                {
                    InstanceKey = new InstanceModel()
                    {
                        CustomerKey = customerKey,
                        LicenseKey = row["LicenseKey"].ToString()
                    },
                    HardwareKey = row["HardwareKey"].ToString(),
                    ClientKey = row["ClientKey"].ToString(),
                    UserCount = Convert.ToInt32(row["UserCount"]),
                    Edition = row["Edition"].ToString(),
                    ContractStart = DateTime.Parse(row["StartDate"].ToString()),
                    ContractEnd = DateTime.Parse(row["EndDate"].ToString()),
                    ActiveModules = Convert.ToInt32(row["ActiveModules"]),
                };
            }

            return license;
        }

        private string GetNewLicenseSecret()
        {
            var secret = RandomGenerator.Generate(16);
            return Convert.ToBase64String(secret);
        }

        private readonly string _connection = @"Server=(localdb)\MSSQLLocalDB;Database=NGLicense;Trusted_Connection=True";
    }
}
