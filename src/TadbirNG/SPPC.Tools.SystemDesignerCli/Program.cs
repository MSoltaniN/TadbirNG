using System;

namespace SPPC.Tools.SystemDesignerCli
{
    class Program
    {
        static void Main(params string[] args)
        {
            if (args.Length > 0)
            {
                CliRunner.Run(args);
            }
        }
    }
}
