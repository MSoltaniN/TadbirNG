using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using SPPC.Framework.Values;

namespace SPPC.Framework.Cryptography
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت گواهینامه های امنیتی را پیاده سازی می کند
    /// </summary>
    public class CertificateManager : ICertificateManager
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند
        /// </summary>
        public CertificateManager()
        {
        }

        /// <summary>
        /// یک گواهینامه امنیتی جدید خودامضا با مشخصات داده شده را ایجاد کرده و برمی گرداند
        /// </summary>
        /// <param name="issuerName">نام مورد نظر برای صادرکننده گواهینامه</param>
        /// <param name="subjectName">نام مورد نظر برای موضوع گواهینامه</param>
        /// <returns>گواهینامه خودامضای جدید</returns>
        public X509Certificate2 GenerateSelfSigned(string issuerName, string subjectName)
        {
            X509Certificate2 certificate;
            using (var issuerKey = RSA.Create(Constants.IssuerKeySizeInBits))
            {
                using var certKey = RSA.Create(Constants.CertKeySizeInBits);
                var parentReq = new CertificateRequest(
                    issuerName, issuerKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                parentReq.CertificateExtensions.Add(
                    new X509BasicConstraintsExtension(true, false, 0, true));

                parentReq.CertificateExtensions.Add(
                    new X509SubjectKeyIdentifierExtension(parentReq.PublicKey, false));

                using var parentCert = parentReq.CreateSelfSigned(
                    DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));
                var req = new CertificateRequest(
                    subjectName, certKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                req.CertificateExtensions.Add(
                    new X509BasicConstraintsExtension(false, false, 0, false));

                req.CertificateExtensions.Add(
                    new X509KeyUsageExtension(
                        X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.NonRepudiation, false));

                req.CertificateExtensions.Add(
                    new X509EnhancedKeyUsageExtension(
                        new OidCollection
                        {
                            new Oid("1.3.6.1.5.5.7.3.8")
                        },
                        true));

                req.CertificateExtensions.Add(
                    new X509SubjectKeyIdentifierExtension(req.PublicKey, false));

                var publicKeyCert = req.Create(
                    parentCert, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(1),
                    new byte[] { 1, 2, 3, 4 });
                certificate = publicKeyCert.CopyWithPrivateKey(certKey);
            }

            return certificate;
        }

        /// <summary>
        /// گواهینامه امنیتی را در انباره با مشخصات داده شده اضافه می کند
        /// </summary>
        /// <param name="certificate">گواهینامه مورد نظر برای اضافه کردن به انباره</param>
        /// <param name="name">نام انباره گواهینامه مورد نظر</param>
        /// <param name="location">موقعیت سیستمی انباره گواهینامه مورد نظر</param>
        public void AddToStore(X509Certificate2 certificate, StoreName name, StoreLocation location)
        {
            try
            {
                var store = new X509Store(name, location);
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);
                store.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}ERROR: Could not add certificate to store {1} in {2}.",
                    Environment.NewLine, name.ToString(), location.ToString());
                Console.WriteLine("Reason : {0}", ex.Message);
            }
        }

        /// <summary>
        /// اولین گواهینامه امنیتی موجود با نام صادرکننده داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="issuerName">نام صادرکننده مورد نظر برای خواندن گواهینامه</param>
        /// <returns>گواهینامه امنیتی خوانده شده یا رفرنس بدون مقدار در صورت پیدا نشدن گواهینامه</returns>
        public X509Certificate2 GetFromStore(string issuerName)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            var foundItems = store.Certificates.Find(X509FindType.FindByIssuerDistinguishedName, issuerName, true);
            return foundItems.Count > 0
                ? foundItems[0]
                : null;
        }

        /// <summary>
        /// گواهینامه امنیتی را از روی فایل مشخص شده بارگذاری کرده و برمی گرداند
        /// </summary>
        /// <param name="path">مسیر فیزیکی فایل گواهینامه</param>
        /// <param name="password">رمز مورد نیاز برای خواندن اطلاعات گواهینامه از روی فایل</param>
        /// <returns>گواهینامه بارگذاری شده از روی فایل</returns>
        /// <remarks>در صورت نادرست بودن رمز داده شده، خطا ایجاد می شود</remarks>
        public X509Certificate2 GetFromFile(string path, string password)
        {
            var rawData = File.ReadAllBytes(path);
            return new X509Certificate2(rawData, password);
        }
    }
}
