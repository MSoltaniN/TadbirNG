using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;

namespace SPPC.Tools.SystemDesigner.Cli
{
    public class DeployActivatorCommand : ICliCommand
    {
        public void Execute()
        {
            string path = ConfigurationManager.AppSettings["InstanceIdPath"];
            string main = Path.Combine(path, "instance.id");
            string old = Path.Combine(path, "_instance.id");
            if (File.Exists(main) && File.Exists(old))
            {
                Console.WriteLine();
                Console.WriteLine("Creating XCopy installation for TadbirActivator...");

                // Step 1: Create XCopy folder with instance key appended to its name...
                string suffix = File.ReadAllText(main).Substring(0, 10);
                string xcopyPath = String.Format(@".\TadbirNG.Activator-{0}", suffix);
                if (!Directory.Exists(xcopyPath))
                {
                    Directory.CreateDirectory(xcopyPath);
                }

                // Step 2: Extract fixed dependencies of activator tool to newly created folder...
                string sourcePath = ConfigurationManager.AppSettings["DeploySource"];
                ZipFile.ExtractToDirectory(sourcePath, xcopyPath);

                // Step 3: Copy Tadbir-specific dependency assemblies to newly created folder...
                foreach (string assembly in _assemblies)
                {
                    File.Copy(assembly, Path.Combine(xcopyPath, assembly));
                }

                // Step 4: If instance.id and _instance.id co-exist, perform cleanup...
                File.Delete(main);
                File.Move(old, main);
            }
        }

        private readonly string[] _assemblies = new string[]
        {
            "SPPC.Tools.TadbirActivator.exe", "SPPC.Framework.Common.dll",
            "SPPC.Framework.Cryptography.dll", "SPPC.Framework.Interfaces.dll",
            "SPPC.Framework.Service.dll", "SPPC.Licensing.Local.Persistence.dll",
            "SPPC.Licensing.Model.dll", "SPPC.Tadbir.Interfaces.dll", "SPPC.Tools.Interfaces.dll"
        };
    }
}
