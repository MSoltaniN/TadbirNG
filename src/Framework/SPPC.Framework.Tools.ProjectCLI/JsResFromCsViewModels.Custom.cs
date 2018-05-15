using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using SPPC.Framework.Common;

namespace SPPC.Framework.Tools.ProjectCLI
{
    public partial class JsResFromCsViewModels
    {
        public JsResFromCsViewModels(IEnumerable<Type> types, IDictionary<string, string> cachedResources)
        {
            _types = types.ToArray();
            _cachedResources = cachedResources;
        }

        private bool HasValidation(Type type)
        {
            int count = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => HasAttribute(prop, typeof(RequiredAttribute))
                    || HasAttribute(prop, typeof(StringLengthAttribute))
                    || HasAttribute(prop, typeof(CompareAttribute)))
                .Count();
            return count > 0;
        }

        private bool HasAttribute(PropertyInfo property, Type attributeType)
        {
            return (Reflector.GetPropertyAttribute(
                property.DeclaringType, property.Name, attributeType) != null);
        }

        private PropertyMetadata GetPropertyMetadata(PropertyInfo property)
        {
            var metadata = new PropertyMetadata() { Name = property.Name };
            var attribute = Reflector.GetPropertyAttribute(
                property.DeclaringType, property.Name, typeof(RequiredAttribute));
            metadata.IsRequired = attribute != null;
            if (metadata.IsRequired)
            {
                metadata.IsRequiredMessage = String.Format(
                    _cachedResources["FieldIsRequired"], _cachedResources[property.Name]);
            }

            var lengthAttribute = Reflector.GetPropertyAttribute(
                property.DeclaringType, property.Name, typeof(StringLengthAttribute)) as StringLengthAttribute;
            if (lengthAttribute != null)
            {
                int minLength = lengthAttribute.MinimumLength;
                int maxLength = lengthAttribute.MaximumLength;
                if (minLength > 0)
                {
                    metadata.HasMinLength = true;
                    metadata.LengthRangeMessage = String.Format(
                        _cachedResources["TextFieldHasRange"], _cachedResources[property.Name], minLength, maxLength);
                }
                else
                {
                    metadata.HasMaxLength = true;
                    metadata.MaxLengthMessage = String.Format(
                        _cachedResources["TextFieldIsTooLong"], _cachedResources[property.Name], maxLength);
                }
            }

            var compareAttribute = Reflector.GetPropertyAttribute(
                property.DeclaringType, property.Name, typeof(CompareAttribute)) as CompareAttribute;
            if (compareAttribute != null)
            {
                metadata.HasCompare = true;
                metadata.CompareToProperty = compareAttribute.OtherProperty;
                metadata.CompareMessage = String.Format(
                    _cachedResources["FieldsDoNotMatch"], _cachedResources[property.Name],
                    _cachedResources[compareAttribute.OtherProperty]);
            }

            return metadata;
        }

        private readonly Type[] _types;
        private readonly IDictionary<string, string> _cachedResources;
    }
}
