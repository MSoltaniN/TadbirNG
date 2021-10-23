using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesignerCli
{
    public class GenerateJsResourceCommand : ICliCommand
    {
        public GenerateJsResourceCommand(params string[] args)
        {
            if (args.Length < 2)
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Insufficient arguments were provided (needed 2).", "paramValues");
            }

            _area = args[0];
            _lang = args[1];
        }

        public void Execute()
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

            foreach (string lang in _lang.Split(','))
            {
                string jsonPath = String.Format(@"{0}\{1}.json", jsResPath, lang);
                var cachedResources = GetCachedResources(lang);
                var allTypes = new List<Type>();
                foreach (string area in _area.Split(','))
                {
                    string csNamespace = String.Format("{0}.{1}", csAssembly, area);
                    allTypes.AddRange(assembly
                        .GetTypes()
                        .Where(type => type.Namespace == csNamespace));
                }

                Console.WriteLine("Generating JSON localized strings for language '{0}'...", lang);
                var template = new JsResFromCsViewModels(allTypes, cachedResources);
                string transformed = template.TransformText();
                string json = File.ReadAllText(jsonPath);
                json = json.Insert(json.Length - 2, transformed);
                File.WriteAllText(jsonPath, json, Encoding.UTF8);
            }
        }

        private IDictionary<string, string> GetCachedResources(string lang)
        {
            var cachedResources = new Dictionary<string, string>();
            string csResPath = ConfigurationManager.AppSettings["CsResPath"];
            string resxPath = String.Format(@"{0}\{1}.{2}.resx", csResPath, _resxBaseName, lang);
            if (!File.Exists(resxPath))
            {
                // Assume default language resource file (without language code)...
                resxPath = String.Format(@"{0}\{1}.resx", csResPath, _resxBaseName);
            }

            var resReader = new ResXResourceReader(resxPath);
                foreach (var entry in resReader.StringResources)
            {
                cachedResources.Add(entry.Key.ToString(), entry.Value.ToString());
            }

            return cachedResources;
        }

        private readonly string _resxBaseName = "AppStrings";
        private readonly string _area;
        private readonly string _lang;
    }
}
