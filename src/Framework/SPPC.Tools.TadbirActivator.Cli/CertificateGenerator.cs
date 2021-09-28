using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using SPPC.Licensing.Model;

namespace SPPC.Tools.TadbirActivator.Cli
{
    public class CertificateGenerator
    {
        public X509Certificate2 GenerateSelfSigned()
        {
            X509Certificate2 certificate;
            using (var issuerKey = RSA.Create(Constants.IssuerKeySizeInBits))
            {
                using (var certKey = RSA.Create(Constants.CertKeySizeInBits))
                {
                    var parentReq = new CertificateRequest(
                        Constants.IssuerName, issuerKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    parentReq.CertificateExtensions.Add(
                        new X509BasicConstraintsExtension(true, false, 0, true));

                    parentReq.CertificateExtensions.Add(
                        new X509SubjectKeyIdentifierExtension(parentReq.PublicKey, false));

                    using (var parentCert = parentReq.CreateSelfSigned(
                        DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2)))
                    {
                        var req = new CertificateRequest(
                            Constants.SubjectName, certKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

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
                }
            }

            return certificate;
        }
    }
}
