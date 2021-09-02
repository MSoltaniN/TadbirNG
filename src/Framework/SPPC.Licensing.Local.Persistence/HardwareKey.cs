using System;
using System.Text;
using DeviceId;
using DeviceId.Encoders;
using DeviceId.Formatters;

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
        public static string UniqueKey
        {
            get
            {
                if (String.IsNullOrEmpty(_uniqueKey))
                {
                    _uniqueKey = Encode(new DeviceIdBuilder()
                        .AddProcessorId()
                        .AddMotherboardSerialNumber()
                        .AddSystemUUID()
                        .AddSystemDriveSerialNumber()
                        .UseFormatter(new StringDeviceIdFormatter(new PlainTextDeviceIdComponentEncoder()))
                        .ToString());
                }

                return _uniqueKey;
            }
        }

        private static string Encode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        private static string _uniqueKey;
    }
}
