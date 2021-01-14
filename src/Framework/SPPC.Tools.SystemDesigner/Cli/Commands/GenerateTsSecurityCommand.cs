using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesigner.Cli
{
    public class GenerateTsSecurityCommand : ICliCommand
    {
        public void Execute()
        {
            string assemblyName = ConfigurationManager.AppSettings["CsInterfacesAssembly"];
            string securityNamespace = ConfigurationManager.AppSettings["CsSecurityNamespace"];
            string tsAppPath = ConfigurationManager.AppSettings["TsAppPath"];
            string csTypeName = String.Format("{0}.SecureEntity", securityNamespace);
            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
            {
                Console.WriteLine(
                    "ERROR: Could not load interfaces assembly (Error code : {0}).", CliResult.TypeLoadError);
                return;
            }

            string generatedPath = Path.Combine(tsAppPath, "permissions.ts");
            var types = assembly.GetTypes()
                .Where(t => t.IsEnum && t.Name.EndsWith("Permissions"));
            Console.WriteLine("Generating TypeScript enum types for permissions...");
            var template1 = new TsPermissionsFromCsPermissions(types);
            File.WriteAllText(generatedPath, template1.TransformText());

            generatedPath = Path.Combine(tsAppPath, "secureEntity.ts");
            var type = assembly.GetType(csTypeName);
            Console.WriteLine("Generating TypeScript constant type for entity names...");
            var template2 = new TsConstTypeFromCsValueClass(type);
            File.WriteAllText(generatedPath, template2.TransformText());
        }
    }
}
