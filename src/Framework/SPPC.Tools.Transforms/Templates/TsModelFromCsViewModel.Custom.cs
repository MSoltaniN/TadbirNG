using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class TsModelFromCsViewModel
    {
		public TsModelFromCsViewModel(Type csType)
        {
            _csType = csType;
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private string GetTypescriptTypeName(PropertyInfo property)
        {
            string tsType = property.PropertyType.Name.Replace("ViewModel", String.Empty);
            if (property.PropertyType.FullName == "System.String")
            {
                tsType = "string";
            }
            else if (property.PropertyType.FullName == "System.DateTime")
            {
                tsType = "Date";
            }
            else if (property.PropertyType.FullName == "System.Boolean")
            {
                tsType = "boolean";
            }
            else if (IsNumber(property))
            {
                tsType = "number";
            }
            else if (property.PropertyType.Name.Contains("IList"))
            {
                var genericArgs = property.PropertyType.GenericTypeArguments;
                if (genericArgs.Length == 1)
                {
                    tsType = String.Format("Array<{0}>", genericArgs[0].Name.Replace("ViewModel", String.Empty));
                }
            }
            else if (property.PropertyType.Name.Contains("IEnumerable"))
            {
                var genericArgs = property.PropertyType.GenericTypeArguments;
                if (genericArgs.Length == 1)
                {
                    tsType = String.Format("{0}[]", genericArgs[0].Name.Replace("ViewModel", String.Empty));
                }
            }

            return tsType;
        }

        private string[] GetReferencedModelTypes(Type type)
        {
            var modelTypes = new List<string>();
            var nonTsTypes = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => IsModelType(prop));
            foreach (var prop in nonTsTypes)
            {
                if (prop.PropertyType.IsGenericType)
                {
                    modelTypes.Add(prop.PropertyType.GenericTypeArguments.Select(t => t.Name.Replace("ViewModel", String.Empty)).First());
                }
                else
                {
                    modelTypes.Add(prop.PropertyType.Name.Replace("ViewModel", String.Empty));
                }
            }

            return modelTypes.Distinct().ToArray();
        }

        private bool IsModelType(PropertyInfo property)
        {
            var tsTypeNames = new string[] { "number", "string", "boolean", "Date" };
            string tsType = GetTypescriptTypeName(property);
            return tsTypeNames.All(name => !tsType.Contains(name));
        }

        private bool IsNumber(PropertyInfo property)
        {
            string[] numberTypes = new string[] { "Int16", "Int32", "Int64", "Decimal", "Single", "Double" };
            return numberTypes.Any(type => property.PropertyType.FullName.Contains(type));
        }

        private bool IsNullable(PropertyInfo property)
        {
            string typeName = property.PropertyType.Name;
            return (property.PropertyType.Name.IndexOf("Nullable") != -1
                || property.Name == "Description");
        }

        private readonly Type _csType;
        private readonly string _version;
    }
}
