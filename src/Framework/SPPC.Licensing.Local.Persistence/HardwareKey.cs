using System;
using System.Text;

namespace SPPC.Licensing.Local.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public static class HardwareKey
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static string GetProcessorId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static string GetVolumeSerial()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static string GetMainBoardSerial()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static string GetFirstMacAddress()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static string GetSystemUniqueId()
        {
            string uniqueId = String.Empty;
            var items = new string[]
            {
                GetProcessorId(),
                GetVolumeSerial(),
                GetMainBoardSerial(),
                GetFirstMacAddress()
            };

            if (items.Length > 0)
            {
                uniqueId = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(
                        String.Join("$", items)));
            }

            return uniqueId;
        }
    }
}
