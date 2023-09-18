using System;

namespace SPPC.Tools.Presentation
{
    public interface IUserInputCollector
    {
        void GetInput();

        object Output { get; }
    }
}
