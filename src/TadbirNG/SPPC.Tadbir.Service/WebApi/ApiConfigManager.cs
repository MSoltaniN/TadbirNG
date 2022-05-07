using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// امکان مدیریت تنظیمات سرویس وب را در فایل پیکربندی فراهم می کند
    /// </summary>
    public class ApiConfigManager
    {
        /// <summary>
        /// تنظیمات سرویس وب را از فایل پیکربندی فعال بارگذاری کرده و برمی گرداند
        /// </summary>
        /// <typeparam name="TConfig">نوع مدل اطلاعاتی تنظیمات</typeparam>
        /// <param name="isDevelopment">مشخص می کند که تنظیمات در حالت توسعه مورد نظر است یا نه</param>
        /// <returns>تنظیمات سرویس وب از نوع مدل اطلاعاتی شخص شده</returns>
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

        /// <summary>
        /// تنظیمات داده شده برای سرویس وب را در فایل پیکربندی ذخیره می کند
        /// </summary>
        /// <typeparam name="TConfig">نوع مدل اطلاعاتی تنظیمات</typeparam>
        /// <param name="config">تنظیمات مورد نظر برای ذخیره</param>
        /// <param name="isDevelopment">مشخص می کند که تنظیمات در حالت توسعه مورد نظر است یا نه</param>
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
