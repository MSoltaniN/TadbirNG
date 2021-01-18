using System;
using System.Linq;
using System.Reflection;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class TsApiFromCsApi
    {
        public TsApiFromCsApi(Type csType)
        {
            _csType = csType;
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private string[] GetFieldNames(Type csType)
        {
            return csType
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(fld => fld.Name)
                .Where(name => name.IndexOf("Url") == -1)
                .ToArray();
        }

        private string GetFieldValue(Type csType, string fieldName)
        {
            return csType
                .GetField(fieldName)
                .GetValue(null)
                .ToString();
        }

        private readonly Type _csType;
        private readonly string _version;
    }
}
