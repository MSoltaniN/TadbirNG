using System;
using System.Collections.Generic;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public interface IUserInputCollector
    {
        void GetInput();

        object Output { get; }
    }
}
