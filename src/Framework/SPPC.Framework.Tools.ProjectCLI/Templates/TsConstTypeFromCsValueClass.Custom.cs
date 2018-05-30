using System;
using System.Reflection;

namespace SPPC.Framework.Tools.ProjectCLI.Templates
{
    public partial class TsConstTypeFromCsValueClass
    {
        public TsConstTypeFromCsValueClass(Type csType)
        {
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            _type = csType;
        }

        private readonly string _version;
        private readonly Type _type;
    }
}
