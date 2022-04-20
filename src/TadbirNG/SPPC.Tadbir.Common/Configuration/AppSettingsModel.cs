using System;

namespace SPPC.Tadbir.Configuration
{
    public class AppSettingsModel
    {
        public AppSettingsModel()
        {
            Logging = new LoggingModel()
            {
                LogLevel = new LogLevelModel()
                {
                    Default = "Information",
                    Microsoft = "Warning"
                }
            };
            AllowedHosts = "*";
        }

        public ConnectionStringsModel ConnectionStrings { get; set; }

        public LoggingModel Logging { get; set; }

        public string AllowedHosts { get; set; }
    }
}
