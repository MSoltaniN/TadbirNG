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
            DisplayBanner();
            if (args.Length == 0)
            {
                Console.WriteLine("No parameter(s) specified.");
                Console.ReadLine();
                return -1;
            }

            string[] supportedCommands = new string[] { "tsvm", "tsapi" };
            string command = GetParameter(args, _commandParam);
            if (String.IsNullOrEmpty(command))
            {
                Console.ReadLine();
                return -1;
            }

            if (!supportedCommands.Contains(command))
            {
                Console.WriteLine("Command '{0}' is not supported.", command);
                Console.ReadLine();
                return -2;
            }

            string types = GetParameter(args, _typesParam);
            if (String.IsNullOrEmpty(types))
            {
                Console.ReadLine();
                return -1;
            }

            foreach (string param in types.Split(','))
            {
                HandleGenerationCommand(command, param);
            }

            Console.WriteLine("Press any key to quit...");
            Console.ReadKey(true);
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

        private static void HandleGenerationCommand(string command, string typeName)
        {
            if (command.ToLower() == "tsvm")
            {
                GenerateTypescriptModel(typeName);
            }
            else if (command.ToLower() == "tsapi")
            {
                GenerateTypescriptApi(typeName);
            }
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

        private static void GenerateTypescriptApi(string typeName)
        {
            string tsApiPath = ConfigurationManager.AppSettings["TsApiPath"];
            string csAssembly = ConfigurationManager.AppSettings["CsApiAssembly"];
            string csNamespace = ConfigurationManager.AppSettings["CsApiNamespace"];
            string csOutPath = ConfigurationManager.AppSettings["CsOutputPath"];
            string tsTypeName = typeName.CamelCase();
            string fullName = String.Format("{0}.{1}", csNamespace, typeName);
            string generatedPath = String.Format(@"{0}\{1}.ts", tsApiPath, tsTypeName);
            string assemblyPath = String.Format(@"{0}\{1}.dll", csOutPath, csAssembly);
            var assembly = Assembly.Load(csAssembly);
            if (assembly != null)
            {
                var csType = assembly.GetType(fullName);
                var template = new TsApiFromCsApi(csType);
                File.WriteAllText(generatedPath, template.TransformText());
            }
        }

        private static void DisplayBanner()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("SPPC Framework : Project Command-Line Interface (v1.1)");
            Console.WriteLine("(c) Copyright 2018, SPPC, All Rights Reserved");
            Console.WriteLine("============================================================");
            Console.WriteLine();
        }

        private static readonly string _commandParam = "-cmd:";
        private static readonly string _typesParam = "-types:";
    }
}
