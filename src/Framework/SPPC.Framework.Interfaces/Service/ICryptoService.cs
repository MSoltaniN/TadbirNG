using System;
using System.Collections.Generic;

namespace SPPC.Framework.Service.Security
{
    /// <summary>
    /// Defines cryptographic operations required for protecting sensitive data.
    /// </summary>
    /// <remarks>An implementation is free to select any cryptographic algorithm available in .NET library for
    /// operations.
    /// </remarks>
    public interface ICryptoService
    {
        /// <summary>
        /// Transforms given binary data to a cryptographic hash value using a standard hashing algorithm.
        /// </summary>
        /// <param name="data">Binary data that needs to be hashed</param>
        /// <returns>Hash of given data</returns>
        byte[] CreateHash(byte[] data);

        /// <summary>
        /// Transforms given text data to a cryptographic hash value using a standard hashing algorithm.
        /// </summary>
        /// <param name="data">Text data that needs to be hashed</param>
        /// <returns>Hash of given data in string form, for example, Hexadecimal or Base64</returns>
        string CreateHash(string data);

        /// <summary>
        /// Validates given data against a hash value by computing data hash and comparing it to the given hash.
        /// </summary>
        /// <param name="data">Binary data that needs to be validated.</param>
        /// <param name="hash">Hash value to use for validation</param>
        /// <returns>True if input data is valid, otherwise false.</returns>
        bool ValidateHash(byte[] data, byte[] hash);

        /// <summary>
        /// Converts input string data to encrypted form with a string representation
        /// </summary>
        /// <param name="data">String data to encrypt</param>
        /// <returns>String representation of encrypted data</returns>
        string Encrypt(string data);

        /// <summary>
        /// Converts string representation of previously encrypted data to original data
        /// </summary>
        /// <param name="cipher">Previously encrypted data as a string representation</param>
        /// <returns>Original data retrieved from encrypted form</returns>
        string Decrypt(string cipher);
    }
}
