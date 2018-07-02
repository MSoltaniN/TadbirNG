using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// Custom contract resolver used for controlling JSON serialization using <see cref="JsonSerializer"/> class.
    /// </summary>
    public class CustomJsonContractResolver : DefaultContractResolver, IContractResolver
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
        /// Creates properties for the given <see cref="JsonContract"/>
        /// </summary>
        /// <param name="type">The type to create properties for</param>
        /// <param name="memberSerialization">The member serialization mode for the type</param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            if (_ignoredProperties != null)
            {
                properties = properties
                    .Where(prop => !_ignoredProperties.Contains(prop.PropertyName))
                    .ToList();
            }

            return properties;
        }

        private readonly string[] _ignoredProperties;
    }
}
