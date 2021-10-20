using System;

namespace SPPC.Framework.Licensing
{
    internal class SshCommand
    {
        internal SshCommand(string command, string resultKey)
        {
            Command = command;
            ResultKey = resultKey;
        }

        internal string Command { get; }

        internal string ResultKey { get; }

        public override string ToString()
        {
            return String.Format("{0} ==> {1}", Command, ResultKey);
        }
    }
}
