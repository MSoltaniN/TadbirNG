using System;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace SPPC.Tadbir.Resources
{
    /// <summary>
    /// متدهای کمکی برای استفاده ساده تر از کلاس مربوطه را پیاده سازی می کند. مزیت اصلی استفاده از این متدها این است که
    /// همیشه متن ترجمه شده را به صورت یک آبجکت رشته برمی گردانند.
    /// </summary>
    public static class StringLocalizerFormatter
    {
        /// <summary>
        /// متن چندزبانه با کلید داده شده را با توجه به تنظیمات زبانی جاری برمی گرداند
        /// </summary>
        /// <param name="localizer">رفرنس آبجکت توسعه یافته</param>
        /// <param name="resourceKey">کلید یکی از متن های چندزبانه موجود</param>
        /// <returns>متن چندزبانه به زبان جاری</returns>
        public static string Format(this IStringLocalizer localizer, string resourceKey)
        {
            return localizer[resourceKey];
        }

        /// <summary>
        /// متن چند زبانه با کلید داده شده را با توجه به تنظیمات زبانی جاری و آرگومان های داده شده برمی گرداند
        /// </summary>
        /// <param name="localizer">رفرنس آبجکت توسعه یافته</param>
        /// <param name="resourceKey">کلید یکی از متن های چندزبانه موجود</param>
        /// <param name="argKeys">کلیدهای آرگومان های موجود در متن چندزبانه</param>
        /// <returns>متن چندزبانه به زبان جاری</returns>
        /// <remarks>هنگام استفاده از این متد توجه داشته باشید که آرگومان ها حتماً باید کلیدهای تعریف شده
        /// برای متن های چندزبانه باشند. درغیر این صورت در اثر نگاشت آنها ممکن است متن نادرستی به دست بیاید.
        /// </remarks>
        public static string Format(
            this IStringLocalizer localizer, string resourceKey, params string[] argKeys)
        {
            var argStrings = argKeys
                .Select(key => localizer[key].Value)
                .ToArray();
            return String.Format(localizer[resourceKey], argStrings);
        }
    }
}
