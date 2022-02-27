using System;
using System.Text;
using NetMQ;
using NetMQ.Sockets;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Framework.Licensing
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن شناسه سخت افزاری یک وسیله را پیاده سازی می کند
    /// </summary>
    public class NetDeviceIdProvider : IDeviceIdProvider
    {
        /// <summary>
        /// شناسه سخت افزاری یک وسیله را از راه دور خوانده و برمی گرداند
        /// </summary>
        /// <returns>شناسه سخت افزاری وسیله مورد نظر</returns>
        /// <remarks>خواندن شناسه سخت افزاری از طریق اتصال به سرویس خاص و با پروتکل تی سی پی انجام می شود</remarks>
        public string GetRemoteDeviceId(RemoteConnection connection)
        {
            Verify.ArgumentNotNull(connection, nameof(connection));

            string address = String.Format($"tcp://{connection.Domain}:{connection.Port}");
            using var requester = new RequestSocket();
            requester.Connect(address);
            requester.SendFrame(HardwareKeyQuery);
            var response = requester.ReceiveFrameString();
            return Encode(response);
        }

        private static string Encode(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        private const string HardwareKeyQuery = "hwkey";
    }
}
