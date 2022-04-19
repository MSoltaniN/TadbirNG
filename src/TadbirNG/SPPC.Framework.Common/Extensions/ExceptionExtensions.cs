using System;
using System.Text;
using SPPC.Framework.Common;

namespace SPPC.Framework.Extensions
{
    /// <summary>
    /// عملیات تکمیلی مورد نیاز برای کار با استثنائات را به کلاس موجود در دات نت اضافه می کند
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// اطلاعات کامل خطا در زنجیره استثنائات را به صورت رشته متنی چندخطی برمی گرداند
        /// </summary>
        /// <param name="exception">آبجکت مورد نظر</param>
        /// <returns>اطلاعات کامل خطا در زنجیره استثنائات آبجکت مورد نظر</returns>
        public static string GetErrorInfo(this Exception exception)
        {
            Verify.ArgumentNotNull(exception, nameof(exception));
            var infoBuilder = new StringBuilder();
            infoBuilder.AppendLine(GetFullErrorInfo(exception));
            var inner = exception.InnerException;
            while (inner != null)
            {
                infoBuilder.AppendLine(GetFullErrorInfo(inner));
                inner = inner.InnerException;
            }

            return infoBuilder.ToString();
        }

        private static string GetFullErrorInfo(Exception exception)
        {
            Verify.ArgumentNotNull(exception, nameof(exception));
            var infoBuilder = new StringBuilder(exception.Message);
            if (exception.Data != null)
            {
                foreach (var key in exception.Data.Keys)
                {
                    infoBuilder.AppendFormat($"{Environment.NewLine}[{key}] = {exception.Data[key]}");
                }
            }

            return infoBuilder.ToString();
        }
    }
}
