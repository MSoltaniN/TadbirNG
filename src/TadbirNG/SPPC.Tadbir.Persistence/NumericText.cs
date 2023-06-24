using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public class NumericText
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="existingValues"></param>
        /// <returns></returns>
        public static string GetNewCodeValue(IEnumerable<string> existingValues)
        {
            int maxLength = existingValues.Max(value => value.Length);
            string format = String.Format("D{0}", maxLength);
            var maxValue = (long)Math.Pow(10, maxLength) - 1;
            var lastValue = existingValues.Any()
                ? Int64.Parse(existingValues.Max())
                : 0L;
            var newValue = Math.Min(lastValue + 1, maxValue);
            return newValue.ToString(format);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="existingValues"></param>
        /// <returns></returns>
        public static string GetNewNumberValue(IEnumerable<string> existingValues)
        {
            if (existingValues.Any(value => !Int64.TryParse(value, out long number)))
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Sequence contains one or more non-numeric values.", nameof(existingValues));
            }

            var lastValue = existingValues
                .Select(value => Int64.Parse(value))
                .OrderByDescending(num => num)
                .FirstOrDefault();
            var newValue = Math.Min(lastValue + 1, Int64.MaxValue);
            return newValue.ToString();
        }
    }
}
