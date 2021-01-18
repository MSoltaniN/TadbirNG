using System;
using System.ComponentModel;
using System.Linq;
using SPPC.Framework.Common;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Extracts a name from a given object using different naming strategies.
    /// </summary>
    public class ObjectNameProvider
    {
        /// <summary>
        /// Returns a name for a given object using either a predefined naming priority or the DisplayName attribute
        /// of an optional property in the source object.
        /// </summary>
        /// <param name="source">Source object to provide a name for.</param>
        /// <param name="property">Name of a property in the source object to use for naming.</param>
        /// <returns>A name for the source object.</returns>
        /// <remarks>When property argument is not null, the DisplayName attribute of the property by that name
        /// is used as the object name. When property argument is null, this method returns the value of Name
        /// property in source object, if present; otherwise returns the name of the CLR type of source object.
        /// </remarks>
        public static string GetName(object source, string property = null)
        {
            var name = String.Empty;
            if (source != null)
            {
                if (String.IsNullOrWhiteSpace(property))
                {
                    name = GetObjectName(source);
                    if (String.IsNullOrWhiteSpace(name))
                        name = GetTypeName(source);
                }
                else
                {
                    name = GetDisplayName(source, property);
                }
            }

            return name;
        }

        /// <summary>
        /// Returns the DisplayName property of DisplayName attribute of a specified property in source object.
        /// </summary>
        /// <param name="source">Source object to provide a name for.</param>
        /// <param name="property">Name of a property in the source object to use for naming.</param>
        /// <returns>Display name of the specified property in source object, derived from DisplayName attribute
        /// of that property.</returns>
        public static string GetDisplayName(object source, string property)
        {
            Verify.ArgumentNotNull(source, "source");
            Verify.ArgumentNotNullOrWhitespace(property, "property");
            var attribute = Reflector.GetPropertyAttribute(source.GetType(), property, typeof(DisplayNameAttribute))
                as DisplayNameAttribute;
            var displayName = (attribute != null) ? attribute.DisplayName : String.Empty;
            return displayName;
        }

        /// <summary>
        /// Returns the value of Name property in source object. Throws ArgumentException if the source object
        /// does not implement a Name property.
        /// </summary>
        /// <param name="source">Source object to provide a name for.</param>
        /// <returns>Value of Name property in source object.</returns>
        public static string GetObjectName(object source)
        {
            Verify.ArgumentNotNull(source, "source");
            var name = Reflector.GetPropertyNames(source).Contains("Name")
                ? Reflector.GetProperty(source, "Name") as string
                : String.Empty;
            return name;
        }

        /// <summary>
        /// Returns the name of CLR type of source object.
        /// </summary>
        /// <param name="source">Source object to provide a name for.</param>
        /// <returns>Name of CLR type of source object.</returns>
        public static string GetTypeName(object source)
        {
            Verify.ArgumentNotNull(source, "source");
            return source.GetType().Name;
        }
    }
}
