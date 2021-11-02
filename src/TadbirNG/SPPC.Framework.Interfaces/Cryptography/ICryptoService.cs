using System.Security.Cryptography.X509Certificates;

namespace SPPC.Framework.Cryptography
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
        /// Provides services for managing X509 certificates
        /// </summary>
        ICertificateManager CertificateManager { get; }

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

        /// <summary>
        /// اطلاعات باینری داده شده را امضای دیجیتالی می کند
        /// </summary>
        /// <param name="data">اطلاعات مورد نظر برای امضا</param>
        /// <param name="certificate">گواهینامه امنیتی مورد نظر برای انجام عملیات</param>
        /// <returns>امضای دیجیتالی اطلاعات داده شده به شکل متنی</returns>
        string SignData(byte[] data, X509Certificate2 certificate);

        /// <summary>
        /// اطلاعات باینری داده شده را با توجه به امضای دیجیتالی داده شده تأیید یا رد می کند
        /// </summary>
        /// <param name="data">اطلاعات مورد نظر برای تأیید امضا</param>
        /// <param name="signature">امضای دیجیتالی مورد استفاده برای تأیید اطلاعات</param>
        /// <param name="certificate">گواهینامه امنیتی مورد نظر برای انجام عملیات</param>
        /// <returns>در صورت درستی اطلاعات داده شده مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        bool VerifyData(byte[] data, string signature, X509Certificate2 certificate);
    }
}
