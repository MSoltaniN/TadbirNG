using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SPPC.Framework.Cryptography
{
    /// <summary>
    /// عملیات مورد نیاز برای امضای دیجیتالی و تأیید امضا را
    /// با استفاده از الگوریتم رمزگذاری نامتقارن آر اِس اِی پیاده سازی می کند
    /// </summary>
    public class DigitalSigner : IDigitalSigner
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="crypto">امکان انجام عملیات درهم سازی و رمزنگاری متقارن را فراهم می کند</param>
        public DigitalSigner(ICryptoService crypto)
        {
            _crypto = crypto;
        }

        /// <summary>
        /// گواهینامه امنیتی مورد نظر برای انجام عملیات
        /// </summary>
        public X509Certificate2 Certificate { get; set; }

        /// <summary>
        /// اطلاعات باینری داده شده را امضای دیجیتالی می کند
        /// </summary>
        /// <param name="data">اطلاعات مورد نظر برای امضا</param>
        /// <returns>امضای دیجیتالی اطلاعات داده شده به شکل متنی</returns>
        public string SignData(byte[] data)
        {
            string signature = String.Empty;
            if (Certificate != null)
            {
                var dataHash = _crypto.CreateHash(data);
                var rsa = (RSA)Certificate.PrivateKey;
                byte[] signatureBytes = rsa.SignHash(dataHash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                signature = Convert.ToBase64String(signatureBytes);
            }

            return signature;
        }

        /// <summary>
        /// اطلاعات باینری داده شده را با توجه به امضای دیجیتالی داده شده تأیید یا رد می کند
        /// </summary>
        /// <param name="data">اطلاعات مورد نظر برای تأیید امضا</param>
        /// <param name="signature">امضای دیجیتالی مورد استفاده برای تأیید اطلاعات</param>
        /// <returns>در صورت درستی اطلاعات داده شده مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public bool VerifyData(byte[] data, string signature)
        {
            bool validated = false;
            if (Certificate != null)
            {
                byte[] dataHash = _crypto.CreateHash(data);
                var rsa = (RSA)Certificate.PublicKey.Key;
                validated = rsa.VerifyHash(dataHash, Convert.FromBase64String(signature),
                    HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }

            return validated;
        }

        private readonly ICryptoService _crypto;
    }
}
