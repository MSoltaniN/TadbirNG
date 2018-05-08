using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SPPC.Framework.Tools.ProjectCLI
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No parameter(s) specified.");
                return -1;
            }

            string[] typesParam = GetParameter(args, _typesParam)
                .Split(',');
            foreach (string param in typesParam)
            {
                GenerateTypescriptModel(param);
            }

            return 0;
        }

        private static string GetParameter(string[] args, string paramName, bool required = true)
        {
            string param = args
                .Where(arg => arg.StartsWith(paramName))
                .FirstOrDefault();
            param = param?.Replace(paramName, String.Empty).Trim('"', '\'');
            if (String.IsNullOrEmpty(param))
            {
                Console.WriteLine("ERROR: A required parameter ({0}) is missing.", paramName);
            }

            return param;
        }

        private static void GenerateTypescriptModel(string typeName)
        {
            string tsModelPath = ConfigurationManager.AppSettings["TsModelPath"];
            string csAssembly = ConfigurationManager.AppSettings["CsViewModelAssemblies"];
            string csOutPath = ConfigurationManager.AppSettings["CsOutputPath"];
            string tsTypeName = typeName
                .Split('.')
                .Last()
                .Replace("ViewModel", String.Empty)
                .CamelCase();
            string fullName = String.Format("{0}.{1}", csAssembly, typeName);
            string generatedPath = String.Format(@"{0}\{1}.ts", tsModelPath, tsTypeName);
            string assemblyPath = String.Format(@"{0}\{1}.dll", csOutPath, csAssembly);
            var assembly = Assembly.Load(csAssembly);
            if (assembly != null)
            {
                var csType = assembly.GetType(fullName);
                var template = new TsModelFromCsViewModel(csType);
                File.WriteAllText(generatedPath, template.TransformText());
            }
        }

        private static readonly string _typesParam = "-types:";
    }
}
