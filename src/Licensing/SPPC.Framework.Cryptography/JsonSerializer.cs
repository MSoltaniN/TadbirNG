using System;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Cryptography
{
    public class JsonSerializer
    {
        public string Serialize<T>(T data)
        {
            Verify.ArgumentNotNull(data, nameof(data));
            string json = JsonHelper.From(data, false);
            var jsonBytes = Encoding.UTF8.GetBytes(json);
            return Convert.ToBase64String(jsonBytes);
        }

        public T Deserialize<T>(string base64)
        {
            Verify.ArgumentNotNullOrEmptyString(base64, nameof(base64));
            var jsonBytes = Convert.FromBase64String(base64);
            string json = Encoding.UTF8.GetString(jsonBytes);
            return JsonHelper.To<T>(json);
        }
    }
}
