using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SPPC.Framework.Common
{
    /// <summary>
    /// Provides common reflection services using .NET Reflection API. Using this class will free developers
    /// from learning the internals of Reflection API.
    /// </summary>
    public static partial class Reflector
    {
        #region Property Manipulation and Query

        /// <summary>
        /// Reads the current value of an instance property in an object.
        /// </summary>
        /// <param name="source">The object to read the property from</param>
        /// <param name="name">The name of the property to read from the object</param>
        /// <param name="throwIfInvalid">Indicates if operation should fail for an invalid property name</param>
        /// <returns>The property value (for a valid property name) or null (for an invalid property name)</returns>
        public static object GetSimpleProperty(object source, string name, bool throwIfInvalid = true)
        {
            if (throwIfInvalid)
            {
                VerifyIsValidProperty(source.GetType(), name);
            }

            PropertyInfo prop = source.GetType().GetProperty(name);
            return (prop?.GetValue(source, null));
        }

        /// <summary>
        /// Reads the current value of an instance property in an object.
        /// </summary>
        /// <param name="source">The object to read the property from</param>
        /// <param name="name">The name of the property to read from the object</param>
        /// <returns>The property value currently set in the object</returns>
        /// <remarks>The property specified by name can be either a simple property or a
        /// property path, where path items are separated by dot.</remarks>
        public static object GetProperty(object source, string name)
        {
            Verify.ArgumentNotNull(source);
            Verify.ArgumentNotNullOrWhitespace(name, "name");

            string[] items = name.Split('.');
            object sourceObject = source;
            object propValue = null;
            for (int i = 0; i < items.Length; i++)
            {
                propValue = GetSimpleProperty(sourceObject, items[i]);
                sourceObject = propValue;
            }

            return propValue;
        }

        /// <summary>
        /// Reads the current value of a static property in a type.
        /// </summary>
        /// <param name="sourceType">The type whose static property is being queried</param>
        /// <param name="name">The name of the property to read from the class</param>
        /// <returns>The current value of the static property in the specified type</returns>
        public static object GetProperty(Type sourceType, string name)
        {
            Verify.ArgumentNotNull(sourceType);
            Verify.ArgumentNotNullOrWhitespace(name, "name");

            VerifyIsValidProperty(sourceType, name);
            var propInfo = sourceType.GetProperty(name);
            return propInfo.GetValue(null, Array.Empty<object>());
        }

        /// <summary>
        /// Reads current value of an indexed property in the source object.
        /// </summary>
        /// <param name="source">An Object whose property must be read.</param>
        /// <param name="name">Name of an indexed property in the source object.</param>
        /// <param name="indexArgTypes">An array of Type objects specifying types of property indices.</param>
        /// <param name="indexArgValues">An object array specifying required property index values.</param>
        /// <returns>Current value of the indexed property.</returns>
        public static object GetProperty(object source, string name,
            Type[] indexArgTypes, object[] indexArgValues)
        {
            Verify.ArgumentNotNull(source, "source");

            if (indexArgTypes == null || indexArgTypes.Length == 0)
            {
                return (GetProperty(source, name));
            }

            PropertyInfo prop = source.GetType().GetProperty(name, indexArgTypes);
            return (prop.GetValue(source, indexArgValues));
        }

        /// <summary>
        /// Overwrites current value of an object property with a new value.
        /// </summary>
        /// <param name="source">The object we have to write the property value to</param>
        /// <param name="name">The name of the property to write in the object</param>
        /// <param name="value">The new value to write to the object property</param>
        public static void SetProperty(object source, string name, object value)
        {
            VerifyIsWritable(source, name);

            object sourceProp = GetProperty(source, name);

            // NOTE: Value types may be convertible, so we must skip next check for them...
            if (value != null && sourceProp != null && !value.GetType().IsValueType && !sourceProp.GetType().IsValueType)
            {
                Verify.TypeIsAssignableFromType(sourceProp.GetType(), value.GetType());
            }

            PropertyInfo prop = source.GetType().GetProperty(name);
            prop.SetValue(source, value, null);
        }

        /// <summary>
        /// Copies a property value from a source object into a target object.
        /// </summary>
        /// <param name="source">The object to read the property from</param>
        /// <param name="sourceProp">The name of the property to read from the source object</param>
        /// <param name="target">The object we have to write the property value to</param>
        /// <param name="targetProp">The name of the property to write in the target object</param>
        public static void CopyProperty(object source, string sourceProp, object target, string targetProp)
        {
            object val = GetProperty(source, sourceProp);
            SetProperty(target, targetProp, val);
        }

        /// <summary>
        /// Determines if a property in a source object is read-only.
        /// </summary>
        /// <param name="source">The object to read the property from</param>
        /// <param name="propertyName">The name of the property to read from the object</param>
        /// <returns>True if the specified property is read-only (i.e. can not be set); otherwise returns false.</returns>
        public static bool IsReadOnly(object source, string propertyName)
        {
            Verify.ArgumentNotNull(source, "source");

            VerifyIsValidProperty(source.GetType(), propertyName);
            var prop = source.GetType().GetProperty(propertyName);
            return (prop.CanWrite == false || prop.GetSetMethod() == null);
        }

        /// <summary>
        /// Determines if a specified property in a source object is a generic collection.
        /// </summary>
        /// <param name="source">The object to read the property from</param>
        /// <param name="propertyName">The name of the property to read from the object</param>
        /// <returns>True if the runtime type of specified property is one of generic collection types;
        /// otherwise returns false.</returns>
        public static bool IsGenericCollection(object source, string propertyName)
        {
            var property = Reflector.GetProperty(source, propertyName);
            return IsGenericCollection(property);
        }

        /// <summary>
        /// Determines if a specified object is a generic collection.
        /// </summary>
        /// <param name="source">The object whose type is checked.</param>
        /// <returns>True if the runtime type of specified object is one of generic collection types;
        /// otherwise returns false.</returns>
        public static bool IsGenericCollection(object source)
        {
            return (source != null && source is IEnumerable && source.GetType().IsGenericType);
        }

        /// <summary>
        /// Gets the type of the specified property in an object instance.
        /// </summary>
        /// <param name="source">An object whose specific property will be queried</param>
        /// <param name="propertyName">Name of the property to read from the given object. If the given object
        /// does not implement the property, an exception will be thrown.</param>
        /// <returns>Type of the specified property in the given object</returns>
        public static Type GetPropertyType(object source, string propertyName)
        {
            Verify.ArgumentNotNull(source, "source");
            return GetPropertyType(source.GetType(), propertyName);
        }

        /// <summary>
        /// Gets the type of the specified property in a given type.
        /// </summary>
        /// <param name="sourceType">A type whose specific property will be queried</param>
        /// <param name="propertyName">Name of the property to query from the given type. If the given type
        /// does not implement the property, an exception will be thrown.</param>
        /// <returns>Type of the specified property in the given type</returns>
        public static Type GetPropertyType(Type sourceType, string propertyName)
        {
            Verify.ArgumentNotNull(sourceType, "sourceType");
            Verify.ArgumentNotNullOrWhitespace(propertyName, "propertyName");

            VerifyIsValidProperty(sourceType, propertyName);
            return sourceType.GetProperty(propertyName).PropertyType;
        }

        /// <summary>
        /// Reflects on the given object and returns the names of all public properties.
        /// </summary>
        /// <param name="source">The source object whose property names must be returned.</param>
        /// <returns>A string array containing all property names.</returns>
        public static string[] GetPropertyNames(object source)
        {
            Verify.ArgumentNotNull(source, "source");

            return GetPropertyNames(source.GetType());
        }

        /// <summary>
        /// Reflects on the given .NET type and returns the names of all public properties.
        /// </summary>
        /// <param name="type">The .NET type whose property names must be returned.</param>
        /// <returns>A string array containing all property names.</returns>
        public static string[] GetPropertyNames(Type type)
        {
            Verify.ArgumentNotNull(type, "type");

            string[] propertyNames = type.GetProperties()
                .Select(info => info.Name)
                .ToArray();
            return propertyNames;
        }

        /// <summary>
        /// Reflects on the given object and returns the names of all properties having a specified Type.
        /// </summary>
        /// <param name="source">The source object whose property names must be returned.</param>
        /// <param name="propertyType">The type of properties that must be returned.</param>
        /// <returns>A string array containing all property names having the specified Type.</returns>
        /// <remarks>Current implementation performs an exact match of the requested property type; in other words,
        /// it does not return properties having types derived from the given type.</remarks>
        public static string[] GetPropertyNames(object source, Type propertyType)
        {
            Verify.ArgumentNotNull(source, "source");

            string[] propertyNames = source.GetType()
                .GetProperties()
                .Where(info => info.PropertyType == propertyType)
                .Select(info => info.Name)
                .ToArray();
            return propertyNames;
        }

        #endregion

        #region Type Instantiation and Query

        /// <summary>
        /// Creates a new instance of a type given by its full type name.
        /// </summary>
        /// <param name="typeName">Fully qualified name of the type that should be instantiated.
        /// </param>
        /// <returns>A new instance of the given type.</returns>
        /// <remarks>If the specified type does not implement a public default constructor, an exception will be thrown.
        /// </remarks>
        public static object Instantiate(string typeName)
        {
            Type type = Type.GetType(typeName, true);
            return (Instantiate(type));
        }

        /// <summary>
        /// Creates a new instance of a type given by the exact .NET type.
        /// </summary>
        /// <param name="type">A Type object specifying the exact type to instantiate.</param>
        /// <returns>A new instance of the given type.</returns>
        /// <remarks>If the specified type does not implement a public default constructor, an exception will be thrown.
        /// </remarks>
        public static object Instantiate(Type type)
        {
            object instance = Activator.CreateInstance(type);
            return instance;
        }

        /// <summary>
        /// Creates a new instance of a type given by the exact .NET type and a variable number of arguments
        /// required by the type's constructor.
        /// </summary>
        /// <param name="type">A Type object specifying the exact type to instantiate.</param>
        /// <param name="args">A variable number of arguments required by the type's constructor.</param>
        /// <returns>A new instance of the given type.</returns>
        public static object Instantiate(Type type, params object[] args)
        {
            object instance = Activator.CreateInstance(type, args);
            return instance;
        }

        /// <summary>
        /// Determines whether a type derives from a specific type.
        /// </summary>
        /// <param name="type">A potential derived Type.</param>
        /// <param name="baseType">A potential base Type.</param>
        /// <returns>True if [type] is derived from [baseType], otherwise false. </returns>
        /// <remarks>Current implementation only supports reference types.</remarks>
        public static bool DerivesFrom(Type type, Type baseType)
        {
            Verify.ArgumentNotNull(baseType, "baseType");

            bool derives = false;
            if (type == baseType)
            {
                derives = true;
            }
            else if (baseType.IsInterface)
            {
                derives = DerivesFromInterface(type, baseType);
            }
            else
            {
                derives = DerivesFromClass(type, baseType);
            }

            return derives;
        }

        /// <summary>
        /// Determines whether a type derives from a specific class. This implies that the type given by
        /// baseType argument must be a class, as opposed to an interface.
        /// </summary>
        /// <param name="type">A potential derived Type.</param>
        /// <param name="baseType">A potential base class.</param>
        /// <returns>True if [type] is derived from [baseType] class, otherwise false. </returns>
        public static bool DerivesFromClass(Type type, Type baseType)
        {
            Verify.ArgumentNotNull(type, "type");

            return (type.IsSubclassOf(baseType));
        }

        /// <summary>
        /// Determines whether a type derives from a specific interface. This implies that the type given by
        /// baseType argument must be an interface, as opposed to a class.
        /// </summary>
        /// <param name="type">A potential derived Type.</param>
        /// <param name="baseType">A potential base interface.</param>
        /// <returns>True if [type] is derived from [baseType] interface, otherwise false. </returns>
        public static bool DerivesFromInterface(Type type, Type baseType)
        {
            Verify.ArgumentNotNull(type, "type");

            if (!baseType.IsInterface)
            {
                throw ExceptionBuilder.NewArgumentException("Specified type must be an interface type.", "baseType");
            }

            Type[] interfaces = type.GetInterfaces();
            return (Array.IndexOf(interfaces, baseType) != -1);
        }

        /// <summary>
        /// Returns a generic type whose type argument(s) match the specified type array and whose base type
        /// corresponds to the given type.
        /// </summary>
        /// <param name="baseGenericType">The base type of required generic type.</param>
        /// <param name="typeArguments">a variable array of type arguments expected by the required generic type.</param>
        /// <returns>The required generic type.</returns>
        public static Type GetGenericType(Type baseGenericType, params Type[] typeArguments)
        {
            Verify.ArgumentNotNull(baseGenericType, "baseGenericType");
            Verify.ArgumentNotNull(typeArguments, "typeArguments");

            Type[] argList = baseGenericType.GetGenericArguments();

            // Make sure given generic arguments are actually expected by base generic type...
            if (argList.Length != typeArguments.Length)
            {
                throw (ExceptionBuilder.NewArgumentException("Insufficient number of generic arguments"));
            }

            Type genericType = baseGenericType.MakeGenericType(typeArguments);
            return genericType;
        }

        #endregion

        #region Attribute Query

        /// <summary>
        /// Reflects on the given type and returns a specific attribute of that type.
        /// </summary>
        /// <param name="targetType">An ICustomAttributeProvider instance corresponding to the type whose custom
        /// attribute will be queried.</param>
        /// <param name="attributeType">The type of attribute to read from target type.</param>
        /// <returns>An Attribute instance assigned to the given Type, if the attribute is available;
        /// otherwise null.</returns>
        public static Attribute GetAttribute(ICustomAttributeProvider targetType, Type attributeType)
        {
            Verify.ArgumentNotNull(targetType, "targetType");

            Attribute attribute = targetType.GetCustomAttributes(attributeType, false)
                .FirstOrDefault() as Attribute;
            return attribute;
        }

        /// <summary>
        /// Reflects on the given type and returns a specific attribute from a property in that type, if available.
        /// </summary>
        /// <param name="targetType">The type from which the specified attribute will be queried</param>
        /// <param name="propertyName">Name of a property in the target type whose attribute is required</param>
        /// <param name="attributeType">The type of attribute to read from the specified property in the target type.
        /// </param>
        /// <returns>An Attribute instance assigned to the property of the given Type, if available;
        /// otherwise null.</returns>
        public static Attribute GetPropertyAttribute(Type targetType, string propertyName, Type attributeType)
        {
            Verify.ArgumentNotNull(targetType, "targetType");
            Verify.ArgumentNotNullOrEmptyString(propertyName, "propertyName");

            VerifyIsValidProperty(targetType, propertyName);
            Attribute attribute = targetType.GetProperty(propertyName)
                .GetCustomAttributes(attributeType, false)
                .FirstOrDefault() as Attribute;
            return attribute;
        }

        #endregion

        #region Method Call Support

        /// <summary>
        /// Invokes a method specified by name on the given object while passing given arguments to the method.
        /// </summary>
        /// <param name="source">The object whose method must be invoked.</param>
        /// <param name="methodName">Name of method to invoke.</param>
        /// <param name="args">An object array that will be passed to the method as arguments.
        /// If given argument list doesn't contain all arguments required by the method, or one
        /// or more arguments have types that don't match the type expected by the method, an exception
        /// will be thrown.</param>
        public static object Invoke(object source, string methodName, params object[] args)
        {
            Verify.ArgumentNotNull(source, "source");
            Verify.ArgumentNotNullOrWhitespace(methodName, "methodName");

            Type[] paramTypes = Type.EmptyTypes;
            if (args != null)
            {
                paramTypes = args
                    .Select(a => a.GetType())
                    .ToArray();
            }

            VerifyIsValidMethod(source.GetType(), methodName);
            MethodInfo methodInfo = source.GetType().GetMethod(methodName, paramTypes);
            return methodInfo.Invoke(source, args);
        }

        #endregion

        #region Implementation

        private static void VerifyIsValidProperty(Type type, string property)
        {
            PropertyInfo prop = type.GetProperty(property);
            if (prop == null)
            {
                var message = String.Format(
                    "Type '{0}' does not implement any property by the name '{1}'.",
                    type.FullName, property);
                throw ExceptionBuilder.NewArgumentException(message, "property");
            }
        }

        private static void VerifyIsValidMethod(Type type, string methodName)
        {
            MethodInfo method = type.GetMethod(methodName);
            if (method == null)
            {
                var message = String.Format(
                    "Type '{0}' does not implement any method by the name '{1}'.",
                    type.FullName, methodName);
                throw ExceptionBuilder.NewArgumentException(message, "methodName");
            }
        }

        private static void VerifyIsWritable(object source, string propertyName)
        {
            if (IsReadOnly(source, propertyName))
            {
                var message = String.Format(
                    "Property '{0}' in type '{1}' cannot be written to -- It is read-only.",
                    propertyName, source.GetType().FullName);
                throw ExceptionBuilder.NewInvalidOperationException(message);
            }
        }

        #endregion
    }
}
