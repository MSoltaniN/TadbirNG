using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// امکانات کمکی برای کار با پیکربندی برنامه ها را فراهم می کند
    /// </summary>
    public static class ConfigUtility
    {
        /// <summary>
        /// اطلاعات پیکربندی داده شده را بررسی کرده و تنظیمات موجود را داخل فایل متنی لاگ می کند
        /// </summary>
        /// <param name="config">پیکربندی داده شده برای بررسی</param>
        public static void InspectConfiguration(IConfiguration config)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Inspecting configuration...");
            if (config == null)
            {
                builder.AppendLine(String.Format($"WARNING: Configuration is null.{Environment.NewLine}"));
            }
            else
            {
                builder.AppendLine(String.Join(Environment.NewLine, config
                    .AsEnumerable()
                    .Select(item => String.Format($"{item.Key} = {item.Value}"))));
            }

            File.WriteAllText("startup.log", builder.ToString());
        }
    }
}
