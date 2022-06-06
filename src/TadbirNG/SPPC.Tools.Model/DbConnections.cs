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
            get { return GetCompanyConnection(); }
        }

        private static string GetSystemConnection()
        {
            string path = Path.Combine(PathConfig.WebApiRoot, "appsettings.Development.json");
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }

        private static string GetCompanyConnection()
        {
            var builder = new SqlConnectionStringBuilder(SystemConnection)
            {
                InitialCatalog = "NGTadbir"
            };
            return builder.ConnectionString;
        }
    }
}
