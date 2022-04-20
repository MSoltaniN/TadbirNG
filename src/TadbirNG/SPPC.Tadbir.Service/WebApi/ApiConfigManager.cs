using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace SPPC.Tadbir.Service
{
    public class ApiConfigManager
    {
        public TConfig LoadConfig<TConfig>(bool isDevelopment = false)
            where TConfig : class
        {
            string appSettingsFile = isDevelopment ? "appSettings.Development.json" : "appSettings.json";
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(appSettingsFile)
                .Build()
                .Get<TConfig>();
        }

        public void SaveConfig<TConfig>(TConfig config, bool isDevelopment = false)
        {
            string appSettingsFile = isDevelopment ? "appSettings.Development.json" : "appSettings.json";
            var jsonWriteOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            jsonWriteOptions.Converters.Add(new JsonStringEnumConverter());

            var jsonConfig = JsonSerializer.Serialize(config, jsonWriteOptions);
            var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appSettingsFile);
            File.WriteAllText(appSettingsPath, jsonConfig);
        }
    }
}
