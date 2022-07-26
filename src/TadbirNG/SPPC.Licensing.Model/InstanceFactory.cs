using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;

namespace SPPC.Licensing.Model
{
    public static class InstanceFactory
    {
        public static InstanceModel FromLicense(LicenseModel license)
        {
            return new InstanceModel()
            {
                CustomerKey = license?.CustomerKey,
                LicenseKey = license?.LicenseKey
            };
        }

        public static InstanceModel FromCrypto(string instance)
        {
            Verify.ArgumentNotNullOrEmptyString(instance, nameof(instance));
            return JsonHelper.To<InstanceModel>(
                CryptoService.Default.Decrypt(instance));
        }

        public static string CryptoFromLicense(LicenseModel license)
        {
            return CryptoService.Default.Encrypt(
                JsonHelper.From(
                    FromLicense(license), false));
        }
    }
}
