using System;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Extensions
{
    /// <summary>
    /// توابع کمکی برای کار با اعداد اعشاری با دقت بالا را تعریف می کند
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// برابری دو عدد اعشاری با دقت بالا را با در نظر گرفتن دقت اعشاری پیش فرض سیستم تعیین می کند
        /// </summary>
        /// <param name="thisDecimal">عدد اعشاری جاری</param>
        /// <param name="other">عدد اعشاری مورد مقایسه</param>
        /// <returns>اگر دو عدد با دقت اعشاری پیش فرض سیستم با هم برابر باشند،
        /// مقدار "درست" و در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        public static bool AlmostEquals(this decimal thisDecimal, decimal other)
        {
            return Math.Abs(thisDecimal - other) <= AppConstants.RoundingPrecision;
        }
    }
}
