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
        /// <param name="length"></param>
        public NumericText(int length)
        {
            Verify.ArgumentNotOutOfRange(length, 1, AppConstants.MaxCodeLength, nameof(length));
            Length = length;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="existingValues"></param>
        /// <returns></returns>
        public string GetNewCodeValue(IEnumerable<string> existingValues)
        {
            string format = String.Format("D{0}", Length);
            var maxValue = (long)Math.Pow(10, Length) - 1;
            var lastValue = existingValues.Any()
                ? Int64.Parse(existingValues.Max())
                : 0;
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
            if (existingValues.Any(value => !Int32.TryParse(value, out int num)))
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Sequence contains one or more non-numeric values.", nameof(existingValues));
            }

            int maxValue = existingValues
                .Select(value => Int32.Parse(value))
                .OrderByDescending(num => num)
                .FirstOrDefault();
            int newValue = Math.Min(maxValue + 1, Int32.MaxValue);
            return newValue.ToString();
        }
    }
}
