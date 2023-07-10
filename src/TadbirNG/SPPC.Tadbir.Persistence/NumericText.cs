using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tadbir.Domain;

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
            var newValue = String.Empty;
            if (existingValues.Any())
            {
                newValue = GetNewCodeValue(existingValues, 0);
            }

            return newValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetNewCodeValue(int length)
        {
            Verify.ArgumentNotOutOfRange(length, 1, AppConstants.MaxCodeLength);
            return GetNewCodeValue(Array.Empty<string>(), length);
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

        private static string GetNewCodeValue(IEnumerable<string> existingValues, int length)
        {
            int maxLength = existingValues.Any()
                ? existingValues.Max(value => value.Length)
                : length;
            string format = String.Format("D{0}", maxLength);
            var maxValue = (long)Math.Pow(10, maxLength) - 1;
            var lastValue = existingValues.Any()
                ? Int64.Parse(existingValues.Max())
                : 0L;
            var newValue = Math.Min(lastValue + 1, maxValue);
            return newValue.ToString(format);
        }
    }
}
