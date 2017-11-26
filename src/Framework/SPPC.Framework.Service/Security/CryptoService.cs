using System;
using System.Security.Cryptography;
using System.Text;
using SPPC.Framework.Common;

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

        private SHA256CryptoServiceProvider _hashProvider;
        private bool _disposed = false;
    }
}
