using SPPC.Framework.Helpers;

namespace SPPC.Framework.Licensing
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن شناسه سخت افزاری یک وسیله را تعریف می کند
    /// </summary>
    public interface IDeviceIdProvider
    {
        /// <summary>
        /// شناسه سخت افزاری یک وسیله را از راه دور خوانده و برمی گرداند
        /// </summary>
        /// <returns>شناسه سخت افزاری وسیله مورد نظر</returns>
        /// <remarks>پروتکل و تنظیمات مورد نیاز برای ارتباط از راه دور
        /// توسط پیاده سازی مورد نظر انتخاب و اعمال می شود</remarks>
        string GetRemoteDeviceId(RemoteConnection connection);
    }
}
