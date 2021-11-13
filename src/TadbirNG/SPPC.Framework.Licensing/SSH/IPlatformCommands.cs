using System.Collections.Generic;

namespace SPPC.Framework.Licensing
{
    internal interface IPlatformCommands
    {
        IEnumerable<SshCommand> AllCommands { get; }

        SshCommand GetProcessorId { get; }

        SshCommand GetMainboardId { get; }

        SshCommand GetSystemId { get; }
    }
}
