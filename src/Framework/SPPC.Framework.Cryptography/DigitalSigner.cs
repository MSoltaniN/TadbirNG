using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SPPC.Framework.Cryptography
{
    public class DigitalSigner : IDigitalSigner
    {
        public DigitalSigner(ICryptoService crypto)
        {
            _crypto = crypto;
        }

        public X509Certificate2 Certificate { get; set; }

        public string SignData(byte[] data)
        {
            string signature = String.Empty;
            if (Certificate != null)
            {
                var dataHash = _crypto.CreateHash(data);
                var rsa = (RSACng)Certificate.PrivateKey;
                byte[] signatureBytes = rsa.SignHash(dataHash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                signature = Convert.ToBase64String(signatureBytes);
            }

            return signature;
        }

        public bool VerifyData(byte[] data, string signature)
        {
            bool validated = false;
            if (Certificate != null)
            {
                byte[] dataHash = _crypto.CreateHash(data);
                var rsa = (RSACng)Certificate.PublicKey.Key;
                validated = rsa.VerifyHash(dataHash, Convert.FromBase64String(signature),
                    HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }

            return validated;
        }

        private readonly ICryptoService _crypto;
    }
}
