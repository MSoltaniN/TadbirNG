using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SPPC.Framework.Common;

namespace SPPC.Framework.Cryptography
{
    /// <summary>
    /// Provides cryptographic operations required for protecting sensitive data using relatively strong
    /// cryptographic algorithms.
    /// </summary>
    public class CryptoService : ICryptoService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoService"/> class.
        /// </summary>
        /// <param name="certificateManager">Provides services for managing X509 certificates</param>
        public CryptoService(ICertificateManager certificateManager)
        {
            CertificateManager = certificateManager;
        }

        /// <summary>
        /// Gets an instance with default implementation
        /// </summary>
        public static ICryptoService Default
        {
            get { return new CryptoService(new CertificateManager()); }
        }

        /// <summary>
        /// Gets a service for managing X509 certificates
        /// </summary>
        public ICertificateManager CertificateManager { get; }

        /// <summary>
        /// Transforms given text data to a cryptographic hash value using a standard hashing algorithm.
        /// Current implementation uses SHA256 data hashing algorithm.
        /// </summary>
        /// <param name="data">Text data that needs to be hashed</param>
        /// <returns>SHA256 hash of given data in Hexadecimal form</returns>
        public string CreateHash(string data)
        {
            Verify.ArgumentNotNullOrEmptyString(data, nameof(data));
            var dataBytes = Encoding.UTF8.GetBytes(data);
            return CreateHash(dataBytes);
        }

        /// <summary>
        /// Transforms given binary data to a cryptographic hash value using a standard hashing algorithm.
        /// Current implementation uses SHA256 data hashing algorithm.
        /// </summary>
        /// <param name="data">Binary data that needs to be hashed</param>
        /// <returns>SHA256 hash of given data in hexadecimal form</returns>
        public string CreateHash(byte[] data)
        {
            Verify.ArgumentNotNullOrEmpty(data, nameof(data));
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(data);
            return Transform
                .ToHexString(hash)
                .ToLower();
        }

        /// <summary>
        /// Validates given text data against a hash value
        /// </summary>
        /// <param name="data">Text data that needs to be validated.</param>
        /// <param name="hash">Hash value to use for validation</param>
        /// <returns>True if input data is valid, otherwise false.</returns>
        public bool ValidateHash(string data, string hash)
        {
            Verify.ArgumentNotNullOrEmptyString(data, nameof(data));
            Verify.ArgumentNotNullOrEmptyString(hash, nameof(hash));

            var dataBytes = Encoding.UTF8.GetBytes(data);
            return ValidateHash(dataBytes, hash);
        }

        /// <summary>
        /// Validates given binary data against a SHA256 hash value
        /// </summary>
        /// <param name="data">Binary data that needs to be validated.</param>
        /// <param name="hash">A SHA256 hash value to use for validation</param>
        /// <returns>True if input data is valid, otherwise false.</returns>
        public bool ValidateHash(byte[] data, string hash)
        {
            Verify.ArgumentNotNullOrEmpty(data, nameof(data));
            Verify.ArgumentNotNullOrEmptyString(hash, nameof(hash));

            using var sha256 = SHA256.Create();
            var dataHashBytes = sha256.ComputeHash(data);
            string dataHash = Transform
                .ToHexString(dataHashBytes)
                .ToLower();
            return String.Compare(dataHash, hash, true) == 0;
        }

        /// <summary>
        /// Converts input string data to encrypted form with a string representation
        /// </summary>
        /// <param name="data">String data to encrypt</param>
        /// <returns>Encrypted data as a Base64 string</returns>
        public string Encrypt(string data)
        {
            Verify.ArgumentNotNullOrEmptyString(data, nameof(data));

            byte[] cipher;
            byte[] wrappedCipher;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (var memStream = new MemoryStream())
                {
                    using var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);
                    using (var cryptoWriter = new StreamWriter(cryptoStream))
                    {
                        cryptoWriter.Write(data);
                    }

                    cipher = memStream.ToArray();
                }

                wrappedCipher = WrapCipher(cipher, aes.Key, aes.IV);
            }

            return Transform.ToBase64String(wrappedCipher);
        }

        /// <summary>
        /// Converts input text data to encrypted form with Base64 representation
        /// </summary>
        /// <param name="data">Text data to encrypt</param>
        /// <param name="certificate">Certificate to use for PGP (Pretty Good Privacy) key exchange</param>
        /// <returns>Base64 representation of encrypted data</returns>
        public string Encrypt(string data, X509Certificate2 certificate)
        {
            Verify.ArgumentNotNullOrEmptyString(data, nameof(data));
            Verify.ArgumentNotNull(certificate, nameof(certificate));

            using Aes aes = Aes.Create();
            aes.KeySize = _KeySizeBits;
            aes.BlockSize = _IvSizeBits;
            var encryptor = aes.CreateEncryptor();
            using var memStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);
            using (var cryptoWriter = new StreamWriter(cryptoStream))
            {
                cryptoWriter.Write(data);
            }

            var dataCipher = memStream.ToArray();
            var keyCipher = EncryptKey(aes, certificate);
            return WrapCipher(dataCipher, keyCipher);
        }

        /// <summary>
        /// Converts string representation of previously encrypted data to original data
        /// </summary>
        /// <param name="cipher">Previously encrypted data as a Base64 string</param>
        /// <returns>Original data retrieved from encrypted form</returns>
        public string Decrypt(string cipher)
        {
            Verify.ArgumentNotNullOrEmptyString(cipher, nameof(cipher));

            byte[] cipherBytes = Transform.FromBase64String(cipher);
            byte[] unwrappedCipher = UnwrapCipher(cipherBytes, out byte[] key, out byte[] iv);
            string data;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using var memStream = new MemoryStream(unwrappedCipher);
                using var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);
                using var cryptoReader = new StreamReader(cryptoStream);
                data = cryptoReader.ReadToEnd();
            }

            return data;
        }

        /// <summary>
        /// Converts Base64 representation of previously encrypted data to original data
        /// </summary>
        /// <param name="cipher">Previously encrypted data in Base64 form</param>
        /// <param name="certificate">Certificate to use for PGP (Pretty Good Privacy) key exchange</param>
        /// <returns>Original data retrieved from encrypted form</returns>
        public string Decrypt(string cipher, X509Certificate2 certificate)
        {
            Verify.ArgumentNotNullOrEmptyString(cipher, nameof(cipher));
            Verify.ArgumentNotNull(certificate, nameof(certificate));

            UnwrapCipher(cipher, out byte[] dataCipher, out byte[] keyCipher);
            UnwrapKey(DecryptKey(keyCipher, certificate), out byte[] key, out byte[] iv);
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memStream = new MemoryStream(dataCipher);
            using var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);
            using var cryptoReader = new StreamReader(cryptoStream);
            return cryptoReader.ReadToEnd();
        }

        /// <summary>
        /// اطلاعات باینری داده شده را امضای دیجیتالی می کند
        /// </summary>
        /// <param name="data">اطلاعات مورد نظر برای امضا</param>
        /// <param name="certificate">گواهینامه امنیتی مورد نظر برای انجام عملیات</param>
        /// <returns>امضای دیجیتالی اطلاعات داده شده به شکل متنی</returns>
        public string SignData(byte[] data, X509Certificate2 certificate)
        {
            string signature = String.Empty;
            if (certificate != null)
            {
                var dataHash = Transform.FromHexString(CreateHash(data));
                var rsa = (RSA)certificate.PrivateKey;
                byte[] signatureBytes = rsa.SignHash(dataHash,
                    HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                signature = Convert.ToBase64String(signatureBytes);
            }

            return signature;
        }

        /// <summary>
        /// اطلاعات باینری داده شده را با توجه به امضای دیجیتالی داده شده تأیید یا رد می کند
        /// </summary>
        /// <param name="data">اطلاعات مورد نظر برای تأیید امضا</param>
        /// <param name="signature">امضای دیجیتالی مورد استفاده برای تأیید اطلاعات</param>
        /// <param name="certificate">گواهینامه امنیتی مورد نظر برای انجام عملیات</param>
        /// <returns>در صورت درستی اطلاعات داده شده مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        public bool VerifyData(byte[] data, string signature, X509Certificate2 certificate)
        {
            bool validated = false;
            if (certificate != null)
            {
                var dataHash = Transform.FromHexString(CreateHash(data));
                var rsa = (RSA)certificate.PublicKey.Key;
                validated = rsa.VerifyHash(dataHash, Convert.FromBase64String(signature),
                    HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }

            return validated;
        }

        private static byte[] WrapCipher(byte[] cipher, byte[] key, byte[] iv)
        {
            return cipher
                .Concat(iv)
                .Concat(key)
                .ToArray();
        }

        private static byte[] UnwrapCipher(byte[] cipher, out byte[] key, out byte[] iv)
        {
            key = new byte[_KeySize];
            iv = new byte[_IvSize];
            Array.Copy(cipher, cipher.Length - _KeySize, key, 0, _KeySize);
            Array.Copy(cipher, cipher.Length - _KeySize - _IvSize, iv, 0, _IvSize);
            byte[] unwrappedCipher = new byte[cipher.Length - _KeySize - _IvSize];
            Array.Copy(cipher, 0, unwrappedCipher, 0, unwrappedCipher.Length);
            return unwrappedCipher;
        }

        private static byte[] EncryptKey(SymmetricAlgorithm alg, X509Certificate2 certificate)
        {
            var key = alg.Key
                .Concat(alg.IV)
                .ToArray();
            var publicKey = (RSA)certificate.PublicKey.Key;
            return publicKey.Encrypt(key, RSAEncryptionPadding.OaepSHA256);
        }

        private static byte[] DecryptKey(byte[] key, X509Certificate2 certificate)
        {
            var privateKey = (RSA)certificate.PrivateKey;
            return privateKey.Decrypt(key, RSAEncryptionPadding.OaepSHA256);
        }

        private static string WrapCipher(byte[] data, byte[] key)
        {
            return Transform.ToBase64String(key
                .Concat(data)
                .ToArray());
        }

        private static void UnwrapCipher(string cipher, out byte[] data, out byte[] key)
        {
            var cipherBytes = Transform.FromBase64String(cipher);
            key = new byte[_KeyCipherSize];
            data = new byte[cipherBytes.Length - _KeyCipherSize];
            Array.Copy(cipherBytes, 0, key, 0, _KeyCipherSize);
            Array.Copy(cipherBytes, _KeyCipherSize, data, 0, data.Length);
        }

        private static void UnwrapKey(byte[] wrappedKey, out byte[] key, out byte[] iv)
        {
            key = new byte[_KeySize];
            iv = new byte[_IvSize];
            Array.Copy(wrappedKey, 0, key, 0, _KeySize);
            Array.Copy(wrappedKey, _KeySize, iv, 0, _IvSize);
        }

        private const int _KeySize = 32;
        private const int _IvSize = 16;
        private const int _KeyCipherSize = 256;
        private const int _KeySizeBits = 256;
        private const int _IvSizeBits = 128;
    }
}
