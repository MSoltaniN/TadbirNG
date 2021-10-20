using System;
using System.Collections.Generic;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public interface ICommandParameterBuilder
    {
        ICommandContext Context { get; set; }

        object Build(string parameterName);
    }
}
