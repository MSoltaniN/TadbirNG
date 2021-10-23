using System;
using System.Collections.Generic;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public interface IRepositoryCommand
    {
        IDictionary<string, object> Parameters { get; }
        bool IsComplete { get; }
        IUserInputCollector InputCollector { get; }

        void Execute();
    }
}
