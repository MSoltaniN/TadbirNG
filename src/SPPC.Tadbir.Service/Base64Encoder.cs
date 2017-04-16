using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SwForAll.Platform.Common;

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
            var encodedValue = String.Empty;
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
                encodedValue = Transform.ToBase64String(stream.ToArray());
            }

            return encodedValue;
        }

        /// <summary>
        /// Decodes a data object from given Base64 encoding.
        /// </summary>
        /// <param name="encodedData">Base64-encoded form of output object</param>
        /// <returns>Decoded object</returns>
        public T Decode(string encodedData)
        {
            Verify.ArgumentNotNullOrEmptyString(encodedData, "encodedData");
            T data = default(T);
            using (var stream = new MemoryStream(Transform.FromBase64String(encodedData)))
            {
                var formatter = new BinaryFormatter();
                data = formatter.Deserialize(stream) as T;
            }

            return data;
        }
    }
}
