using System.Collections.Generic;
using SPPC.Tools.Presentation;

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
