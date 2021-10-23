using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

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
            AsymmetricKeyParameter myCAprivateKey = null;
            GenerateRoot(issuerName, ref myCAprivateKey);
            var selfSigned = GenerateSelfSigned(subjectName, issuerName, myCAprivateKey);
            return selfSigned;
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

        private static SecureRandom GetSecureRandom()
        {
            byte[] seed = RandomGenerator.Generate(32);
            SecureRandom random = SecureRandom.GetInstance("SHA256PRNG");
            random.SetSeed(seed);
            return random;
        }

        private static X509Certificate2 GenerateRoot(string subjectName, ref AsymmetricKeyParameter caPrivateKey)
        {
            const int keyStrength = 2048;

            // Generating Random Numbers
            var random = GetSecureRandom();

            // The Certificate Generator
            var certificateGenerator = new X509V3CertificateGenerator();

            // Serial Number
            BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
            certificateGenerator.SetSerialNumber(serialNumber);

            // Signature Algorithm
            const string signatureAlgorithm = "SHA256WithRSA";
#pragma warning disable CS0618 // Type or member is obsolete
            certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);
#pragma warning restore CS0618 // Type or member is obsolete

            // Issuer and Subject Name
            var subjectDN = new X509Name(subjectName);
            X509Name issuerDN = subjectDN;
            certificateGenerator.SetIssuerDN(issuerDN);
            certificateGenerator.SetSubjectDN(subjectDN);

            // Valid For
            DateTime notBefore = DateTime.UtcNow.Date;
            DateTime notAfter = notBefore.AddYears(2);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            // Subject Public Key
            AsymmetricCipherKeyPair subjectKeyPair;
            var keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            subjectKeyPair = keyPairGenerator.GenerateKeyPair();

            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            // Generating the Certificate
            AsymmetricCipherKeyPair issuerKeyPair = subjectKeyPair;

            // Selfsign certificate
#pragma warning disable CS0618 // Type or member is obsolete
            Org.BouncyCastle.X509.X509Certificate certificate = certificateGenerator.Generate(issuerKeyPair.Private, random);
#pragma warning restore CS0618 // Type or member is obsolete
            var x509 = new X509Certificate2(certificate.GetEncoded());

            caPrivateKey = issuerKeyPair.Private;

            return x509;
        }

        private static X509Certificate2 GenerateSelfSigned(
            string subjectName, string issuerName, AsymmetricKeyParameter issuerPrivKey)
        {
            const int keyStrength = 2048;

            // Generating Random Numbers
            var random = GetSecureRandom();

            // The Certificate Generator
            var certificateGenerator = new X509V3CertificateGenerator();

            // Serial Number
            BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
            certificateGenerator.SetSerialNumber(serialNumber);

            // Signature Algorithm
            const string signatureAlgorithm = "SHA256WithRSA";
#pragma warning disable CS0618 // Type or member is obsolete
            certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);
#pragma warning restore CS0618 // Type or member is obsolete

            // Issuer and Subject Name
            var subjectDN = new X509Name(subjectName);
            var issuerDN = new X509Name(issuerName);
            certificateGenerator.SetIssuerDN(issuerDN);
            certificateGenerator.SetSubjectDN(subjectDN);

            // Valid For
            DateTime notBefore = DateTime.UtcNow.Date;
            DateTime notAfter = notBefore.AddYears(2);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            // Subject Public Key
            AsymmetricCipherKeyPair subjectKeyPair;
            var keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            subjectKeyPair = keyPairGenerator.GenerateKeyPair();

            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            // Selfsign certificate
#pragma warning disable CS0618 // Type or member is obsolete
            Org.BouncyCastle.X509.X509Certificate certificate = certificateGenerator.Generate(issuerPrivKey, random);
#pragma warning restore CS0618 // Type or member is obsolete

            // Corresponding private key
            PrivateKeyInfo info = PrivateKeyInfoFactory.CreatePrivateKeyInfo(subjectKeyPair.Private);

            // Merge into X509Certificate2
            var x509 = new X509Certificate2(certificate.GetEncoded());

            Asn1Sequence seq = (Asn1Sequence)Asn1Object.FromByteArray(info.ParsePrivateKey().GetDerEncoded());
            if (seq.Count != 9)
            {
                ////throw new PemException("malformed sequence in RSA private key");
            }

#pragma warning disable CS0618 // Type or member is obsolete
            var rsa = new RsaPrivateKeyStructure(seq);
#pragma warning restore CS0618 // Type or member is obsolete
            var rsaparams = new RsaPrivateCrtKeyParameters(
                rsa.Modulus, rsa.PublicExponent, rsa.PrivateExponent, rsa.Prime1, rsa.Prime2,
                rsa.Exponent1, rsa.Exponent2, rsa.Coefficient);

            x509.PrivateKey = DotNetUtilities.ToRSA(rsaparams);
            return x509;
        }
    }
}
