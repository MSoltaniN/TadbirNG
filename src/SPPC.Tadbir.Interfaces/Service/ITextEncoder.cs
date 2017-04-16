using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// Defines a simple contract for encoding and decoding a specific .NET runtime object type using a string form.
    /// </summary>
    /// <typeparam name="T">Type of object that this encoder can handle. This type must be a concrete form
    /// that can be instantiated.</typeparam>
    public interface ITextEncoder<T>
        where T : class, new()
    {
        /// <summary>
        /// Encodes given data object into a string form.
        /// </summary>
        /// <param name="data">Object to encode</param>
        /// <returns>String-encoded form of given object</returns>
        string Encode(T data);

        /// <summary>
        /// Decodes a data object from given encoded form.
        /// </summary>
        /// <param name="encodedData">Encoded form of output object</param>
        /// <returns>Decoded object</returns>
        T Decode(string encodedData);
    }
}
