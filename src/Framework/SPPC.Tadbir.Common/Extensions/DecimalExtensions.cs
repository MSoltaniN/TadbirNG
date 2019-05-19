using System;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Extensions
{
    public static class DecimalExtensions
    {
        public static bool AlmostEquals(this decimal thisDecimal, decimal other)
        {
            return Math.Abs(thisDecimal - other) <= AppConstants.RoundingPrecision;
        }
    }
}
