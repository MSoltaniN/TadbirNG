using System;
using System.Linq;
using SPPC.Framework.Common;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class EnvironmentHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static string GetProcessVariable(string variable)
        {
            return GetVariable(variable, EnvironmentVariableTarget.Process);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        public static void AddProcessVariable(string variable, string value)
        {
            Verify.ArgumentNotNullOrEmptyString(value, nameof(value));
            var currentValue = GetProcessVariable(variable);
            if (currentValue != null)
            {
                currentValue = String.Join(';', currentValue, value);
            }
            else
            {
                currentValue = value;
            }

            Environment.SetEnvironmentVariable(variable, currentValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static string GetMachineVariable(string variable)
        {
            return GetVariable(variable, EnvironmentVariableTarget.Machine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public static string FindMachineItemFromPart(string variable, string part)
        {
            Verify.ArgumentNotNullOrEmptyString(part, nameof(part));
            var machineItem = String.Empty;
            var value = GetMachineVariable(variable);
            if (value != null)
            {
                machineItem = value
                    .Split(';')
                    .Where(item => item.Contains(part))
                    .FirstOrDefault();
            }

            return machineItem;
        }

        private static string GetVariable(string variable, EnvironmentVariableTarget target)
        {
            Verify.ArgumentNotNullOrEmptyString(variable, nameof(variable));
            return Environment.GetEnvironmentVariable(variable, target);
        }
    }
}
