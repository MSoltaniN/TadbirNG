using System;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// کلاس های تنظیمات را برای استفاده سایر قسمت های برنامه می سازد
    /// </summary>
    public static class ConfigFactory
    {
        /// <summary>
        /// انواع مختلف کلاس های تنظیمات را از روی مقدار سریال شده متنی آنها می سازد
        /// </summary>
        /// <param name="modelType">نام کلاس تنظیمات</param>
        /// <param name="json">ریز اطلاعات تنظیمات به صورت سریال شده با فرمت JSON</param>
        /// <returns>کلاس تنظیمات ساخته شده</returns>
        public static object CreateFromJson(string json, string modelType)
        {
            Verify.ArgumentNotNullOrEmptyString(modelType, "modelType");
            Verify.ArgumentNotNullOrEmptyString(json, "json");
            object config = null;
            var type = Type.GetType(String.Format("{0}.{1}", _namespace, modelType));
            if (type != null)
            {
                config = JsonHelper.To(json, type);
            }

            return config;
        }

        private const string _namespace = "SPPC.Tadbir.Configuration.Models";
    }
}
