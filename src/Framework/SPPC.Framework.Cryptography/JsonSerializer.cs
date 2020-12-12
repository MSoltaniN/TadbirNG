using System;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Cryptography
{
    /// <summary>
    /// عملیات مورد نیاز برای تبدیل آبجکت های سی شارپ به رشته کدشده و بالعکس را پیاده سازی می کند
    /// </summary>
    /// <remarks>این کلاس از قالب متنی جیسان برای تبدیل دوطرفه آبجکت ها استفاده می کند</remarks>
    public class JsonSerializer : IEncodedSerializer
    {
        /// <summary>
        /// آبجکت داده شده را به متن کدشده تبدیل کرده و برمی گرداند
        /// </summary>
        /// <typeparam name="T">نوع کلاس آبجکت داده شده برای تبدیل</typeparam>
        /// <param name="data">آبجکت داده شده برای تبدیل</param>
        /// <returns>شکل متنی و کدشده اطلاعات آبجکت داده شده</returns>
        public string Serialize<T>(T data)
        {
            Verify.ArgumentNotNull(data, nameof(data));
            string json = JsonHelper.From(data, false);
            var jsonBytes = Encoding.UTF8.GetBytes(json);
            return Convert.ToBase64String(jsonBytes);
        }

        /// <summary>
        /// متن کدشده داده شده را تبدیل به آبجکت اولیه می کند
        /// </summary>
        /// <typeparam name="T">نوع کلاس آبجکت مورد نظر برای تبدیل</typeparam>
        /// <param name="encoded">شکل متنی کدشده داده شده برای تبدیل</param>
        /// <returns>آبجکت تبدیل شده</returns>
        public T Deserialize<T>(string base64)
        {
            Verify.ArgumentNotNullOrEmptyString(base64, nameof(base64));
            var jsonBytes = Convert.FromBase64String(base64);
            string json = Encoding.UTF8.GetString(jsonBytes);
            return JsonHelper.To<T>(json);
        }
    }
}
