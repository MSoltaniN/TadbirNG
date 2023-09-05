using System.Data.SqlClient;
using System.IO;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Configuration;

namespace SPPC.Tools.Model
{
    public static class DbConnections
    {
        public static string SystemConnection
        {
            get { return GetSystemConnection(); }
        }

        public static string CompanyConnection
        {
            get { return GetDbConnection("NGTadbir"); }
        }

        public static string LicenseConnection
        {
            get { return GetDbConnection("NGLicense"); }
        }

        private static string GetSystemConnection()
        {
            string path = Path.Combine(PathConfig.WebApiRoot, "appSettings.Development.json");
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }

        private static string GetDbConnection(string dbName)
        {
            var builder = new SqlConnectionStringBuilder(SystemConnection)
            {
                InitialCatalog = dbName
            };
            return builder.ConnectionString;
        }
    }
}
