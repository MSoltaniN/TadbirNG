using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SwForAll.Platform.Common;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// Provides basic support for JavaScript Object Notation (JSON) serialization/deserialization.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Serializes an object into JSON format.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        /// <param name="value">Object to serialize</param>
        /// <param name="indented">Indicates if JSON output needs to be formatted in a readable, indented form.
        /// Default is true.</param>
        /// <returns>Input object serialized as JSON</returns>
        public static string From<T>(T value, bool indented = true)
        {
            Verify.ArgumentNotNull(value, "value");

            using (var writer = new StringWriter(new StringBuilder()))
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = indented ? Formatting.Indented : Formatting.None;
                serializer.Serialize(writer, value);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Deserializes a well-formed JSON value into an object having specific type.
        /// </summary>
        /// <typeparam name="T">Type of deserialized object</typeparam>
        /// <param name="json">Value having JSON format</param>
        /// <returns>Object deserialized from specified JSON value</returns>
        public static T To<T>(string json)
        {
            Verify.ArgumentNotNullOrWhitespace(json, "json");

            using (var reader = new StringReader(json))
            {
                var serializer = new JsonSerializer();
                T value = (T)serializer.Deserialize(reader, typeof(T));
                return value;
            }
        }
    }
}
