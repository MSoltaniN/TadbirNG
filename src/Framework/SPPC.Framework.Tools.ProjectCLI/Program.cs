using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using SPPC.Tadbir.ViewModel.Finance;

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

            string[] supportedCommands = new string[] { "tsvm", "tsapi", "jsres" };
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
                HandleGenerationCommand(args, command, param);
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
            if (required && String.IsNullOrEmpty(param))
            {
                Console.WriteLine("ERROR: A required parameter ({0}) is missing.", paramName);
            }

            return param;
        }

        private static void HandleGenerationCommand(string[] args, string command, string typeName)
        {
            if (command.ToLower() == "tsvm")
            {
                GenerateTypescriptModel(typeName);
            }
            else if (command.ToLower() == "tsapi")
            {
                GenerateTypescriptApi(typeName);
            }
            else if (command.ToLower() == "jsres")
            {
                string area = GetParameter(args, _areaParam);
                if (String.IsNullOrEmpty(area))
                {
                    return;
                }

                string lang = GetParameter(args, _langParam, false);
                if (String.IsNullOrEmpty(lang))
                {
                    lang = "fa";
                    return;
                }

                GenerateJsonResources(area, lang);
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

        private static void GenerateJsonResources(string areas, string langs)
        {
            string csAssembly = ConfigurationManager.AppSettings["CsViewModelAssemblies"];
            string csOutPath = ConfigurationManager.AppSettings["CsOutputPath"];
            string jsResPath = ConfigurationManager.AppSettings["JsResPath"];
            string assemblyPath = String.Format(@"{0}\{1}.dll", csOutPath, csAssembly);
            var assembly = Assembly.Load(csAssembly);
            if (assembly == null)
            {
                Console.WriteLine("ERROR: Could not load view model assembly ({0}).", csAssembly);
                return;
            }

            foreach (string lang in langs.Split(','))
            {
                string jsonPath = String.Format(@"{0}\{1}.json", jsResPath, lang);
                var cachedResources = GetCachedResources(lang);
                var allTypes = new List<Type>();
                allTypes.Add(typeof(AccountViewModel));
                allTypes.Add(typeof(ProjectViewModel));
                allTypes.Add(typeof(CostCenterViewModel));
                foreach (string area in areas.Split(','))
                {
                    string csNamespace = String.Format("{0}.{1}", csAssembly, area);
                    //allTypes.AddRange(assembly
                    //    .GetTypes()
                    //    .Where(type => type.Namespace == csNamespace));
                }

                var template = new JsResFromCsViewModels(allTypes, cachedResources);
                string transformed = template.TransformText();
                string json = File.ReadAllText(jsonPath);
                json = json.Insert(json.Length - 2, transformed);
                File.WriteAllText(jsonPath, json, Encoding.UTF8);
            }
        }

        private static IDictionary<string, string> GetCachedResources(string lang)
        {
            var cachedResources = new Dictionary<string, string>();
            string csResPath = ConfigurationManager.AppSettings["CsResPath"];
            string resxPath = String.Format(@"{0}\{1}.{2}.resx", csResPath, _resxBaseName, lang);
            if (!File.Exists(resxPath))
            {
                // Assume default language resource file (without language code)...
                resxPath = String.Format(@"{0}\{1}.resx", csResPath, _resxBaseName);
            }

            using (var resReader = new ResXResourceReader(resxPath))
            {
                foreach (DictionaryEntry entry in resReader)
                {
                    cachedResources.Add(entry.Key.ToString(), entry.Value.ToString());
                }
            }

            return cachedResources;
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
        private static readonly string _areaParam = "-area:";
        private static readonly string _langParam = "-lang:";
        private static readonly string _resxBaseName = "AppStrings";
    }
}
