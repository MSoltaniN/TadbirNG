using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SPPC.Framework.Common;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// Provides basic support for JavaScript Object Notation (JSON) serialization/deserialization.
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Serializes an object into JSON format.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        /// <param name="value">Object to serialize</param>
        /// <param name="indented">Indicates if JSON output needs to be formatted in a readable, indented form.
        /// Default is true.</param>
        /// <param name="ignoredProperties">Array of property names to ignore during serialization</param>
        /// <returns>Input object serialized as JSON</returns>
        public static string From<T>(T value, bool indented = true, string[] ignoredProperties = null)
        {
            return From((object)value, indented, ignoredProperties);
        }

        /// <summary>
        /// Serializes an object into JSON format.
        /// </summary>
        /// <param name="value">Object to serialize</param>
        /// <param name="indented">Indicates if JSON output needs to be formatted in a readable, indented form.
        /// Default is true.</param>
        /// <param name="ignoredProperties">Array of property names to ignore during serialization</param>
        /// <returns>Input object serialized as JSON</returns>
        public static string From(object value, bool indented = true, string[] ignoredProperties = null)
        {
            Verify.ArgumentNotNull(value, "value");
            using (var writer = new StringWriter(new StringBuilder()))
            {
                var serializer = new JsonSerializer
                {
                    Formatting = indented ? Formatting.Indented : Formatting.None,
                    ContractResolver = new CustomJsonContractResolver(ignoredProperties)
                };

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

        /// <summary>
        /// Deserializes a well-formed JSON value into an object.
        /// </summary>
        /// <param name="json">Value having JSON format</param>
        /// <param name="type">Type of object to deserialize from JSON</param>
        /// <returns>Object deserialized from specified JSON value</returns>
        public static object To(string json, Type type)
        {
            Verify.ArgumentNotNullOrWhitespace(json, "json");
            using (var reader = new JsonTextReader(new StringReader(json)))
            {
                var serializer = new JsonSerializer();
                object value = serializer.Deserialize(reader, type);
                return value;
            }
        }
    }
}
