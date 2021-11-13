using System.Collections.Generic;

namespace SPPC.Framework.Licensing
{
    internal static class SshCommands
    {
        static SshCommands()
        {
            Windows = new WindowsCommands();
            Linux = new LinuxCommands();
        }

        internal static IPlatformCommands Windows { get; }

        internal static IPlatformCommands Linux { get; }

        private class WindowsCommands : IPlatformCommands
        {
            internal WindowsCommands()
            {
                _getProcessorId = new SshCommand("wmic cpu get ProcessorId", "ProcessorId");
                _getMainboardId = new SshCommand("wmic baseboard get SerialNumber", "SerialNumber");
                _getSystemId = new SshCommand("wmic csproduct get UUID", "UUID");
                _allCommands = new List<SshCommand>
                {
                    _getProcessorId, _getMainboardId, _getSystemId
                };
            }

            IEnumerable<SshCommand> IPlatformCommands.AllCommands
            {
                get { return _allCommands; }
            }

            SshCommand IPlatformCommands.GetProcessorId
            {
                get { return _getProcessorId; }
            }

            SshCommand IPlatformCommands.GetMainboardId
            {
                get { return _getMainboardId; }
            }

            SshCommand IPlatformCommands.GetSystemId
            {
                get { return _getSystemId; }
            }

            private readonly SshCommand _getProcessorId;
            private readonly SshCommand _getMainboardId;
            private readonly SshCommand _getSystemId;
            private readonly IEnumerable<SshCommand> _allCommands;
        }

        private class LinuxCommands : IPlatformCommands
        {
            internal LinuxCommands()
            {
                _getProcessorId = new SshCommand("dmidecode -t processor | egrep ID", "ID:");
                _getMainboardId = new SshCommand("dmidecode -t baseboard | egrep id", "ID:");
                _getSystemId = new SshCommand("dmidecode -t system | egrep ID", "UUID:");
                _allCommands = new List<SshCommand>
                {
                    _getProcessorId, _getMainboardId, _getSystemId
                };
            }

            IEnumerable<SshCommand> IPlatformCommands.AllCommands
            {
                get { return _allCommands; }
            }

            SshCommand IPlatformCommands.GetProcessorId
            {
                get { return _getProcessorId; }
            }

            SshCommand IPlatformCommands.GetMainboardId
            {
                get { return _getMainboardId; }
            }

            SshCommand IPlatformCommands.GetSystemId
            {
                get { return _getSystemId; }
            }

            private readonly SshCommand _getProcessorId;
            private readonly SshCommand _getMainboardId;
            private readonly SshCommand _getSystemId;
            private readonly IEnumerable<SshCommand> _allCommands;
        }
    }
}
