using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// Custom contract resolver used for controlling JSON serialization using <see cref="JsonSerializer"/> class.
    /// </summary>
    public class CustomJsonContractResolver : CamelCasePropertyNamesContractResolver, IContractResolver
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CustomJsonContractResolver"/> class
        /// </summary>
        /// <param name="ignoredProperties">Array of property names to ignore during serialization</param>
        public CustomJsonContractResolver(string[] ignoredProperties)
        {
            _ignoredProperties = ignoredProperties;
        }

        /// <summary>
        /// Gets the serializable members for the type
        /// </summary>
        /// <param name="objectType">The type to get serializable members for</param>
        /// <returns>The serializable members for the type</returns>
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var properties = objectType.GetProperties();
            if (_ignoredProperties != null)
            {
                return properties
                    .Where(prop => !_ignoredProperties.Contains(prop.Name))
                    .Cast<MemberInfo>()
                    .ToList();
            }

            return base.GetSerializableMembers(objectType);
        }

        private readonly string[] _ignoredProperties;
    }
}
