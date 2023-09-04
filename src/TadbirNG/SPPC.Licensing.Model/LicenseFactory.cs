using System;

namespace SPPC.Licensing.Model
{
    public static class LicenseFactory
    {
        public static LicenseViewModel FromModel(LicenseModel license)
        {
            if (license == null)
            {
                return null;
            }

            return new LicenseViewModel()
            {
                ActiveModules = license.ActiveModules,
                ContactName = $"{license.Customer?.ContactFirstName} {license.Customer?.ContactLastName}",
                CustomerName = license.Customer?.CompanyName,
                Edition = license.Edition,
                StartDate = license.StartDate,
                EndDate = license.EndDate,
                UserCount = license.UserCount
            };
        }

        public static LicenseViewModel FromFileModel(LicenseFileModel license)
        {
            if (license == null)
            {
                return null;
            }

            return new LicenseViewModel()
            {
                ActiveModules = license.ActiveModules,
                ContactName = license.ContactName,
                CustomerName = license.CustomerName,
                Edition = license.Edition,
                StartDate = license.StartDate,
                EndDate = license.EndDate,
                UserCount = license.UserCount
            };
        }

        public static LicenseViewModel FromContact(string firstName, string lastName)
        {
            return new LicenseViewModel()
            {
                CustomerName = "تیم توسعه تدبیر وب",
                ContactName = String.Format("{0} {1}", firstName, lastName),
                Edition = "Enterprise",
                UserCount = 5,
                ActiveModules = 1023,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date + TimeSpan.FromDays(365)
            };
        }
    }
}
