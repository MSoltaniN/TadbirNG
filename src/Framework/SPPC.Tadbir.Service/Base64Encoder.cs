using System;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Provides operations for encoding and decoding of a specific .NET runtime object type using Base64 scheme.
    /// </summary>
    /// <typeparam name="T">Type of object that this encoder can handle. This type must be a concrete form
    /// that can be instantiated.</typeparam>
    public class Base64Encoder<T> : ITextEncoder<T>
        where T : class, new()
    {
        /// <summary>
        /// Encodes given data object into Base64 format.
        /// </summary>
        /// <param name="data">Object to encode</param>
        /// <returns>Base64-encoded form of given object</returns>
        public string Encode(T data)
        {
            var json = Json.From(data, false);
            return Transform.ToBase64String(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// Decodes a data object from given Base64 encoding.
        /// </summary>
        /// <param name="encodedData">Base64-encoded form of output object</param>
        /// <returns>Decoded object</returns>
        public T Decode(string encodedData)
        {
            Verify.ArgumentNotNullOrEmptyString(encodedData, "encodedData");
            string json = Encoding.UTF8.GetString(Transform.FromBase64String(encodedData));
            T data = Json.To<T>(json);
            return data;
        }
    }
}
