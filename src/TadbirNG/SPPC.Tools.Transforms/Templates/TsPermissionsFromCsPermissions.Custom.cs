using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class TsPermissionsFromCsPermissions
    {
        public TsPermissionsFromCsPermissions(IEnumerable<Type> csTypes)
        {
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            _types = csTypes.ToArray();
        }

        private readonly string _version;
        private readonly Type[] _types;
    }
}
