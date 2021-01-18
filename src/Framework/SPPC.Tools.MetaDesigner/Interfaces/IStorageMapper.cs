using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Transforms
{
    public interface IStorageMapper
    {
        string MapPropertyType(BuiltinType type, int length);
    }
}
