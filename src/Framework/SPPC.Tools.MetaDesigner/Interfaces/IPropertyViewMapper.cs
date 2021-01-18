using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Transforms
{
    public interface IPropertyViewMapper
    {
        ViewType MapPropertyType(BuiltinType type);
        string GetDefaultName(string name, ViewType viewType);
        string GetDefaultBindingMember(ViewType viewType);
    }
}
