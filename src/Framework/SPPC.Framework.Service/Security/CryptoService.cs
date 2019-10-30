using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Tadbir.Values;

namespace SPPC.Framework.Service.Security
{
    /// <summary>
    /// Provides cryptographic operations required for protecting sensitive data using relatively strong
    /// cryptographic algorithms.
    /// </summary>
    public class CryptoService : ICryptoService, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryptoService"/> class.
        /// </summary>
        public CryptoService()
        {
            _hashProvider = new SHA256CryptoServiceProvider();
        }

        /// <summary>
        /// Transforms given binary data to a cryptographic hash value using a standard hashing algorithm.
        /// Current implementation uses SHA256 data hashing algorithm.
        /// </summary>
        /// <param name="data">Binary data that needs to be hashed</param>
        /// <returns>SHA256 hash of given data</returns>
        public byte[] CreateHash(byte[] data)
        {
            return _hashProvider.ComputeHash(data);
        }

        /// <summary>
        /// Transforms given text data to a cryptographic hash value using a standard hashing algorithm.
        /// Current implementation uses SHA256 data hashing algorithm.
        /// </summary>
        /// <param name="data">Text data that needs to be hashed</param>
        /// <returns>SHA256 hash of given data in Hexadecimal form</returns>
        public string CreateHash(string data)
        {
            Verify.ArgumentNotNullOrEmptyString(data, "data");
            var dataBytes = Encoding.UTF8.GetBytes(data);
            var dataHashBytes = _hashProvider.ComputeHash(dataBytes);
            return Transform.ToHexString(dataHashBytes);
        }

        /// <summary>
        /// Validates given data against a SHA256 hash value by computing data hash and comparing it to the given hash.
        /// </summary>
        /// <param name="data">Binary data that needs to be validated.</param>
        /// <param name="hash">A SHA256 hash value to use for validation</param>
        /// <returns>True if input data is valid, otherwise false.</returns>
        public bool ValidateHash(byte[] data, byte[] hash)
        {
            byte[] dataHash = _hashProvider.ComputeHash(data);
            string hexDataHash = Transform.ToHexString(dataHash);
            string hexHash = Transform.ToHexString(hash);
            return (hexDataHash == hexHash);
        }

        /// <summary>
        /// Converts input string data to encrypted form with a string representation
        /// </summary>
        /// <param name="data">String data to encrypt</param>
        /// <returns>String representation of encrypted data</returns>
        public string Encrypt(string data)
        {
            byte[] cipher;
            byte[] wrappedCipher;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter cryptoWriter = new StreamWriter(cryptoStream))
                        {
                            cryptoWriter.Write(data);
                        }

                        cipher = memStream.ToArray();
                    }
                }

                wrappedCipher = WrapCipher(cipher, aes.Key, aes.IV);
            }

            return Transform.ToBase64String(wrappedCipher);
        }

        /// <summary>
        /// Converts string representation of previously encrypted data to original data
        /// </summary>
        /// <param name="cipher">Previously encrypted data as a string representation</param>
        /// <returns>Original data retrieved from encrypted form</returns>
        public string Decrypt(string cipher)
        {
            byte[] key, iv;
            byte[] cipherBytes = Transform.FromBase64String(cipher);
            byte[] unwrappedCipher = UnwrapCipher(cipherBytes, out key, out iv);
            string data;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memStream = new MemoryStream(unwrappedCipher))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader cryptoReader = new StreamReader(cryptoStream))
                        {
                            data = cryptoReader.ReadToEnd();
                        }
                    }
                }
            }

            return data;
        }

        #region IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Supports correct implementation of the Disposable pattern for this class.
        /// </summary>
        /// <param name="disposing">Indicates if this instance is currently being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _hashProvider.Dispose();
                _disposed = true;
            }
        }

        #endregion

        private static byte[] WrapCipher(byte[] cipher, byte[] key, byte[] iv)
        {
            return cipher
                .Concat(iv)
                .Concat(key)
                .ToArray();
        }

        private static byte[] UnwrapCipher(byte[] cipher, out byte[] key, out byte[] iv)
        {
            int keyLength = AppConstants.CryptoKeySize;
            int ivLength = AppConstants.CryptoIvSize;
            key = new byte[keyLength];
            iv = new byte[ivLength];
            Array.Copy(cipher, cipher.Length - keyLength, key, 0, keyLength);
            Array.Copy(cipher, cipher.Length - keyLength - ivLength, iv, 0, ivLength);
            byte[] unwrappedCipher = new byte[cipher.Length - keyLength - ivLength];
            Array.Copy(cipher, 0, unwrappedCipher, 0, unwrappedCipher.Length);
            return unwrappedCipher;
        }

        private SHA256CryptoServiceProvider _hashProvider;
        private bool _disposed = false;
    }
}
