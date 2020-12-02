using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SPPC.Framework.Cryptography
{
    public class DigitalSigner
    {
        public DigitalSigner(X509Certificate2 certificate)
        {
            _crypto = new CryptoService();
            _manager = new CertificateManager();
            _certificate = certificate;
        }

        public string SignData(byte[] data)
        {
            string signature = String.Empty;
            var dataHash = _crypto.CreateHash(data);
            if (_certificate != null)
            {
                var rsa = (RSACng)_certificate.PrivateKey;
                byte[] signatureBytes = rsa.SignHash(dataHash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                signature = Convert.ToBase64String(signatureBytes);
            }

            return signature;
        }

        public bool VerifyData(byte[] data, string signature)
        {
            bool validated = false;
            if (_certificate != null)
            {
                byte[] dataHash = _crypto.CreateHash(data);
                var rsa = (RSACng)_certificate.PublicKey.Key;
                validated = rsa.VerifyHash(dataHash, Convert.FromBase64String(signature),
                    HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }

            return validated;
        }

        private readonly CryptoService _crypto;
        private readonly CertificateManager _manager;
        private readonly X509Certificate2 _certificate;
    }
}
