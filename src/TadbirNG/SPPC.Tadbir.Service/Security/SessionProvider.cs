using System;
using DeviceDetectorNET;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// عملیات مورد نیاز برای تشخیص اطلاعات یک جلسه کاری در برنامه را پیاده سازی می کند
    /// </summary>
    public class SessionProvider : ISessionProvider
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="crypto">امکان انجام عملیات رمزنگاری و درهم سازی را فراهم می کند</param>
        public SessionProvider(ICryptoService crypto)
        {
            _crypto = crypto;
        }

        /// <summary>
        /// اطلاعات کامل جلسه کاری فعلی کاربر در برنامه را تهیه کرده و برمی گرداند
        /// </summary>
        /// <param name="userAgent">اطلاعات فرستاده شده توسط مرورگر وب در هدر عامل کاربری</param>
        /// <param name="ipAddress">آدرس آی پی که از دستگاه کاربر به دست آمده است</param>
        /// <returns>اطلاعات کامل جلسه کاری فعلی کاربر در برنامه</returns>
        public SessionViewModel GetSession(string userAgent, string ipAddress)
        {
            Verify.ArgumentNotNullOrEmptyString(userAgent, nameof(userAgent));

            SessionViewModel session = null;
            var detector = new DeviceDetector(userAgent);
            detector.DiscardBotInformation();
            detector.AddClientsParser();
            detector.Parse();
            if (!detector.IsBot())
            {
                session = new SessionViewModel()
                {
                    Device = GetDevice(detector),
                    Browser = GetBrowser(detector),
                    SinceUtc = DateTime.UtcNow,
                    LastActivityUtc = DateTime.UtcNow,
                    IPAddress = ipAddress,
                    Fingerprint = GetFingerprint(detector, userAgent)
                };
            }

            return session;
        }

        private static string GetDevice(DeviceDetector detector)
        {
            string device;

            // NOTE: Brand and model for PC/Laptop devices cannot be detected from UserAgent...
            var brand = detector.GetBrandName();
            var model = detector.GetModel();
            if (String.IsNullOrEmpty(brand) || String.IsNullOrEmpty(model))
            {
                device = String.Format($"Desktop PC ({GetOS(detector)})");
            }
            else
            {
                device = String.Format(
                    $"{brand} {model} ({detector.GetDeviceName()}, {GetOS(detector)})");
            }

            return device;
        }

        private static string GetOS(DeviceDetector detector)
        {
            var os = String.Empty;
            var osResult = detector.GetOs();
            if (osResult.Success)
            {
                var match = osResult.Match;
                os = String.Format($"{match.Name} {match.Version}");
            }

            return os;
        }

        private static string GetBrowser(DeviceDetector detector)
        {
            var browser = String.Empty;
            var browserResult = detector.GetBrowserClient();
            if (browserResult.Success)
            {
                var match = browserResult.Match;
                browser = String.Format($"{match.Name} {match.Version}");
            }

            return browser;
        }

        private string GetFingerprint(DeviceDetector detector, string userAgent)
        {
            string fingerprint = String.Format(
                $"[{GetDevice(detector)}].[{GetBrowser(detector)}].[{userAgent}]");
            return _crypto.CreateHash(fingerprint).ToLower();
        }

        private readonly ICryptoService _crypto;
    }
}
